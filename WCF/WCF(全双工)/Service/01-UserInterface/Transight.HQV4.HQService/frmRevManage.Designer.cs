namespace Transight.HQV4.HQService
{
    partial class frmRevManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chk_stores = new System.Windows.Forms.CheckBox();
            this.DgvStores = new System.Windows.Forms.DataGridView();
            this.S_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_RESULT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnGiveup = new System.Windows.Forms.Button();
            this.lblNum = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DgvService = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAutoUpdte = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOtherUpload = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUPBizDate = new System.Windows.Forms.Button();
            this.btnCallBack = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnUpdImport = new System.Windows.Forms.Button();
            this.btnIenImport = new System.Windows.Forms.Button();
            this.btnIendl = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewDisableCheckBoxColumn1 = new Transight.HQV4.HQService.DataGridViewDisableCheckBoxColumn();
            this.dataGridViewDisableButtonColumn1 = new Transight.HQV4.HQService.DataGridViewDisableButtonColumn();
            this.dataGridViewDisableButtonColumn2 = new Transight.HQV4.HQService.DataGridViewDisableButtonColumn();
            this.S_CHK = new Transight.HQV4.HQService.DataGridViewDisableCheckBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.H_MENDIAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Open = new Transight.HQV4.HQService.DataGridViewDisableButtonColumn();
            this.Close = new Transight.HQV4.HQService.DataGridViewDisableButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvStores)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvService)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chk_stores);
            this.panel1.Controls.Add(this.DgvStores);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 450);
            this.panel1.TabIndex = 0;
            // 
            // chk_stores
            // 
            this.chk_stores.AutoSize = true;
            this.chk_stores.Location = new System.Drawing.Point(12, 57);
            this.chk_stores.Name = "chk_stores";
            this.chk_stores.Size = new System.Drawing.Size(48, 16);
            this.chk_stores.TabIndex = 3;
            this.chk_stores.Text = "全选";
            this.chk_stores.UseVisualStyleBackColor = true;
            this.chk_stores.Click += new System.EventHandler(this.chk_stores_Click);
            // 
            // DgvStores
            // 
            this.DgvStores.AllowUserToAddRows = false;
            this.DgvStores.AllowUserToDeleteRows = false;
            this.DgvStores.AllowUserToOrderColumns = true;
            this.DgvStores.AllowUserToResizeRows = false;
            this.DgvStores.BackgroundColor = System.Drawing.Color.White;
            this.DgvStores.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvStores.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvStores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvStores.ColumnHeadersHeight = 30;
            this.DgvStores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DgvStores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.S_CHK,
            this.S_NAME,
            this.S_RESULT});
            this.DgvStores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvStores.GridColor = System.Drawing.Color.Silver;
            this.DgvStores.Location = new System.Drawing.Point(0, 49);
            this.DgvStores.MultiSelect = false;
            this.DgvStores.Name = "DgvStores";
            this.DgvStores.ReadOnly = true;
            this.DgvStores.RowHeadersVisible = false;
            this.DgvStores.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvStores.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvStores.RowTemplate.Height = 30;
            this.DgvStores.RowTemplate.ReadOnly = true;
            this.DgvStores.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvStores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvStores.Size = new System.Drawing.Size(377, 401);
            this.DgvStores.TabIndex = 2;
            this.DgvStores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvStores_CellContentClick);
            // 
            // S_NAME
            // 
            this.S_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.S_NAME.DataPropertyName = "StoreName";
            this.S_NAME.FillWeight = 83.14721F;
            this.S_NAME.HeaderText = "门店名称";
            this.S_NAME.Name = "S_NAME";
            this.S_NAME.ReadOnly = true;
            // 
            // S_RESULT
            // 
            this.S_RESULT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.S_RESULT.DataPropertyName = "Result";
            this.S_RESULT.FillWeight = 83.14721F;
            this.S_RESULT.HeaderText = "操作结果";
            this.S_RESULT.Name = "S_RESULT";
            this.S_RESULT.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnRetry);
            this.panel2.Controls.Add(this.btnGiveup);
            this.panel2.Controls.Add(this.lblNum);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(377, 49);
            this.panel2.TabIndex = 1;
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(255, 15);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(75, 23);
            this.btnRetry.TabIndex = 4;
            this.btnRetry.Text = "重试操作";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnGiveup
            // 
            this.btnGiveup.Location = new System.Drawing.Point(157, 15);
            this.btnGiveup.Name = "btnGiveup";
            this.btnGiveup.Size = new System.Drawing.Size(75, 23);
            this.btnGiveup.TabIndex = 3;
            this.btnGiveup.Text = "放弃操作";
            this.btnGiveup.UseVisualStyleBackColor = true;
            this.btnGiveup.Click += new System.EventHandler(this.btnGiveup_Click);
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.BackColor = System.Drawing.Color.White;
            this.lblNum.ForeColor = System.Drawing.Color.Red;
            this.lblNum.Location = new System.Drawing.Point(80, 20);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(11, 12);
            this.lblNum.TabIndex = 2;
            this.lblNum.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "个";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "已选门店：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(60, 25);
            this.tabControl1.Location = new System.Drawing.Point(377, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(391, 450);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DgvService);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(383, 417);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "服务开关";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DgvService
            // 
            this.DgvService.AllowUserToAddRows = false;
            this.DgvService.AllowUserToDeleteRows = false;
            this.DgvService.AllowUserToOrderColumns = true;
            this.DgvService.AllowUserToResizeRows = false;
            this.DgvService.BackgroundColor = System.Drawing.Color.White;
            this.DgvService.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvService.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvService.ColumnHeadersHeight = 30;
            this.DgvService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DgvService.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.H_MENDIAN,
            this.Open,
            this.Close});
            this.DgvService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvService.GridColor = System.Drawing.Color.Silver;
            this.DgvService.Location = new System.Drawing.Point(3, 3);
            this.DgvService.MultiSelect = false;
            this.DgvService.Name = "DgvService";
            this.DgvService.ReadOnly = true;
            this.DgvService.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvService.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DgvService.RowTemplate.Height = 30;
            this.DgvService.RowTemplate.ReadOnly = true;
            this.DgvService.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvService.Size = new System.Drawing.Size(377, 411);
            this.DgvService.TabIndex = 0;
            this.DgvService.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvService_CellContentClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(383, 417);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "其他操作";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.btnAutoUpdte);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 283);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(377, 131);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "软件版本";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(142, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 19;
            this.button1.Text = "还原到最近版本";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAutoUpdte
            // 
            this.btnAutoUpdte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAutoUpdte.Location = new System.Drawing.Point(21, 56);
            this.btnAutoUpdte.Name = "btnAutoUpdte";
            this.btnAutoUpdte.Size = new System.Drawing.Size(100, 28);
            this.btnAutoUpdte.TabIndex = 18;
            this.btnAutoUpdte.Text = "自动更新";
            this.btnAutoUpdte.UseVisualStyleBackColor = true;
            this.btnAutoUpdte.Click += new System.EventHandler(this.btnAutoUpdte_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.btnOtherUpload);
            this.groupBox2.Controls.Add(this.btnUpload);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 132);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据上传";
            // 
            // btnOtherUpload
            // 
            this.btnOtherUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOtherUpload.Location = new System.Drawing.Point(142, 51);
            this.btnOtherUpload.Name = "btnOtherUpload";
            this.btnOtherUpload.Size = new System.Drawing.Size(100, 28);
            this.btnOtherUpload.TabIndex = 22;
            this.btnOtherUpload.Text = "其他数据上传";
            this.btnOtherUpload.UseVisualStyleBackColor = true;
            this.btnOtherUpload.Click += new System.EventHandler(this.btnOtherUpload_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpload.Location = new System.Drawing.Point(21, 51);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(100, 28);
            this.btnUpload.TabIndex = 21;
            this.btnUpload.Text = "销售数据上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnUPBizDate);
            this.groupBox1.Controls.Add(this.btnCallBack);
            this.groupBox1.Controls.Add(this.btnDownload);
            this.groupBox1.Controls.Add(this.btnUpdImport);
            this.groupBox1.Controls.Add(this.btnIenImport);
            this.groupBox1.Controls.Add(this.btnIendl);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 148);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置文件下发流程";
            // 
            // btnUPBizDate
            // 
            this.btnUPBizDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUPBizDate.Location = new System.Drawing.Point(255, 80);
            this.btnUPBizDate.Name = "btnUPBizDate";
            this.btnUPBizDate.Size = new System.Drawing.Size(100, 28);
            this.btnUPBizDate.TabIndex = 26;
            this.btnUPBizDate.Text = "更新营业日";
            this.btnUPBizDate.UseVisualStyleBackColor = true;
            this.btnUPBizDate.Click += new System.EventHandler(this.btnUPBizDate_Click);
            // 
            // btnCallBack
            // 
            this.btnCallBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCallBack.Location = new System.Drawing.Point(255, 46);
            this.btnCallBack.Name = "btnCallBack";
            this.btnCallBack.Size = new System.Drawing.Size(100, 28);
            this.btnCallBack.TabIndex = 25;
            this.btnCallBack.Text = "处理Ien数据反馈";
            this.btnCallBack.UseVisualStyleBackColor = true;
            this.btnCallBack.Click += new System.EventHandler(this.btnCallBack_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDownload.Location = new System.Drawing.Point(137, 80);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(100, 28);
            this.btnDownload.TabIndex = 24;
            this.btnDownload.Text = "各类文件下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUpdImport
            // 
            this.btnUpdImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpdImport.Location = new System.Drawing.Point(21, 80);
            this.btnUpdImport.Name = "btnUpdImport";
            this.btnUpdImport.Size = new System.Drawing.Size(100, 28);
            this.btnUpdImport.TabIndex = 23;
            this.btnUpdImport.Text = "upd表导入";
            this.btnUpdImport.UseVisualStyleBackColor = true;
            this.btnUpdImport.Click += new System.EventHandler(this.btnUpdImport_Click);
            // 
            // btnIenImport
            // 
            this.btnIenImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnIenImport.Location = new System.Drawing.Point(137, 46);
            this.btnIenImport.Name = "btnIenImport";
            this.btnIenImport.Size = new System.Drawing.Size(100, 28);
            this.btnIenImport.TabIndex = 22;
            this.btnIenImport.Text = "IEN文件导入";
            this.btnIenImport.UseVisualStyleBackColor = true;
            this.btnIenImport.Click += new System.EventHandler(this.btnIenImport_Click);
            // 
            // btnIendl
            // 
            this.btnIendl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnIendl.Location = new System.Drawing.Point(21, 46);
            this.btnIendl.Name = "btnIendl";
            this.btnIendl.Size = new System.Drawing.Size(100, 28);
            this.btnIendl.TabIndex = 21;
            this.btnIendl.Text = "IEN文件自动下载";
            this.btnIendl.UseVisualStyleBackColor = true;
            this.btnIendl.Click += new System.EventHandler(this.btnIendl_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "StoreName";
            this.dataGridViewTextBoxColumn1.FillWeight = 83.14721F;
            this.dataGridViewTextBoxColumn1.HeaderText = "门店名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Result";
            this.dataGridViewTextBoxColumn2.FillWeight = 83.14721F;
            this.dataGridViewTextBoxColumn2.HeaderText = "操作结果";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn3.HeaderText = "名称";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 60;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewDisableCheckBoxColumn1
            // 
            this.dataGridViewDisableCheckBoxColumn1.DataPropertyName = "IsCheck";
            this.dataGridViewDisableCheckBoxColumn1.FillWeight = 70F;
            this.dataGridViewDisableCheckBoxColumn1.HeaderText = "";
            this.dataGridViewDisableCheckBoxColumn1.Name = "dataGridViewDisableCheckBoxColumn1";
            this.dataGridViewDisableCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDisableCheckBoxColumn1.Width = 70;
            // 
            // dataGridViewDisableButtonColumn1
            // 
            this.dataGridViewDisableButtonColumn1.DataPropertyName = "Code";
            this.dataGridViewDisableButtonColumn1.HeaderText = "操作";
            this.dataGridViewDisableButtonColumn1.Name = "dataGridViewDisableButtonColumn1";
            this.dataGridViewDisableButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewDisableButtonColumn2
            // 
            this.dataGridViewDisableButtonColumn2.DataPropertyName = "Code";
            this.dataGridViewDisableButtonColumn2.HeaderText = "操作";
            this.dataGridViewDisableButtonColumn2.Name = "dataGridViewDisableButtonColumn2";
            this.dataGridViewDisableButtonColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // S_CHK
            // 
            this.S_CHK.DataPropertyName = "IsCheck";
            this.S_CHK.FillWeight = 70F;
            this.S_CHK.HeaderText = "";
            this.S_CHK.Name = "S_CHK";
            this.S_CHK.ReadOnly = true;
            this.S_CHK.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.S_CHK.Width = 70;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.FillWeight = 60F;
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 60;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewButtonColumn1.DataPropertyName = "Code";
            this.dataGridViewButtonColumn1.HeaderText = "操作";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewButtonColumn1.Text = "开启|关闭";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.DataPropertyName = "Code";
            this.dataGridViewButtonColumn2.HeaderText = "操作";
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.Text = "关闭";
            this.dataGridViewButtonColumn2.UseColumnTextForButtonValue = true;
            // 
            // H_MENDIAN
            // 
            this.H_MENDIAN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.H_MENDIAN.DataPropertyName = "Name";
            this.H_MENDIAN.HeaderText = "名称";
            this.H_MENDIAN.MinimumWidth = 60;
            this.H_MENDIAN.Name = "H_MENDIAN";
            this.H_MENDIAN.ReadOnly = true;
            this.H_MENDIAN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Open
            // 
            this.Open.DataPropertyName = "Code";
            this.Open.HeaderText = "操作";
            this.Open.Name = "Open";
            this.Open.ReadOnly = true;
            this.Open.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Open.Text = "开启";
            this.Open.UseColumnTextForButtonValue = true;
            // 
            // Close
            // 
            this.Close.DataPropertyName = "Code";
            this.Close.HeaderText = "操作";
            this.Close.Name = "Close";
            this.Close.ReadOnly = true;
            this.Close.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Close.Text = "关闭";
            this.Close.UseColumnTextForButtonValue = true;
            // 
            // frmRevManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmRevManage";
            this.Text = "门店管理";
            this.Load += new System.EventHandler(this.frmRevManage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvStores)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvService)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOtherUpload;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAutoUpdte;
        private System.Windows.Forms.Button btnUPBizDate;
        private System.Windows.Forms.Button btnCallBack;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUpdImport;
        private System.Windows.Forms.Button btnIenImport;
        private System.Windows.Forms.Button btnIendl;
        private System.Windows.Forms.DataGridView DgvService;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridView DgvStores;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.CheckBox chk_stores;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnGiveup;
        private DataGridViewDisableCheckBoxColumn S_CHK;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_RESULT;
        private System.Windows.Forms.DataGridViewTextBoxColumn H_MENDIAN;
        private DataGridViewDisableButtonColumn Open;
        private DataGridViewDisableButtonColumn Close;
        private DataGridViewDisableCheckBoxColumn dataGridViewDisableCheckBoxColumn1;
        private DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn1;
        private DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn2;
    }
}