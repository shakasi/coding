using System.Web;
using System.Web.Optimization;

namespace ESTM.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                      "~/Scripts/knockout-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                      "~/Content/jqGrid/js/i18n/grid.locale-cn.js",
                      "~/Content/jqGrid/js/jquery.jqGrid.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table").Include(
                      "~/Content/bootstrap-table/bootstrap-table.js",
                      "~/Content/bootstrap-table/extensions/editable/bootstrap-table-editable.js",
                      "~/Content/bootstrap3-editable/js/bootstrap-editable.js",
                      "~/Content/bootstrap-table/locale/bootstrap-table-zh-CN.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/jqgrid").Include(
                     "~/Content/jqGrid/css/ui.jqgrid-bootstrap.css"));


            bundles.Add(new StyleBundle("~/Content/table-css").Include(
                      "~/Content/bootstrap-table/bootstrap-table.css",
                      "~/Content/bootstrap3-editable/css/bootstrap-editable.css"));

            //bootstrap时间控件
            bundles.Add(new ScriptBundle("~/Content/bootstrap/datepicker/js").Include(
                     "~/Content/bootstrap-datetimepicker-master/js/bootstrap-datetimepicker.min.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrap/datepicker/css").Include(
                     "~/Content/bootstrap-datetimepicker-master/css/bootstrap-datetimepicker.min.css"));

            #region 权限管理
            bundles.Add(new ScriptBundle("~/bundles/PowerManage/UserManager").Include(
                      "~/Scripts/PowerManage/UserManager.js"));

            bundles.Add(new ScriptBundle("~/bundles/PowerManage/DepartmentManage").Include(
                      "~/Scripts/PowerManage/DepartmentManage.js"));

            bundles.Add(new ScriptBundle("~/bundles/PowerManage/MenuManage").Include(
                      "~/Scripts/PowerManage/MenuManage.js"));

            bundles.Add(new ScriptBundle("~/bundles/PowerManage/RoleManage").Include(
                      "~/Scripts/PowerManage/RoleManage.js"));
            #endregion
        }
    }
}
