using ESTM.Common.DtoModel;
using ESTM.Web.IBLL;
using Serialize.Linq.Nodes;
using Serialize.Linq.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ESTM.Web.BLL
{
    [Export(typeof(IPowerManager))]
    public class PowerManager : IPowerManager
    {
        #region Fields
        private ServiceReference_PowerManager.PowerManageWCFServiceClient oService = CreatePowerManagerService.GetInstance(); 
        #endregion

        #region 接口实现
        public List<DTO_TB_USERS> GetUsers(Expression<Func<Common.DtoModel.DTO_TB_USERS, bool>> selector = null)
        {
            return oService.GetUsers(GetExpressionNode<DTO_TB_USERS>(selector));
        }

        public List<Common.DtoModel.DTO_TB_DEPARTMENT> GetDepartments(Expression<Func<Common.DtoModel.DTO_TB_DEPARTMENT, bool>> selector = null)
        {
            return oService.GetDepartments(GetExpressionNode<DTO_TB_DEPARTMENT>(selector));
        }

        public List<Common.DtoModel.DTO_TB_ROLE> GetRoles(Expression<Func<Common.DtoModel.DTO_TB_ROLE, bool>> selector = null)
        {
            return oService.GetRoles(GetExpressionNode<DTO_TB_ROLE>(selector));
        }

        public List<Common.DtoModel.DTO_TB_MENU> GetMenus(Expression<Func<Common.DtoModel.DTO_TB_MENU, bool>> selector = null)
        {
            return oService.GetMenus(GetExpressionNode<DTO_TB_MENU>(selector));
        } 
        #endregion

        #region Privates
        //将lamada表达式转换为可用于WCF传递的ExpressionNode类型
        private ExpressionNode GetExpressionNode<Dto>(Expression<Func<Dto,bool>> selector)
        {
            if (selector == null)
            {
                return null;
            }
            ExpressionConverter expressionConverter = new ExpressionConverter();
            ExpressionNode expressionNode = expressionConverter.Convert(selector);
            return expressionNode;
        }
        #endregion


        public DTO_TB_USERS AddUser(DTO_TB_USERS oUser)
        {
            return oService.AddUser(oUser);
        }

        public bool DeleteUser(DTO_TB_USERS oUser)
        {
            return oService.DeleteUser(oUser);
        }

        public bool DeleteUser(Expression<Func<DTO_TB_USERS, bool>> selector = null)
        {
            if (selector == null)
            {
                return false;
            }
            ExpressionConverter expressionConverter = new ExpressionConverter();
            ExpressionNode expressionNode = expressionConverter.Convert(selector);
            return oService.DeleteUserByLamada(expressionNode);
        }


        public bool UpdateUser(DTO_TB_USERS oUser)
        {
            return oService.UpdateUser(oUser);
        }

        public DTO_TB_DEPARTMENT AddDepartment(DTO_TB_DEPARTMENT oDept)
        {
            return oService.AddDepartment(oDept);
        }

        public bool DeleteDepartment(DTO_TB_DEPARTMENT oDept)
        {
            return oService.DeleteDepartment(oDept);
        }

        public bool UpdateDepartment(DTO_TB_DEPARTMENT oDept)
        {
            return oService.UpdateDepartment(oDept);
        }




        public bool DeleteDepartment(Expression<Func<DTO_TB_DEPARTMENT, bool>> selector = null)
        {
            if (selector == null)
            {
                return false;
            }
            ExpressionConverter expressionConverter = new ExpressionConverter();
            ExpressionNode expressionNode = expressionConverter.Convert(selector);
            return oService.DeleteDeptByLamada(expressionNode);
        }
    }
}
