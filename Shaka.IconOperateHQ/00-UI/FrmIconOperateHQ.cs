using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Shaka.Infrastructure;
using Shaka.BLL;
using Shaka.Domain;
using Shaka.UI.UC;

namespace Shaka.UI
{
    public partial class FrmIconOperateHQ : Form
    {
        private string _loginID = "datascan";

        private HQIconOperateBLL _bll = null;

        public FrmIconOperateHQ()
        {
            InitializeComponent();
            this.ucPagination.Query += new QueryHandler(PaginationQuery);
        }

        private void FrmIconOperateHQ_Load(object sender, EventArgs e)
        {
            _bll = new HQIconOperateBLL();
            Query();
        }

        private void PaginationQuery(UCPagination sender)
        {
            Query();
        }

        private void Query()
        {
            int totalRow = 0;
            List<StoreGroupInfo> storeGroupList = _bll.GetStoreGroupByUser(_loginID, ucPagination.CurrentPage, "number ASC",
                ucPagination.PageRecordCount, ref totalRow);
            dgvStoreGroup.DataSource = storeGroupList;

            ucPagination.TotalRecordCount = totalRow;
        }

        private void dgvStoreGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sgNumber = -1;
            foreach (DataGridViewRow row in dgvStoreGroup.SelectedRows)
            {
                sgNumber = (int)row.Cells["StoreGroupNumber"].Value;
                break;
            }
            if (sgNumber != -1)
            {
                FrmIconHQ frmIcon = new FrmIconHQ(sgNumber, dtpEffectiveDate.Value.Date);
                frmIcon.ShowDialog();
            }
        }
    }
}
