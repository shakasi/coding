using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using fastJSON;
using System.ServiceModel;
using log4net;

using Cuscapi.BLL;
using Cuscapi.Utils;
using Cuscapi.Model;
using Transight.HQV4.HQService.Contracts;
using System.Collections.Concurrent;
using System.ServiceModel.Channels;
using System.Configuration;

namespace Transight.HQV4.HQService.JsonService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Operations : IJsonOperation
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static JSONParameters _jsonParameters = new JSONParameters { UseExtensions = false, UseUTCDateTime = false, UsingGlobalTypes = false };

        private string _connStr = System.Configuration.ConfigurationManager.ConnectionStrings["HQ"].ToString();

        public string Test(UserInfo user)
        {
            user.Description = "我是服务器改过了";
            var returnValue = CompressionHelper.Compress(JSON.Instance.ToJSON(user, _jsonParameters));
            return returnValue;
        }
        public void WriteFileServerLog(string storenum, string fileNumber)
        {
            try
            {
                string sql = @"insert into RevFileServerLog
                                          (
                                            storenum,
                                            fileNumber,
                                            IsDownload,
                                            UpDT
                                           )
                                       values
                                          (
                                            @storenum,
                                            @fileNumber,
                                            1,
                                            getdate()
                                          )
                                    ";
                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.ExecuteNonQuery(sql, new SqlParameter("@storenum", storenum), new SqlParameter("@fileNumber", fileNumber));
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                throw;
            }
        }

        public void WriteFileServerLogs(byte[] fileInfos)
        {
            //获取用户传来的DataSet序列化 并反序列化
            MemoryStream ms = new MemoryStream(fileInfos);
            BinaryFormatter bf = new BinaryFormatter();
            Object o = bf.Deserialize(ms);
            DataSetSurrogate.DataSetSurrogate dss = (DataSetSurrogate.DataSetSurrogate)o;
            DataSet dsFileInfos = dss.ConvertToDataSet();

            using (SqlConnection destinationConnection =
                           new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HQ"].ToString()))
            {
                destinationConnection.Open();

                using (SqlTransaction transaction =
                           destinationConnection.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(
                               destinationConnection, SqlBulkCopyOptions.Default,
                               transaction))
                    {
                        bulkCopy.BatchSize = 10;
                        bulkCopy.DestinationTableName = "dbo.RevFileServerLog";

                        // Write from the source to the destination.
                        // This should fail with a duplicate key error.
                        try
                        {
                            bulkCopy.WriteToServer(dsFileInfos.Tables[0]);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _log.Fatal(ex.Message);
                            throw ex;
                        }
                    }
                }
            }
        }

        public void UploadSalesData(byte[] salesData, string storeNo, int value)
        {
            if (value != -1)
            {
                byte[] normalSalesData = CompressionHelper.Decompress(salesData);
                _log.Info("上传数据:" + salesData.Length + "|" + normalSalesData.Length);

                //获取用户传来的DataSet序列化 并反序列化
                MemoryStream ms = new MemoryStream(normalSalesData);
                BinaryFormatter bf = new BinaryFormatter();
                Object o;
                try
                {
                    o = bf.Deserialize(ms);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                DataSetSurrogate.DataSetSurrogate dss = (DataSetSurrogate.DataSetSurrogate)o;
                DataSet dsSalesData = dss.ConvertToDataSet();
                _log.Info("上传数据:" + dsSalesData.Tables[0].Rows.Count + "|" + dsSalesData.Tables[1].Rows.Count + "|" + dsSalesData.Tables[2].Rows.Count);

                //using (SqlConnection destinationConnection =
                //               new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HQ"].ToString()))
                //{
                //    destinationConnection.Open();

                //    using (SqlTransaction transaction =
                //               destinationConnection.BeginTransaction())
                //    {
                //        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(
                //                   destinationConnection, SqlBulkCopyOptions.Default,
                //                   transaction))
                //        {
                //            bulkCopy.BatchSize = 10;

                //            try
                //            {
                //                bulkCopy.DestinationTableName = "dbo.oprDTChecks";
                //                bulkCopy.WriteToServer(dsSalesData.Tables[0]);

                //                bulkCopy.DestinationTableName = "dbo.oprDTTransactions";
                //                bulkCopy.WriteToServer(dsSalesData.Tables[1]);

                //                bulkCopy.DestinationTableName = "dbo.oprDTDetails";
                //                bulkCopy.WriteToServer(dsSalesData.Tables[2]);

                //                transaction.Commit();
                //                SaveUploadSaleDateLog(storeNo, value);

                //            }
                //            catch (Exception ex)
                //            {
                //                try
                //                {
                //                    SaveUploadSaleDateLog(storeNo, -1);
                //                }
                //                catch
                //                { }
                //                transaction.Rollback();
                //                LogHelper.WriteLogFile(ex.Message);
                //                throw ex;
                //            }
                //        }
                //    }
                //}

                //用存在就Update代替上面纯粹新增 2017.01.05 shaka
                WCFDataSyncBLL _wCFDataSyncBLL = new WCFDataSyncBLL();
                _wCFDataSyncBLL.UploadSalesData(dsSalesData);
            }
            else
            {
                //门店返回报错信息 value==-1
                if (storeNo != null && value == -1)
                {
                    SaveUploadSaleDateLog(storeNo, value);
                }
            }
        }

        private void SaveUploadSaleDateLog(string storeNo, int value)
        {
            try
            {
                string sql = @"insert into [uploadsaleslog]
                                          (
                                            [storenum],
                                            [status],
                                            [updt]
                                           )
                                       values
                                          (
                                            @storenum,
                                            @status,
                                            getdate()
                                          )
                                    ";

                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.ExecuteNonQuery(sql, new SqlParameter("@storenum", storeNo), new SqlParameter("@status", value));

            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
            }
        }

        public bool HasAnyNewUpdate(string storenum)
        {
            try
            {
                string sql = @"select count(1) from RevUpdatePkgAllocation where storenum=@storenum and IsUpdated is null";

                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                object result = sqlHelper.ExecuteScalar(sql, new SqlParameter("@storenum", storenum));

                if (result != null && int.Parse(result.ToString()) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                throw ex;
            }
        }

        public DataSet GetUpdatePkgInfo(string storenum, string isUpdate)
        {
            try
            {
                string strSql = " IsUpdated is null ";
                if (!string.IsNullOrEmpty(isUpdate))
                    strSql = string.Format(@" IsUpdated={0} ", isUpdate);

                string sql = string.Format(@"select top 1 a.number id, a.storenum, a.pkgNumber, b.name pkgName, b.version pkgVersion, b.DownloadURL pkgDownUrl, a.IsForceUpdate
                             from RevUpdatePkgAllocation a, RevUpdatePkg b
                            where a.storenum=@storenum and a.PkgNumber=b.Number and {0} order by a.Number desc", strSql);

                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                return sqlHelper.ExecuteDataset(sql, new SqlParameter("@storenum", storenum));
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                throw ex;
            }
        }

        public void UpdateStorePkgStatus(string storenum, int pkgNumber, bool isDownloaded, DateTime downloadDate, bool isUpdated, DateTime updatedDate)
        {
            try
            {
                string sqlStr = @"update RevUpdatePkgAllocation set IsDownload=@IsDownload, DownloadDate=@DownloadDate, IsUpdated=@IsUpdated, UpdatedDate=@UpdatedDate
                              where StoreNum=@StoreNum and PkgNumber=@PkgNumber";

                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.ExecuteNonQuery(sqlStr, new SqlParameter("@IsDownload", isDownloaded ? "1" : "0"),
                                           new SqlParameter("@DownloadDate", downloadDate),
                                           new SqlParameter("@IsUpdated", isUpdated ? "1" : "0"),
                                           new SqlParameter("@UpdatedDate", updatedDate),
                                           new SqlParameter("@StoreNum", storenum),
                                           new SqlParameter("@PkgNumber", pkgNumber));
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                throw ex;
            }
        }

        public void WriteUpdatePkgLog(string storenum, int pkgNumber, int downloadStatus, DateTime downloadDate, int updateStatus, DateTime updateDate, string remark)
        {
            try
            {
                string sqlStr = @"insert into RevUpdatePkgAllocationLog(storenum, pkgnumber, downloadstatus, downloaddate, updatestatus, updatedate,remark)
                               values(@storenum, @pkgnumber, @downloadstatus, @downloaddate, @updatestatus, @updatedate,@remark)";

                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.ExecuteNonQuery(sqlStr,
                                          new SqlParameter("@storenum", storenum),
                                          new SqlParameter("@pkgnumber", pkgNumber),
                                          new SqlParameter("@downloadstatus", downloadStatus),
                                          new SqlParameter("@downloaddate", downloadDate),
                                          new SqlParameter("@updatestatus", updateStatus),
                                          new SqlParameter("@updatedate", updateDate),
                                          new SqlParameter("@remark", remark));
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                throw ex;
            }
        }

        public void StoreOperateLog(string ienName, string storenum, DateTime effectiveDate, int groupNumber, string stepId, int status, DateTime createTime, string remark)
        {
            DataTable dt = new DataTable();
            dt.TableName = "storeoperationlog";
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["HQ"].ToString();

            SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
            DataSet columnNameDS = sqlHelper.ExecuteDataset(string.Format("select name from syscolumns where id=(select max(id) from sysobjects where xtype='u' and name=N'{0}') order by colid", dt.TableName));
            if (columnNameDS != null && columnNameDS.Tables.Count > 0 && columnNameDS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in columnNameDS.Tables[0].Rows)
                {
                    dt.Columns.Add(dr[0].ToString());
                }
            }
            else
            {
                string msgStr = string.Format("查询表{0}的列名发生错误", dt.TableName);
                _log.Info(msgStr);
                throw new Exception(msgStr);
            }

            if ("".Equals(ienName))
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(new SqlParameter("@storenum", storenum));
                pars.Add(new SqlParameter("@effectiveDate", effectiveDate));

                DataSet ds = sqlHelper.ExecuteDataset("pro_GetIenNameStateForUpdToLocal", CommandType.StoredProcedure, pars.ToArray());
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataRow dr;
                    foreach (DataRow drSub in ds.Tables[0].Rows)
                    {
                        dr = dt.NewRow();
                        dr["ienName"] = drSub[0];
                        dr["status"] = status == 0 ? status : drSub[1];
                        dr["storenum"] = storenum;
                        if (effectiveDate != DateTime.MinValue)
                        {
                            dr["effective_Date"] = effectiveDate;
                        }
                        dr["groupNumber"] = groupNumber;
                        dr["step_id"] = stepId;
                        dr["operatedate"] = createTime;
                        dr["callTime"] = DateTime.Now;
                        dr["remark"] = remark;
                        dt.Rows.Add(dr);
                    }
                }
            }
            else
            {
                DataRow dr;
                dr = dt.NewRow();
                dr["ienName"] = ienName;
                dr["status"] = status;
                dr["storenum"] = storenum;
                if (effectiveDate != DateTime.MinValue)
                {
                    dr["effective_Date"] = effectiveDate;
                }
                dr["groupNumber"] = groupNumber;
                dr["step_id"] = stepId;
                dr["operatedate"] = createTime;
                dr["callTime"] = DateTime.Now;
                dr["remark"] = remark;
                dt.Rows.Add(dr);
            }
            UpdateStoreOperateLog(connStr, dt);
        }

        private void UpdateStoreOperateLog(string connStr, DataTable dt)
        {
            if (dt == null || dt.Rows.Count < 1)
            {
                return;
            }
            try
            {
                using (SqlConnection destinationConnection =
                               new SqlConnection(connStr))
                {
                    destinationConnection.Open();

                    using (SqlTransaction transaction =
                               destinationConnection.BeginTransaction())
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(
                                   destinationConnection, SqlBulkCopyOptions.Default,
                                   transaction))
                        {
                            bulkCopy.BatchSize = 10;

                            try
                            {
                                //先删除 ienName 已经存在的
                                string delConfigStr = @"delete from storeoperationlog where ienName = '{0}'
                                    and step_id ='{1}' and storenum = '{2}'";
                                StringBuilder delConfigSB = new StringBuilder();
                                foreach (DataRow dr in dt.Rows)
                                {
                                    delConfigSB.Append(string.Format("\r\n" + delConfigStr, dr["ienName"], dr["step_id"], dr["storenum"]));
                                }
                                if (delConfigSB.Length > 1)
                                {
                                    SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                                    sqlHelper.ExecuteNonQuery(delConfigSB.ToString());
                                }

                                bulkCopy.DestinationTableName = dt.TableName;
                                bulkCopy.WriteToServer(dt);
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _log.Fatal(ex.Message);
                                throw ex;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                throw ex;
            }
        }

        public void UploadStoresData(byte[] storeData)
        {
            byte[] normalstoreData = CompressionHelper.Decompress(storeData);
            _log.Info("上传门店数据:" + storeData.Length + "|" + normalstoreData.Length);

            //获取用户传来的DataSet序列化 并反序列化
            MemoryStream ms = new MemoryStream(normalstoreData);
            BinaryFormatter bf = new BinaryFormatter();
            Object o;
            try
            {
                o = bf.Deserialize(ms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            DataSetSurrogate.DataSetSurrogate dss = (DataSetSurrogate.DataSetSurrogate)o;
            DataSet dsStoreData = dss.ConvertToDataSet();
            _log.Info("上传门店数据:" + dsStoreData.Tables[0].Rows.Count + "|" + dsStoreData.Tables[1].Rows.Count);


            using (SqlConnection destinationConnection =
                           new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HQ"].ToString()))
            {
                destinationConnection.Open();

                using (SqlTransaction transaction =
                           destinationConnection.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(
                               destinationConnection, SqlBulkCopyOptions.FireTriggers,
                               transaction))
                    {
                        bulkCopy.BatchSize = 10;

                        try
                        {
                            bulkCopy.DestinationTableName = "dbo.strgroup";
                            bulkCopy.WriteToServer(dsStoreData.Tables[1]);

                            bulkCopy.DestinationTableName = "dbo.storeno";
                            bulkCopy.WriteToServer(dsStoreData.Tables[0]);

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _log.Fatal(ex.Message);
                            throw ex;
                        }
                    }
                }
            }
        }

        public string HelloWorld()
        {

            return "Hello World";
        }
        private bool checkuser(string userId, string password)
        {
            bool result = false;
            if (userId.ToUpper() == "Ajisen".ToUpper() && password == "8888")
                result = true;
            return result;
        }

        public int UploadWXSalesData(string UniqueCode, string UserId, string PassWord, List<StoreChecksDate> ChecksData)
        {
            _log.Info("上传味千销售数据：UniqueCode:" + UniqueCode + "|UserId:" + UserId + "|PassWord:" + PassWord);
            if (!checkuser(UserId, PassWord))
            {
                _log.Info("用户名密码错误");
                return 2;//账号密码错误
            }

            int retReturn = -1;

            StringBuilder sqlStr = new StringBuilder(" SELECT count(1) FROM WXSalesData WHERE uniqueCode=@uniqueCode");

            SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
            int count = (int)sqlHelper.ExecuteScalar(sqlStr.ToString(), new SqlParameter("@uniqueCode", UniqueCode));
            if (count > 0)
            {
                _log.Info("数据已存在");
                return 99;//数据库已存在
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO WXSalesData(uniqueCode,userId,[passWord])");
            sb.AppendLine("VALUES(@uniqueCode,@userId,@password);");

            //添加参数
            List<SqlParameter> listPara = new List<SqlParameter>();
            listPara.Add(new SqlParameter("@uniqueCode", UniqueCode));
            listPara.Add(new SqlParameter("@userId", UserId));
            listPara.Add(new SqlParameter("@password", PassWord));

            sqlHelper.BeginTran();
            int num = (int)ChecksData.Count / 17;//被17整除的次数
            for (int i = 0; i < ChecksData.Count; i++)
            {
                sb.AppendLine("INSERT INTO WXStoreChecksDate(StoreCode,[Date],CheckCount,CustomerTotal,ChecksTotal,WXSalesDataUniqueCode)");
                sb.AppendLine("VALUES(@StoreCode" + i + ",@Date" + i + ",@CheckCount" + i + ",@CustomerTotal" + i + ",@ChecksTotal" + i + ",@WXSalesDataUniqueCode" + i + ");");

                listPara.Add(new SqlParameter("@StoreCode" + i, ChecksData[i].StoreCode));
                listPara.Add(new SqlParameter("@Date" + i, ChecksData[i].Date));
                listPara.Add(new SqlParameter("@CheckCount" + i, ChecksData[i].CheckCount));
                listPara.Add(new SqlParameter("@CustomerTotal" + i, ChecksData[i].CustomerTotal));
                listPara.Add(new SqlParameter("@ChecksTotal" + i, ChecksData[i].ChecksTotal));
                listPara.Add(new SqlParameter("@WXSalesDataUniqueCode" + i, ChecksData[i].WXSalesDataUniqueCode));

                if (((i + 1) % 17 == 0) || (i == ChecksData.Count - 1))//记录条数为17的倍数或最后一条
                {
                    SqlParameter[] parameters = new SqlParameter[listPara.Count];
                    for (int k = 0; k < listPara.Count; k++)
                    {
                        parameters[k] = listPara[k];
                    }
                    try
                    {
                        int executeNum = sqlHelper.ExecuteNonQuery(sb.ToString(), parameters);
                    }
                    catch (Exception ex)
                    {
                        _log.Fatal(ex.Message + "问题出现在第" + i + "次,时间为:" + DateTime.Now.ToString());
                        //大于0的错误现在指定为1，指数据库连接错误，或语句错误
                        sqlHelper.RollBackTran();
                        retReturn = 1;

                        return retReturn;
                    }
                    sb = new StringBuilder();
                    listPara = new List<SqlParameter>();
                }
            }
            sqlHelper.CommitTran();
            _log.Info("数据导入成功");
            return retReturn;
        }
        #region 通用同步写法 shaka
        public string[] GetColumns(string tableName)
        {
            List<string> colList = new List<string>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["HQ"].ToString();

            SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
            DataSet columnNameDS = sqlHelper.ExecuteDataset(string.Format("select name from syscolumns where id=(select max(id) from sysobjects where xtype='u' and name=N'{0}') order by colid", tableName));
            if (columnNameDS != null && columnNameDS.Tables.Count > 0 && columnNameDS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in columnNameDS.Tables[0].Rows)
                {
                    colList.Add(dr[0].ToString());
                }
            }
            else
            {
                string msgStr = string.Format("查询表{0}的列名发生错误", tableName);
                _log.Info(msgStr);
                throw new Exception(msgStr);
            }
            return colList.ToArray();
        }

        public void DataSync(byte[] syncData)
        {
            byte[] normalstoreData = CompressionHelper.Decompress(syncData);
            _log.Info("同步数据:" + syncData.Length + "|" + normalstoreData.Length);

            //获取用户传来的DataSet序列化 并反序列化
            MemoryStream ms = new MemoryStream(normalstoreData);
            BinaryFormatter bf = new BinaryFormatter();
            Object o;
            try
            {
                o = bf.Deserialize(ms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            DataSetSurrogate.DataSetSurrogate dss = (DataSetSurrogate.DataSetSurrogate)o;
            DataSet dsStoreData = dss.ConvertToDataSet();

            //using (SqlConnection destinationConnection =
            //               new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HQ"].ToString()))
            //{
            //    destinationConnection.Open();

            //    using (SqlTransaction transaction =
            //               destinationConnection.BeginTransaction())
            //    {
            //        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(
            //                   destinationConnection, SqlBulkCopyOptions.FireTriggers,
            //                   transaction))
            //        {
            //            bulkCopy.BatchSize = 10;

            //            try
            //            {
            //                foreach (DataTable dt in dsStoreData.Tables)
            //                {
            //                    bulkCopy.DestinationTableName = dt.TableName;
            //                    bulkCopy.WriteToServer(dt);
            //                }
            //                transaction.Commit();
            //            }
            //            catch (Exception ex)
            //            {
            //                transaction.Rollback();
            //                LogHelper.WriteLogFile(ex.Message);
            //                throw ex;
            //            }
            //        }
            //    }
            //}

            //用存在就Update代替上面纯粹新增 2017.01.05 shaka
            WCFDataSyncBLL _wCFDataSyncBLL = new WCFDataSyncBLL();
            _wCFDataSyncBLL.DataSync(dsStoreData);
        }
        #endregion

        #region // 味千HQ菜品接口
        /// <summary>
        /// 菜品接口
        /// </summary>
        /// <param name="UserId">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="CPInfoList">菜品数据列表</param>
        /// <returns></returns>
        public int UploadCPInfo(string UserId, string PassWord, List<CPInfo> CPInfoList)
        {
            if (!checkuser(UserId, PassWord))
            {
                return 2;//账号密码错误
            }

            int retReturn = -1;
            try
            {
                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.BeginTran();
                try
                {
                    if (CPInfoList.Count > 0)
                    {
                        foreach (CPInfo item in CPInfoList)
                        {
                            string sqlStr = string.Format(@"SELECT * FROM WQCP WHERE LINTITEMNUMBER={0}", item.LINTITEMNUMBER);

                            DataTable dt = sqlHelper.ExecuteDataset(sqlStr).Tables[0];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string sqlUpdate = string.Format(@"UPDATE WQCP SET [STRPRODUCTDESCRIPTION]=N'{0}',[STRDEPTCODE]=N'{1}',[STRCATGORY]=N'{2}',[STRYSNACTIVE]=N'{3}',
                                [STRYSNSETMEAL]=N'{4}',[STRYSNFLAVOR]=N'{5}',[STRITEMTYPE]=N'{6}',[Date]='{7}' WHERE LINTITEMNUMBER={8}",
                                    item.STRPRODUCTDESCRIPTION, item.STRDEPTCODE, item.STRCATGORY, item.STRYSNACTIVE, item.STRYSNSETMEAL, item.STRYSNFLAVOR,
                                    item.STRITEMTYPE, DateTime.Now, item.LINTITEMNUMBER);
                                sqlHelper.ExecuteNonQuery(sqlUpdate);
                            }
                            else
                            {
                                string sqlInsert = string.Format(@"INSERT INTO WQCP([LINTITEMNUMBER],[STRPRODUCTDESCRIPTION],[STRDEPTCODE],[STRCATGORY],[STRYSNACTIVE],[STRYSNSETMEAL],[STRYSNFLAVOR],[STRITEMTYPE],[Date]) 
                                VALUES({0},N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}','{8}')", item.LINTITEMNUMBER, item.STRPRODUCTDESCRIPTION, item.STRDEPTCODE, item.STRCATGORY,
                                    item.STRYSNACTIVE, item.STRYSNSETMEAL, item.STRYSNFLAVOR, item.STRITEMTYPE, DateTime.Now);
                                sqlHelper.ExecuteNonQuery(sqlInsert);
                            }
                        }
                        sqlHelper.CommitTran();
                        _log.Info("数据导入成功");
                    }
                }
                catch (Exception ex)
                {
                    sqlHelper.RollBackTran();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                retReturn = 1;
            }
            return retReturn;
        }

        /// <summary>
        /// 做法接口
        /// </summary>
        /// <param name="UserId">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="ZFInfoList">做法数据列表</param>
        /// <returns></returns>
        public int UploadZFInfo(string UserId, string PassWord, List<ZFInfo> ZFInfoList)
        {
            if (!checkuser(UserId, PassWord))
            {
                return 2;//账号密码错误
            }

            int retReturn = -1;
            try
            {
                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.BeginTran();
                try
                {
                    if (ZFInfoList.Count > 0)
                    {
                        foreach (ZFInfo item in ZFInfoList)
                        {
                            string sqlStr = string.Format(@"SELECT * FROM WQZF WHERE STRTRADECODE=N'{0}' AND LINTMODIFIERCODE={1} AND [CTRCODE]={2}",
                                item.STRTRADECODE, item.LINTMODIFIERCODE, item.CTRCODE);

                            DataTable dt = sqlHelper.ExecuteDataset(sqlStr).Tables[0];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string sqlUpdate = string.Format(@"UPDATE WQZF SET [STRDESCRIPTION]=N'{0}',[LINTITEMNUMBER]={1},[YSNACTIVE]=N'{2}',[Date]='{3}'   
                                WHERE STRTRADECODE=N'{4}' AND LINTMODIFIERCODE={5} AND [CTRCODE]={6}",
                                    item.STRDESCRIPTION, item.LINTITEMNUMBER, item.YSNACTIVE, DateTime.Now, item.STRTRADECODE, item.LINTMODIFIERCODE, item.CTRCODE);
                                sqlHelper.ExecuteNonQuery(sqlUpdate);
                            }
                            else
                            {
                                string sqlInsert = string.Format(@"INSERT INTO WQZF([STRTRADECODE],[LINTMODIFIERCODE],[CTRCODE],[STRDESCRIPTION],[LINTITEMNUMBER],[YSNACTIVE],[Date]) 
                                VALUES(N'{0}',{1},{2},N'{3}',{4},N'{5}','{6}')",
                                    item.STRTRADECODE, item.LINTMODIFIERCODE, item.CTRCODE, item.STRDESCRIPTION, item.LINTITEMNUMBER, item.YSNACTIVE, DateTime.Now);
                                sqlHelper.ExecuteNonQuery(sqlInsert);
                            }
                        }
                        sqlHelper.CommitTran();
                        _log.Info("数据导入成功");
                    }
                }
                catch (Exception ex)
                {
                    sqlHelper.RollBackTran();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                retReturn = 1;
            }
            return retReturn;
        }

        /// <summary>
        /// 套餐明细接口
        /// </summary>
        /// <param name="UserId">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="TCMXInfoList">套餐明细数据列表</param>
        /// <returns></returns>
        public int UploadTCMXInfo(string UserId, string PassWord, List<TCMXInfo> TCMXInfoList)
        {
            if (!checkuser(UserId, PassWord))
            {
                return 2;//账号密码错误
            }

            int retReturn = -1;
            try
            {
                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.BeginTran();
                try
                {
                    if (TCMXInfoList.Count > 0)
                    {
                        string sqlStr = string.Format(@"DELETE FROM WQTCMX");

                        sqlHelper.ExecuteNonQuery(sqlStr);

                        foreach (TCMXInfo item in TCMXInfoList)
                        {
                            string sqlInsert = string.Format(@"INSERT INTO WQTCMX([LINTITEMNUMBER],[CTRCODE],[INTSEQUENCE],[LINTINGREDIENTITEM],[DBLQTY],[Date]) VALUES({0},{1},{2},{3},{4},'{5}')",
                                item.LINTITEMNUMBER, item.CTRCODE, item.INTSEQUENCE, item.LINTINGREDIENTITEM, item.DBLQTY, DateTime.Now);
                            sqlHelper.ExecuteNonQuery(sqlInsert);
                        }
                        sqlHelper.CommitTran();
                        _log.Info("数据导入成功");
                    }
                }
                catch (Exception ex)
                {
                    sqlHelper.RollBackTran();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                retReturn = 1;
            }
            return retReturn;
        }

        /// <summary>
        /// 套餐替换明细接口
        /// </summary>
        /// <param name="UserId">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="TCTHMXInfoList">套餐替换明细数据列表</param>
        /// <returns></returns>
        public int UploadTCTHMXInfo(string UserId, string PassWord, List<TCTHMXInfo> TCTHMXInfoList)
        {
            if (!checkuser(UserId, PassWord))
            {
                return 2;//账号密码错误
            }

            int retReturn = -1;
            try
            {
                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.BeginTran();
                try
                {
                    if (TCTHMXInfoList.Count > 0)
                    {
                        string sqlStr = string.Format(@"DELETE FROM WQTCTHMX");

                        sqlHelper.ExecuteNonQuery(sqlStr);

                        foreach (TCTHMXInfo item in TCTHMXInfoList)
                        {
                            string sqlInsert = string.Format(@"INSERT INTO WQTCTHMX([LINTITEMNUMBER],[LINTINGREDIENTITEM],[CTRCODE],[INTSEQUENCE],[LINTREPLACEITEM],[Date]) VALUES({0},{1},{2},{3},{4},'{5}')",
                                item.LINTITEMNUMBER, item.LINTINGREDIENTITEM, item.CTRCODE, item.INTSEQUENCE, item.LINTREPLACEITEM, DateTime.Now);
                            sqlHelper.ExecuteNonQuery(sqlInsert);
                        }
                        sqlHelper.CommitTran();
                        _log.Info("数据导入成功");
                    }
                }
                catch (Exception ex)
                {
                    sqlHelper.RollBackTran();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                retReturn = 1;
            }
            return retReturn;
        }

        /// <summary>
        /// 价格接口
        /// </summary>
        /// <param name="UserId">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="PriceInfoList">价格数据列表</param>
        /// <returns></returns>
        public int UploadPriceInfo(string UserId, string PassWord, List<PriceInfo> PriceInfoList)
        {
            if (!checkuser(UserId, PassWord))
            {
                return 2;//账号密码错误
            }

            int retReturn = -1;
            try
            {
                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.BeginTran();
                try
                {
                    if (PriceInfoList.Count > 0)
                    {
                        foreach (PriceInfo item in PriceInfoList)
                        {
                            string sqlStr = string.Format(@"SELECT * FROM WQPRICE WHERE STRSTORECODE=N'{0}' AND LINTITEMNUMBER={1}", item.STRSTORECODE, item.LINTITEMNUMBER);

                            DataTable dt = sqlHelper.ExecuteDataset(sqlStr).Tables[0];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string sqlUpdate = string.Format(@"UPDATE WQPRICE SET [CURSELL]={0},[YSNACTIVE]=N'{1}',[Date]='{2}' WHERE STRSTORECODE=N'{3}' AND LINTITEMNUMBER={4}",
                                    item.CURSELL, item.YSNACTIVE, DateTime.Now, item.STRSTORECODE, item.LINTITEMNUMBER);
                                sqlHelper.ExecuteNonQuery(sqlUpdate);
                            }
                            else
                            {
                                string sqlInsert = string.Format(@"INSERT INTO WQPRICE([STRSTORECODE],[LINTITEMNUMBER],[CURSELL],[YSNACTIVE],[Date]) VALUES(N'{0}',{1},{2},N'{3}','{4}')",
                                    item.STRSTORECODE, item.LINTITEMNUMBER, item.CURSELL, item.YSNACTIVE, DateTime.Now);
                                sqlHelper.ExecuteNonQuery(sqlInsert);
                            }
                        }
                        sqlHelper.CommitTran();
                        _log.Info("数据导入成功");
                    }
                }
                catch (Exception ex)
                {
                    sqlHelper.RollBackTran();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                retReturn = 1;
            }
            return retReturn;
        }

        /// <summary>
        /// 品牌接口
        /// </summary>
        /// <param name="UserId">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="BrandInfoList">品牌数据列表</param>
        /// <returns></returns>
        public int UploadBrandInfo(string UserId, string PassWord, List<BrandInfo> BrandInfoList)
        {
            if (!checkuser(UserId, PassWord))
            {
                return 2;//账号密码错误
            }

            int retReturn = -1;
            try
            {
                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.BeginTran();
                try
                {
                    if (BrandInfoList.Count > 0)
                    {
                        foreach (BrandInfo item in BrandInfoList)
                        {
                            string sqlStr = string.Format(@"SELECT * FROM [dbo].[WQBRAND] WHERE STRDEPTCODE=N'{0}' ", item.STRDEPTCODE);

                            DataTable dt = sqlHelper.ExecuteDataset(sqlStr).Tables[0];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string sqlUpdate = string.Format(@"UPDATE [dbo].[WQBRAND] SET [STRDEPTDESCRIPTION]=N'{0}',[YSNACTIVE]=N'{1}',[Date]='{2}' WHERE STRDEPTCODE=N'{3}'",
                                    item.STRDEPTDESCRIPTION, item.YSNACTIVE, DateTime.Now, item.STRDEPTCODE);
                                sqlHelper.ExecuteNonQuery(sqlUpdate);
                            }
                            else
                            {
                                string sqlInsert = string.Format(@"INSERT INTO [dbo].[WQBRAND]([STRDEPTCODE],[STRDEPTDESCRIPTION],[YSNACTIVE],[Date]) VALUES('{0}',N'{1}',N'{2}','{3}')",
                                    item.STRDEPTCODE, item.STRDEPTDESCRIPTION, item.YSNACTIVE, DateTime.Now);
                                sqlHelper.ExecuteNonQuery(sqlInsert);
                            }
                        }
                        sqlHelper.CommitTran();
                        _log.Info("数据导入成功");
                    }
                }
                catch (Exception ex)
                {
                    sqlHelper.RollBackTran();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                retReturn = 1;
            }
            return retReturn;
        }

        /// <summary>
        /// 分类接口
        /// </summary>
        /// <param name="UserId">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="ClassInfoList">分类数据列表</param>
        /// <returns></returns>
        public int UploadClassInfo(string UserId, string PassWord, List<ClassInfo> ClassInfoList)
        {
            if (!checkuser(UserId, PassWord))
            {
                return 2;//账号密码错误
            }

            int retReturn = -1;
            try
            {
                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.BeginTran();
                try
                {
                    if (ClassInfoList.Count > 0)
                    {
                        foreach (ClassInfo item in ClassInfoList)
                        {
                            string sqlStr = string.Format(@"SELECT * FROM WQClass WHERE STRCATCODE=N'{0}'", item.STRCATCODE);

                            DataTable dt = sqlHelper.ExecuteDataset(sqlStr).Tables[0];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string sqlUpdate = string.Format(@"UPDATE WQClass SET [STRCATDESCRIPTION]=N'{0}',[YSNACTIVE]=N'{1}',[Date]='{2}' WHERE STRCATCODE=N'{3}'",
                                    item.STRCATDESCRIPTION, item.YSNACTIVE, DateTime.Now, item.STRCATCODE);
                                sqlHelper.ExecuteNonQuery(sqlUpdate);
                            }
                            else
                            {
                                string sqlInsert = string.Format(@"INSERT INTO WQClass([STRCATCODE],[STRCATDESCRIPTION],[YSNACTIVE],[Date]) VALUES(N'{0}',N'{1}',N'{2}','{3}')",
                                    item.STRCATCODE, item.STRCATDESCRIPTION, item.YSNACTIVE, DateTime.Now);
                                sqlHelper.ExecuteNonQuery(sqlInsert);
                            }
                        }
                        sqlHelper.CommitTran();
                        _log.Info("数据导入成功");
                    }
                }
                catch (Exception ex)
                {
                    sqlHelper.RollBackTran();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                retReturn = 1;
            }
            return retReturn;
        }



        /// <summary>
        /// 门店标准菜明细接口
        /// </summary>
        /// <param name="UserId">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="ClassInfoList">标准菜明细数据列表</param>
        /// <returns></returns>
        public int UploadCPDetailInfo(string UserId, string PassWord, List<CPDetailInfo> CPDetailInfoList)
        {
            if (!checkuser(UserId, PassWord))
            {
                return 2;//账号密码错误
            }

            int retReturn = -1;
            try
            {
                SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
                sqlHelper.BeginTran();
                try
                {
                    if (CPDetailInfoList.Count > 0)
                    {
                        string sqlStr = string.Format(@"DELETE FROM WQCPDETAIL");

                        sqlHelper.ExecuteNonQuery(sqlStr);

                        foreach (CPDetailInfo item in CPDetailInfoList)
                        {
                            string sqlInsert = string.Format(@"INSERT INTO WQCPDETAIL([STRMENUGROUPID],[STRSTOREGROUPID],[STRTRADECODE],[LINTITEMNUMBER],[Date]) VALUES(N'{0}',N'{1}',N'{2}',{3},'{4}')",
                                item.STRMENUGROUPID, item.STRSTOREGROUPID, item.STRTRADECODE, item.LINTITEMNUMBER, DateTime.Now);
                            sqlHelper.ExecuteNonQuery(sqlInsert);
                        }
                        sqlHelper.CommitTran();
                        _log.Info("数据导入成功");
                    }
                }
                catch (Exception ex)
                {
                    sqlHelper.RollBackTran();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                retReturn = 1;
            }
            return retReturn;
        }
        #endregion

        #region 盼贷数据上传接口

        #region 盼贷用户注册信息数据上传接口(手机号码)
        /// <summary>
        /// 用户注册信息数据上传接口(手机号码)
        /// </summary>
        /// <param name="RequestUniqueCode">本次请求唯一编号，用于传输失败时重传数据</param>
        /// <param name="PandaiUniqueCode">盼贷内部用户信息唯一识别码，例如用户编号</param>
        /// <param name="REVUniqueCode">REV内部唯一识别码，由跳转链接时随请求提供（URL传值）</param>
        /// <param name="PhoneNumber">注册手机号码</param>
        /// <returns>-1成功   0丢包（由调用方去判断）  >1 失败</returns>
        public int PandaiRegister(string RequestUniqueCode, string PandaiUniqueCode, string REVUniqueCode, string PhoneNumber, string TimeStamp)
        {
            int retReturn = -1;
            StringBuilder SQL = new StringBuilder(" SELECT count(1) FROM PandaiRegister WITH (NOLOCK) WHERE RequestUniqueCode=@RequestUniqueCode");

            SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
            int count = (int)sqlHelper.ExecuteScalar(SQL.ToString(), new SqlParameter("@RequestUniqueCode", RequestUniqueCode));
            if (count > 0)
            {
                _log.Info("数据已存在");
                return 99;//数据库已存在
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO PandaiRegister(RequestUniqueCode,PandaiUniqueCode,REVUniqueCode,PhoneNumber,[TimeStamp])");
            sb.AppendLine("VALUES(@RequestUniqueCode,@PandaiUniqueCode,@REVUniqueCode,@PhoneNumber,@TimeStamp);");

            //添加参数
            List<SqlParameter> listPara = new List<SqlParameter>();
            listPara.Add(new SqlParameter("@RequestUniqueCode", RequestUniqueCode));
            listPara.Add(new SqlParameter("@PandaiUniqueCode", PandaiUniqueCode));
            listPara.Add(new SqlParameter("@REVUniqueCode", REVUniqueCode));
            listPara.Add(new SqlParameter("@PhoneNumber", PhoneNumber));
            listPara.Add(new SqlParameter("@TimeStamp", TimeStamp));
            //add 
            sqlHelper.BeginTran();
            SqlParameter[] parameters = new SqlParameter[listPara.Count];
            for (int k = 0; k < listPara.Count; k++)
            {
                parameters[k] = listPara[k];
            }
            try
            {
                int executeNum = sqlHelper.ExecuteNonQuery(sb.ToString(), parameters);
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message + "   时间为:" + DateTime.Now.ToString());
                //大于0的错误现在指定为1，指数据库连接错误，或语句错误
                sqlHelper.RollBackTran();
                retReturn = 1;

                return retReturn;
            }
            sqlHelper.CommitTran();
            _log.Info("数据导入成功");
            return retReturn;

        }
        #endregion

        #region 盼贷用户认证信息数据上传接口
        /// <summary>
        /// 盼贷用户认证信息数据上传接口
        /// </summary>
        /// <param name="RequestUniqueCode">本次请求唯一编号，用于传输失败时重传数据</param>
        /// <param name="PandaiUniqueCode">盼贷内部用户信息唯一识别码，例如用户编号</param>
        /// <param name="REVUniqueCode">REV内部唯一识别码，由跳转链接时随请求提供（URL传值）</param>
        /// <param name="Name">用户姓名</param>
        /// <param name="IDNumber">身份证编号</param>
        /// <returns>-1成功   0丢包（由调用方去判断）  >1 失败</returns>
        public int PandaiAuthentication(string RequestUniqueCode, string PandaiUniqueCode, string REVUniqueCode, string Name, string IDNumber, string TimeStamp)
        {
            int retReturn = -1;
            StringBuilder sqlStr = new StringBuilder(" SELECT count(1) FROM PandaiAuthentication WITH (NOLOCK) WHERE RequestUniqueCode=@RequestUniqueCode");

            SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
            int count = (int)sqlHelper.ExecuteScalar(sqlStr.ToString(), new SqlParameter("@RequestUniqueCode", RequestUniqueCode));
            if (count > 0)
            {
                _log.Info("数据已存在");
                return 99;//数据库已存在
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO PandaiAuthentication(RequestUniqueCode,PandaiUniqueCode,REVUniqueCode,Name,IDNumber,[TimeStamp])");
            sb.AppendLine("VALUES(@RequestUniqueCode,@PandaiUniqueCode,@REVUniqueCode,@Name,@IDNumber,@TimeStamp);");

            //添加参数
            List<SqlParameter> listPara = new List<SqlParameter>();
            listPara.Add(new SqlParameter("@RequestUniqueCode", RequestUniqueCode));
            listPara.Add(new SqlParameter("@PandaiUniqueCode", PandaiUniqueCode));
            listPara.Add(new SqlParameter("@REVUniqueCode", REVUniqueCode));
            listPara.Add(new SqlParameter("@Name", Name));
            listPara.Add(new SqlParameter("@IDNumber", IDNumber));
            listPara.Add(new SqlParameter("@TimeStamp", TimeStamp));
            //add 
            sqlHelper.BeginTran();

            SqlParameter[] parameters = new SqlParameter[listPara.Count];
            for (int k = 0; k < listPara.Count; k++)
            {
                parameters[k] = listPara[k];
            }
            try
            {
                int executeNum = sqlHelper.ExecuteNonQuery(sb.ToString(), parameters);
            }
            catch (Exception ex)
            {
                //大于0的错误现在指定为1，指数据库连接错误，或语句错误
                _log.Fatal(ex.Message + "   时间为:" + DateTime.Now.ToString());
                sqlHelper.RollBackTran();
                retReturn = 1;

                return retReturn;
            }
            sqlHelper.CommitTran();
            _log.Info("数据导入成功");
            return retReturn;

        }
        #endregion

        #region 盼贷用户绑定银行卡信息数据上传接口
        /// <summary>
        /// 盼贷用户绑定银行卡信息数据上传接口
        /// </summary>
        /// <param name="RequestUniqueCode">本次请求唯一编号，用于传输失败时重传数据</param>
        /// <param name="PandaiUniqueCode">盼贷内部用户信息唯一识别码，例如用户编号</param>
        /// <param name="REVUniqueCode">REV内部唯一识别码，由跳转链接时随请求提供（URL传值）</param>
        /// <param name="AccountNumber">银行卡号</param>
        /// <returns>-1成功   0丢包（由调用方去判断）  >1 失败</returns>
        public int PandaiBindingCard(string RequestUniqueCode, string PandaiUniqueCode, string REVUniqueCode, string AccountNumber, string TimeStamp)
        {
            int retReturn = -1;
            StringBuilder sqlStr = new StringBuilder(" SELECT count(1) FROM PandaiBindingCard WITH (NOLOCK) WHERE RequestUniqueCode=@RequestUniqueCode");

            SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
            int count = (int)sqlHelper.ExecuteScalar(sqlStr.ToString(), new SqlParameter("@RequestUniqueCode", RequestUniqueCode));
            if (count > 0)
            {
                _log.Info("数据已存在");
                return 99;//数据库已存在
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO PandaiBindingCard(RequestUniqueCode,PandaiUniqueCode,REVUniqueCode,AccountNumber,[TimeStamp])");
            sb.AppendLine("VALUES(@RequestUniqueCode,@PandaiUniqueCode,@REVUniqueCode,@AccountNumber,@TimeStamp);");

            //添加参数
            List<SqlParameter> listPara = new List<SqlParameter>();
            listPara.Add(new SqlParameter("@RequestUniqueCode", RequestUniqueCode));
            listPara.Add(new SqlParameter("@PandaiUniqueCode", PandaiUniqueCode));
            listPara.Add(new SqlParameter("@REVUniqueCode", REVUniqueCode));
            listPara.Add(new SqlParameter("@AccountNumber", AccountNumber));
            listPara.Add(new SqlParameter("@TimeStamp", TimeStamp));
            //add 
            sqlHelper.BeginTran();

            SqlParameter[] parameters = new SqlParameter[listPara.Count];
            for (int k = 0; k < listPara.Count; k++)
            {
                parameters[k] = listPara[k];
            }
            try
            {
                _log.Info(sb.ToString());

                int executeNum = sqlHelper.ExecuteNonQuery(sb.ToString(), parameters);
            }
            catch (Exception ex)
            {
                //大于0的错误现在指定为1，指数据库连接错误，或语句错误
                _log.Fatal(ex.Message + "   时间为:" + DateTime.Now.ToString());
                sqlHelper.RollBackTran();
                retReturn = 1;

                return retReturn;
            }
            sqlHelper.CommitTran();
            _log.Info("数据导入成功");
            return retReturn;

        }
        #endregion

        #region 盼贷用户首次注资信息数据上传接口
        /// <summary>
        /// 盼贷用户首次注资信息数据上传接口
        /// </summary>
        /// <param name="RequestUniqueCode">本次请求唯一编号，用于传输失败时重传数据</param>
        /// <param name="PandaiUniqueCode">盼贷内部用户信息唯一识别码，例如用户编号</param>
        /// <param name="REVUniqueCode">REV内部唯一识别码，由跳转链接时随请求提供（URL传值）</param>
        /// <param name="ProductName">产品名称（第一次购买）</param>
        /// <param name="AMOUNT">投资金额（第一次购买）</param>
        /// <returns>-1成功   0丢包（由调用方去判断）  >1 失败</returns>
        public int PandaiInvestment(string RequestUniqueCode, string PandaiUniqueCode, string REVUniqueCode, string ProductName, string AMOUNT, string TimeStamp)
        {
            int retReturn = -1;
            StringBuilder sqlStr = new StringBuilder(" SELECT count(1) FROM PandaiInvestment WITH (NOLOCK) WHERE RequestUniqueCode=@RequestUniqueCode");

            SqlDBHelper sqlHelper = new SqlDBHelper(_connStr);
            int count = (int)sqlHelper.ExecuteScalar(sqlStr.ToString(), new SqlParameter("@RequestUniqueCode", RequestUniqueCode));
            if (count > 0)
            {
                _log.Info("数据已存在");
                return 99;//数据库已存在
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO PandaiInvestment(RequestUniqueCode,PandaiUniqueCode,REVUniqueCode,ProductName,AMOUNT,[TimeStamp])");
            sb.AppendLine("VALUES(@RequestUniqueCode,@PandaiUniqueCode,@REVUniqueCode,@ProductName,@AMOUNT,@TimeStamp);");

            //添加参数
            List<SqlParameter> listPara = new List<SqlParameter>();
            listPara.Add(new SqlParameter("@RequestUniqueCode", RequestUniqueCode));
            listPara.Add(new SqlParameter("@PandaiUniqueCode", PandaiUniqueCode));
            listPara.Add(new SqlParameter("@REVUniqueCode", REVUniqueCode));
            listPara.Add(new SqlParameter("@ProductName", ProductName));
            listPara.Add(new SqlParameter("@AMOUNT", AMOUNT));
            listPara.Add(new SqlParameter("@TimeStamp", TimeStamp));
            //add 
            sqlHelper.BeginTran();

            SqlParameter[] parameters = new SqlParameter[listPara.Count];
            for (int k = 0; k < listPara.Count; k++)
            {
                parameters[k] = listPara[k];
            }
            try
            {
                int executeNum = sqlHelper.ExecuteNonQuery(sb.ToString(), parameters);
            }
            catch (Exception ex)
            {
                //大于0的错误现在指定为1，指数据库连接错误，或语句错误
                _log.Fatal(ex.Message + "   时间为:" + DateTime.Now.ToString());
                sqlHelper.RollBackTran();
                retReturn = 1;

                return retReturn;
            }
            sqlHelper.CommitTran();
            _log.Info("数据导入成功");
            return retReturn;

        }
        #endregion
        #endregion
    }
}