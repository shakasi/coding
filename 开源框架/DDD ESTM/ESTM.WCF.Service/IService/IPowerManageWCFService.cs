using ESTM.Common.DtoModel;
using Serialize.Linq.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ESTM.WCF.Service
{
    /// <summary>
    /// 权限管理模块接口契约
    /// </summary>
    [ServiceContract]
    [ServiceInterface]
    public interface IPowerManageWCFService
    {

        #region 用户管理
        [OperationContract]
        List<DTO_TB_USERS> GetUsers(ExpressionNode expressionNode);

        [OperationContract]
        DTO_TB_USERS AddUser(DTO_TB_USERS oUser);


        [OperationContract]
        bool DeleteUser(DTO_TB_USERS oUser);

        [OperationContract]
        bool DeleteUserByLamada(ExpressionNode expressionNode);

        [OperationContract]
        bool UpdateUser(DTO_TB_USERS oUser);
        #endregion

        #region 部门管理
        [OperationContract]
        List<DTO_TB_DEPARTMENT> GetDepartments(ExpressionNode expressionNode);

        [OperationContract]
        DTO_TB_DEPARTMENT AddDepartment(DTO_TB_DEPARTMENT oDept);

        [OperationContract]
        bool DeleteDepartment(DTO_TB_DEPARTMENT oDept);

        [OperationContract]
        bool DeleteDeptByLamada(ExpressionNode expressionNode);

        [OperationContract]
        bool UpdateDepartment(DTO_TB_DEPARTMENT oDept);
        #endregion

        #region 角色管理
        [OperationContract]
        List<DTO_TB_ROLE> GetRoles(ExpressionNode expressionNode);

        [OperationContract]
        DTO_TB_ROLE AddRole(DTO_TB_ROLE oRole);
        #endregion

        #region 菜单管理
        [OperationContract]
        List<DTO_TB_MENU> GetMenus(ExpressionNode expressionNode);

        [OperationContract]
        DTO_TB_MENU AddMenu(DTO_TB_MENU oMenu);
        #endregion
    }
}
