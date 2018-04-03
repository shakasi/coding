using ESTM.Common.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ESTM.Web.IBLL
{
    public interface IPowerManager
    {
        List<DTO_TB_USERS> GetUsers(Expression<Func<DTO_TB_USERS, bool>> selector = null);

        DTO_TB_USERS AddUser(DTO_TB_USERS oUser);

        bool DeleteUser(DTO_TB_USERS oUser);

        bool UpdateUser(DTO_TB_USERS oUser);

        bool DeleteUser(Expression<Func<DTO_TB_USERS, bool>> selector = null);



        List<DTO_TB_DEPARTMENT> GetDepartments(Expression<Func<DTO_TB_DEPARTMENT, bool>> selector = null);

        DTO_TB_DEPARTMENT AddDepartment(DTO_TB_DEPARTMENT oDept);

        bool DeleteDepartment(DTO_TB_DEPARTMENT oDept);

        bool DeleteDepartment(Expression<Func<DTO_TB_DEPARTMENT, bool>> selector = null);

        bool UpdateDepartment(DTO_TB_DEPARTMENT oDept);




        List<DTO_TB_ROLE> GetRoles(Expression<Func<DTO_TB_ROLE, bool>> selector = null);

        List<DTO_TB_MENU> GetMenus(Expression<Func<DTO_TB_MENU, bool>> selector = null);

    }
}
