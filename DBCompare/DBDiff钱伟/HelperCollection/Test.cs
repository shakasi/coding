using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.HelperCollection.DataAccess;

namespace Utility.HelperCollection
{
    public class Test
    {
        static void Main(string[] args)
        {

           Stopwatch watch = new Stopwatch();
           string strconn = "Data Source=(local);Integrated Security=SSPI;Initial Catalog=ShareLocation";

           //string oraclestrconn = "User ID=system;Data Source=orcl;Password=123asd";
           //DBHelperBase<T_User> helper = new DBHelperBase<T_User>(DataBaseType.Oracle, oraclestrconn);
           //oracleHelper.SelectAll(false);


            //dataadapt 批量更新速度慢
            //DataTable dt=new DataTable ();
            //SqlConnection conn = new SqlConnection(strconn);
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = new SqlCommand("select * from T_USERS", conn);
            //da.Fill(dt);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["DisplayName"] = 1;
            //}
            //SqlCommand updateCommand = new SqlCommand();
            //updateCommand.CommandText = "Update T_USERS set DisplayName=@DisplayName where id=@id";
            //updateCommand.Connection = conn;
            //SqlParameter param = new SqlParameter();
            //param.ParameterName = "@DisplayName";
            //param.SourceColumn = "DisplayName";
            //updateCommand.Parameters.Add(param);
            //param = new SqlParameter();
            //param.ParameterName = "@id";
            //param.SourceColumn = "id";
            //updateCommand.Parameters.Add(param);
            //da.UpdateCommand = updateCommand;
            //watch.Start();
            //int i=  da.Update(dt);
            //watch.Stop();
            //Console.WriteLine("update" + i.ToString() + "条的时间：" + watch.ElapsedMilliseconds);
            //Console.Read();


  
           //查询全部
           DBHelperBase<T_User> helper = new DBHelperBase<T_User>(DataBaseType.MSSQLServer, strconn);
            /*


           watch.Start();
           EntityCollection<T_User> users = helper.SelectAll(true);
           watch.Stop();
           Console.WriteLine("orm查询用aop" + users.Count+"条的时间："+ watch.ElapsedMilliseconds);
           watch.Restart();
           EntityCollection<T_User> users1 = helper.SelectAll(false);
           watch.Stop();
           Console.WriteLine("orm查询不用aop" + users.Count + "条的时间：" + watch.ElapsedMilliseconds);


           //foreach (T_User aa in users)
           //{
           //    aa.DisplayName = aa.DisplayName + Guid.NewGuid().ToString();
           //}
           //Console.WriteLine("开始更新");
           //watch.Restart();
           //users.Update();
           //watch.Stop();
           //Console.WriteLine("更新时间：" + watch.ElapsedMilliseconds);
           //Console.Read();

          

           string id = Guid.NewGuid().ToString();

            //insert
          
           T_User newUser =  users.CreateNewEntity();
           newUser.ID = id;
           newUser.Password = Guid.NewGuid().ToString();
           newUser.UserName = Guid.NewGuid().ToString();
           newUser.State = 100;
           users.Add(newUser);
           users.Update();
        

          //update
          T_User user =  users.Where(t => t.ID == id).First();
          user.UserName = id;
          users.Update();

         //delete
          users.Delete(user);
          users.Update();
         
         //search by condition
          T_User searchuser = helper.CreateSimpleSeachCondition();
          searchuser.ID = id ;
          users = helper.SelectDataByCondition(searchuser,true);

*/
         DbSearchConditionCollection<T_User> cc = helper.CreateComplexSeachCondition();
         cc.Add(p => p.ID, DbSearchOperation.equals, "000073cd-17e3-483d-a14f-5e1efff93cb6");
         EntityCollection<T_User> usersaaaa= helper.SelectDataByCondition(cc, true);
         Console.Read();
        }
    }
}
