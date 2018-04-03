namespace DBDiff
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDBUser = new System.Windows.Forms.TextBox();
            this.btnAnalyse = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtSid2 = new System.Windows.Forms.TextBox();
            this.txtPassword2 = new System.Windows.Forms.TextBox();
            this.txtUserName2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtSid1 = new System.Windows.Forms.TextBox();
            this.txtPassword1 = new System.Windows.Forms.TextBox();
            this.txtUserName1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.listView2 = new System.Windows.Forms.ListView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnExportAllSQL = new System.Windows.Forms.Button();
            this.btnSelAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelReverse = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDBUser
            // 
            this.txtDBUser.Location = new System.Drawing.Point(157, 327);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Size = new System.Drawing.Size(390, 22);
            this.txtDBUser.TabIndex = 7;
            this.txtDBUser.Text = "GWGL";
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Location = new System.Drawing.Point(599, 324);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(197, 28);
            this.btnAnalyse.TabIndex = 6;
            this.btnAnalyse.Text = "比对";
            this.btnAnalyse.UseVisualStyleBackColor = true;
            this.btnAnalyse.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.txtSid2);
            this.panel2.Controls.Add(this.txtPassword2);
            this.panel2.Controls.Add(this.txtUserName2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(426, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(395, 285);
            this.panel2.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(172, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 17);
            this.label9.TabIndex = 13;
            this.label9.Text = "B库(老库)";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(133, 223);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 38);
            this.button3.TabIndex = 12;
            this.button3.Text = "测试";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // txtSid2
            // 
            this.txtSid2.Location = new System.Drawing.Point(133, 169);
            this.txtSid2.Name = "txtSid2";
            this.txtSid2.Size = new System.Drawing.Size(182, 22);
            this.txtSid2.TabIndex = 11;
            this.txtSid2.Text = "  (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNE" +
    "CT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl)))";
            // 
            // txtPassword2
            // 
            this.txtPassword2.Location = new System.Drawing.Point(133, 115);
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.Size = new System.Drawing.Size(182, 22);
            this.txtPassword2.TabIndex = 10;
            this.txtPassword2.Text = "123asd";
            // 
            // txtUserName2
            // 
            this.txtUserName2.Location = new System.Drawing.Point(133, 62);
            this.txtUserName2.Name = "txtUserName2";
            this.txtUserName2.Size = new System.Drawing.Size(182, 22);
            this.txtUserName2.TabIndex = 9;
            this.txtUserName2.Text = "gwgl";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "SID：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "密码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(65, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "用户名：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.txtSid1);
            this.panel1.Controls.Add(this.txtPassword1);
            this.panel1.Controls.Add(this.txtUserName1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(37, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 285);
            this.panel1.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(136, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 17);
            this.label8.TabIndex = 7;
            this.label8.Text = "A库(新库)";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(87, 223);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 38);
            this.button2.TabIndex = 6;
            this.button2.Text = "测试";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // txtSid1
            // 
            this.txtSid1.Location = new System.Drawing.Point(112, 169);
            this.txtSid1.Name = "txtSid1";
            this.txtSid1.Size = new System.Drawing.Size(182, 22);
            this.txtSid1.TabIndex = 5;
            this.txtSid1.Text = "  (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.35.3.188)(PORT = 1521))(CON" +
    "NECT_DATA =(SERVICE_NAME = LX)))";
            // 
            // txtPassword1
            // 
            this.txtPassword1.Location = new System.Drawing.Point(112, 115);
            this.txtPassword1.Name = "txtPassword1";
            this.txtPassword1.Size = new System.Drawing.Size(182, 22);
            this.txtPassword1.TabIndex = 4;
            this.txtPassword1.Text = "eport";
            // 
            // txtUserName1
            // 
            this.txtUserName1.Location = new System.Drawing.Point(112, 62);
            this.txtUserName1.Name = "txtUserName1";
            this.txtUserName1.Size = new System.Drawing.Size(182, 22);
            this.txtUserName1.TabIndex = 3;
            this.txtUserName1.Text = "gwgl";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "SID：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(84, 330);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "用户：";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(48, 381);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(234, 363);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(301, 381);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(259, 363);
            this.listView2.TabIndex = 10;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(583, 381);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(221, 363);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            // 
            // btnExportAllSQL
            // 
            this.btnExportAllSQL.Location = new System.Drawing.Point(537, 750);
            this.btnExportAllSQL.Name = "btnExportAllSQL";
            this.btnExportAllSQL.Size = new System.Drawing.Size(267, 38);
            this.btnExportAllSQL.TabIndex = 12;
            this.btnExportAllSQL.Text = "批量生产SQL语句";
            this.btnExportAllSQL.UseVisualStyleBackColor = true;
            this.btnExportAllSQL.Click += new System.EventHandler(this.btnExportAllSQL_Click);
            // 
            // btnSelAll
            // 
            this.btnSelAll.Location = new System.Drawing.Point(48, 750);
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(59, 27);
            this.btnSelAll.TabIndex = 15;
            this.btnSelAll.Text = "全选";
            this.btnSelAll.UseVisualStyleBackColor = true;
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(178, 750);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(59, 27);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelReverse
            // 
            this.btnSelReverse.Location = new System.Drawing.Point(113, 750);
            this.btnSelReverse.Name = "btnSelReverse";
            this.btnSelReverse.Size = new System.Drawing.Size(59, 27);
            this.btnSelReverse.TabIndex = 17;
            this.btnSelReverse.Text = "反选";
            this.btnSelReverse.UseVisualStyleBackColor = true;
            this.btnSelReverse.Click += new System.EventHandler(this.btnSelReverse_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 848);
            this.Controls.Add(this.btnSelReverse);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelAll);
            this.Controls.Add(this.btnExportAllSQL);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDBUser);
            this.Controls.Add(this.btnAnalyse);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDBUser;
        private System.Windows.Forms.Button btnAnalyse;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtSid2;
        private System.Windows.Forms.TextBox txtPassword2;
        private System.Windows.Forms.TextBox txtUserName2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtSid1;
        private System.Windows.Forms.TextBox txtPassword1;
        private System.Windows.Forms.TextBox txtUserName1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnExportAllSQL;
        private System.Windows.Forms.Button btnSelAll;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelReverse;


    }
}

