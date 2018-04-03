using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;

using Cuscapi.Model;

namespace Transight.HQV4.HQService.Contracts
{
    [ServiceContract]//(Name = "CalculatorService", Namespace = "http://www.artech.com/")]
    public interface IJsonOperation
    {
        [OperationContract]
        string Test(UserInfo user);

        [OperationContract]
        void WriteFileServerLog(string storenum, string fileNumber);

        [OperationContract]
        void WriteFileServerLogs(byte[] fileInfos);

        [OperationContract]
        void UploadSalesData(byte[] salesData, string storeNo, int value);

        [OperationContract]
        bool HasAnyNewUpdate(string storenum);

        [OperationContract]
        DataSet GetUpdatePkgInfo(string storenum, string isUpdate);

        [OperationContract]
        void UpdateStorePkgStatus(string storenum, int pkgNumber, bool isDownloaded, DateTime downloadDate, bool isUpdated, DateTime updatedDate);

        [OperationContract]
        void WriteUpdatePkgLog(string storenum, int pkgNumber, int downloadStatus, DateTime downloadDate, int updateStatus, DateTime updateDate, string remark);

        [OperationContract]
        void StoreOperateLog(string ienName, string storenum, DateTime effectiveDate, int groupNumber, string stepId, int status, DateTime createTime, string remark);

        [OperationContract]
        void UploadStoresData(byte[] storeData);

        [OperationContract]
        string HelloWorld();

        [OperationContract]
        int UploadWXSalesData(string UniqueCode, string UserId, string PassWord, List<StoreChecksDate> ChecksData);

        [OperationContract]
        string[] GetColumns(string tableName);

        [OperationContract]
        void DataSync(byte[] syncData);

        [OperationContract]
        /// <returns></returns>
        int UploadCPInfo(string UserId, string PassWord, List<CPInfo> CPInfoList);

        [OperationContract]
        int UploadZFInfo(string UserId, string PassWord, List<ZFInfo> ZFInfoList);

        [OperationContract]
        int UploadTCMXInfo(string UserId, string PassWord, List<TCMXInfo> TCMXInfoList);

        [OperationContract]
        int UploadTCTHMXInfo(string UserId, string PassWord, List<TCTHMXInfo> TCTHMXInfoList);

        [OperationContract]
        int UploadPriceInfo(string UserId, string PassWord, List<PriceInfo> PriceInfoList);

        [OperationContract]
        int UploadBrandInfo(string UserId, string PassWord, List<BrandInfo> BrandInfoList);

        [OperationContract]
        int UploadClassInfo(string UserId, string PassWord, List<ClassInfo> ClassInfoList);

        [OperationContract]
        int UploadCPDetailInfo(string UserId, string PassWord, List<CPDetailInfo> CPDetailInfoList);

        [OperationContract]
        int PandaiRegister(string RequestUniqueCode, string PandaiUniqueCode, string REVUniqueCode, string PhoneNumber, string TimeStamp);

        [OperationContract]
        int PandaiAuthentication(string RequestUniqueCode, string PandaiUniqueCode, string REVUniqueCode, string Name, string IDNumber, string TimeStamp);

        [OperationContract]
        int PandaiBindingCard(string RequestUniqueCode, string PandaiUniqueCode, string REVUniqueCode, string AccountNumber, string TimeStamp);

        [OperationContract]
        int PandaiInvestment(string RequestUniqueCode, string PandaiUniqueCode, string REVUniqueCode, string ProductName, string AMOUNT, string TimeStamp);
    }

}