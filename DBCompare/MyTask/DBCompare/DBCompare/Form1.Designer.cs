namespace DBCompare
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbIPA = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbDBA = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnA = new System.Windows.Forms.Button();
            this.tbPwdA = new System.Windows.Forms.TextBox();
            this.tbUserNameA = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAnalyse = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbIPB = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbDBB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPwdB = new System.Windows.Forms.TextBox();
            this.tbUserNameB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnB = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.listView3 = new System.Windows.Forms.ListView();
            this.listView4 = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(7, 252);
            this.listView1.Margin = new System.Windows.Forms.Padding(2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(135, 332);
            this.listView1.TabIndex = 23;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(102, 13);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "A库(新库)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbIPA);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.tbDBA);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnA);
            this.panel1.Controls.Add(this.tbPwdA);
            this.panel1.Controls.Add(this.tbUserNameA);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 195);
            this.panel1.TabIndex = 18;
            // 
            // tbIPA
            // 
            this.tbIPA.Location = new System.Drawing.Point(83, 36);
            this.tbIPA.Margin = new System.Windows.Forms.Padding(2);
            this.tbIPA.Name = "tbIPA";
            this.tbIPA.Size = new System.Drawing.Size(138, 21);
            this.tbIPA.TabIndex = 11;
            this.tbIPA.Text = "192.168.21.142\\sqlexpress";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(32, 36);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 10;
            this.label13.Text = "IP    ：";
            // 
            // tbDBA
            // 
            this.tbDBA.Location = new System.Drawing.Point(84, 65);
            this.tbDBA.Margin = new System.Windows.Forms.Padding(2);
            this.tbDBA.Name = "tbDBA";
            this.tbDBA.Size = new System.Drawing.Size(138, 21);
            this.tbDBA.TabIndex = 9;
            this.tbDBA.Text = "Ajisen";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "库名  ：";
            // 
            // btnA
            // 
            this.btnA.Location = new System.Drawing.Point(84, 159);
            this.btnA.Margin = new System.Windows.Forms.Padding(2);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(94, 28);
            this.btnA.TabIndex = 6;
            this.btnA.Text = "测试";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // tbPwdA
            // 
            this.tbPwdA.Location = new System.Drawing.Point(84, 130);
            this.tbPwdA.Margin = new System.Windows.Forms.Padding(2);
            this.tbPwdA.Name = "tbPwdA";
            this.tbPwdA.PasswordChar = '*';
            this.tbPwdA.Size = new System.Drawing.Size(138, 21);
            this.tbPwdA.TabIndex = 4;
            this.tbPwdA.Text = "123!@#qwe";
            // 
            // tbUserNameA
            // 
            this.tbUserNameA.Location = new System.Drawing.Point(84, 97);
            this.tbUserNameA.Margin = new System.Windows.Forms.Padding(2);
            this.tbUserNameA.Name = "tbUserNameA";
            this.tbUserNameA.Size = new System.Drawing.Size(138, 21);
            this.tbUserNameA.TabIndex = 3;
            this.tbUserNameA.Text = "sa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 134);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码  ：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 97);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Location = new System.Drawing.Point(493, 213);
            this.btnAnalyse.Margin = new System.Windows.Forms.Padding(2);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(102, 28);
            this.btnAnalyse.TabIndex = 20;
            this.btnAnalyse.Text = "比对";
            this.btnAnalyse.UseVisualStyleBackColor = true;
            this.btnAnalyse.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbIPB);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.tbDBB);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tbPwdB);
            this.panel2.Controls.Add(this.tbUserNameB);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.btnB);
            this.panel2.Location = new System.Drawing.Point(299, 7);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(296, 195);
            this.panel2.TabIndex = 19;
            // 
            // tbIPB
            // 
            this.tbIPB.Location = new System.Drawing.Point(99, 36);
            this.tbIPB.Margin = new System.Windows.Forms.Padding(2);
            this.tbIPB.Name = "tbIPB";
            this.tbIPB.Size = new System.Drawing.Size(138, 21);
            this.tbIPB.TabIndex = 21;
            this.tbIPB.Text = ".";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(48, 36);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 20;
            this.label14.Text = "IP    ：";
            // 
            // tbDBB
            // 
            this.tbDBB.Location = new System.Drawing.Point(99, 65);
            this.tbDBB.Margin = new System.Windows.Forms.Padding(2);
            this.tbDBB.Name = "tbDBB";
            this.tbDBB.Size = new System.Drawing.Size(138, 21);
            this.tbDBB.TabIndex = 19;
            this.tbDBB.Text = "POS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 65);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "库名  ：";
            // 
            // tbPwdB
            // 
            this.tbPwdB.Location = new System.Drawing.Point(99, 130);
            this.tbPwdB.Margin = new System.Windows.Forms.Padding(2);
            this.tbPwdB.Name = "tbPwdB";
            this.tbPwdB.PasswordChar = '*';
            this.tbPwdB.Size = new System.Drawing.Size(138, 21);
            this.tbPwdB.TabIndex = 17;
            this.tbPwdB.Text = "456852";
            // 
            // tbUserNameB
            // 
            this.tbUserNameB.Location = new System.Drawing.Point(99, 97);
            this.tbUserNameB.Margin = new System.Windows.Forms.Padding(2);
            this.tbUserNameB.Name = "tbUserNameB";
            this.tbUserNameB.Size = new System.Drawing.Size(138, 21);
            this.tbUserNameB.TabIndex = 16;
            this.tbUserNameB.Text = "sa";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 134);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "密码  ：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 97);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "用户名：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(129, 13);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "B库(老库)";
            // 
            // btnB
            // 
            this.btnB.Location = new System.Drawing.Point(100, 159);
            this.btnB.Margin = new System.Windows.Forms.Padding(2);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(94, 28);
            this.btnB.TabIndex = 12;
            this.btnB.Text = "测试";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Click += new System.EventHandler(this.btnB_Click);
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(158, 252);
            this.listView2.Margin = new System.Windows.Forms.Padding(2);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(135, 332);
            this.listView2.TabIndex = 32;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // listView3
            // 
            this.listView3.Location = new System.Drawing.Point(309, 252);
            this.listView3.Margin = new System.Windows.Forms.Padding(2);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(135, 332);
            this.listView3.TabIndex = 33;
            this.listView3.UseCompatibleStateImageBehavior = false;
            // 
            // listView4
            // 
            this.listView4.Location = new System.Drawing.Point(460, 252);
            this.listView4.Margin = new System.Windows.Forms.Padding(2);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(135, 332);
            this.listView4.TabIndex = 34;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView4_ItemSelectionChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 598);
            this.Controls.Add(this.listView4);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAnalyse);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库结构比较";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.TextBox tbPwdA;
        private System.Windows.Forms.TextBox tbUserNameA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAnalyse;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.TextBox tbDBA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDBB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPwdB;
        private System.Windows.Forms.TextBox tbUserNameB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.TextBox tbIPA;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbIPB;
        private System.Windows.Forms.Label label14;
    }
}

