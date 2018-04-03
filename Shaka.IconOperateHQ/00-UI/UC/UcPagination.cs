using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Shaka.UI.UC
{
    public delegate void QueryHandler(UCPagination sender);

    public partial class UCPagination : UserControl
    {
        QueryHandler onQuery = null;
        public event QueryHandler Query
        {
            add { onQuery += value; }
            remove { onQuery -= value; }
        }

        int _currentPage;
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            set
            {
                if (value <= 0)
                    return;
                if (value > this.TotalPageCount && this.TotalPageCount > 0)
                    return;

                _currentPage = value;
                this.txt_CurrentPage.Text = value.ToString();
            }
            get { return _currentPage; }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecordCount
        {
            set
            {
                this.label_TotalRecordCount.Text = value.ToString();
                var pageCount = value / Convert.ToInt32(this.PageRecordCount);
                if (value % Convert.ToInt32(this.PageRecordCount) > 0)
                    pageCount++;
                this.label_TotalPageCount.Text = pageCount.ToString();
                DisplayLinks();
            }
            get { return Convert.ToInt32(this.label_TotalRecordCount.Text); }
        }

        /// <summary>
        /// 每页的记录数
        /// </summary>
        public int PageRecordCount
        {
            set;
            get;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount
        {
            set { this.label_TotalPageCount.Text = value.ToString(); }
            get { return Convert.ToInt32(this.label_TotalPageCount.Text); }
        }

        public UCPagination()
        {
            InitializeComponent();
        }

        private void UCPagination_Load(object sender, EventArgs e)
        {
            this._currentPage = 1;
            this.PageRecordCount = 5;
            this.txt_CurrentPage.Text = this.CurrentPage.ToString();
        }

        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_FirstPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CurrentPage == 0)
                return;

            this.CurrentPage = 1;
            if (onQuery != null)
                onQuery(this);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_PreviousPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CurrentPage == 0)
                return;

            this.CurrentPage--;
            if (onQuery != null)
                onQuery(this);
        }

        /// <summary>
        /// 跳转页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_Go_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CurrentPage == 0)
                return;

            int iCurrentPage = Convert.ToInt32(this.txt_CurrentPage.Text);
            if (iCurrentPage > this.TotalPageCount || iCurrentPage <= 0)
            {
                this.txt_CurrentPage.Text = this._currentPage.ToString();
                return;
            }

            this.CurrentPage = iCurrentPage;
            this.txt_CurrentPage.Text = this._currentPage.ToString();
            if (onQuery != null)
                onQuery(this);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_NextPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CurrentPage == 0)
                return;

            this.CurrentPage++;
            if (onQuery != null)
                onQuery(this);
        }

        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_LastPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CurrentPage == 0)
                return;

            this.CurrentPage = this.TotalPageCount;
            if (onQuery != null)
                onQuery(this);
        }

        /// <summary>
        /// 刷新各个分页链接的可用状态
        /// </summary>
        public void DisplayLinks()
        {
            linkLabel_PreviousPage.Enabled = linkLabel_FirstPage.Enabled = !(this.CurrentPage <= 1);
            linkLabel_NextPage.Enabled = linkLabel_LastPage.Enabled = !(this.CurrentPage >= this.TotalPageCount);
            linkLabel_Go.Enabled = (this.TotalPageCount > 0);
        }
    }
}
