
namespace WinFormsApptest1
{
    partial class server
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.startserver = new System.Windows.Forms.Button();
            this.closeserver = new System.Windows.Forms.Button();
            this.IPAddr = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.TextBox();
            this.Serstatus = new System.Windows.Forms.TextBox();
            this.ShowDetail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SendSize = new System.Windows.Forms.TextBox();
            this.WindLoca1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器 IP地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "服务器状态：";
            // 
            // startserver
            // 
            this.startserver.Location = new System.Drawing.Point(299, 79);
            this.startserver.Name = "startserver";
            this.startserver.Size = new System.Drawing.Size(93, 29);
            this.startserver.TabIndex = 3;
            this.startserver.Text = "启动服务器";
            this.startserver.UseVisualStyleBackColor = true;
            this.startserver.Click += new System.EventHandler(this.button1_Click);
            // 
            // closeserver
            // 
            this.closeserver.Location = new System.Drawing.Point(411, 79);
            this.closeserver.Name = "closeserver";
            this.closeserver.Size = new System.Drawing.Size(93, 29);
            this.closeserver.TabIndex = 4;
            this.closeserver.Text = "关闭服务器";
            this.closeserver.UseVisualStyleBackColor = true;
            this.closeserver.Click += new System.EventHandler(this.closeserver_Click);
            // 
            // IPAddr
            // 
            this.IPAddr.Location = new System.Drawing.Point(173, 41);
            this.IPAddr.Name = "IPAddr";
            this.IPAddr.Size = new System.Drawing.Size(124, 27);
            this.IPAddr.TabIndex = 5;
            this.IPAddr.Text = "127.0.0.1";
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(424, 41);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(124, 27);
            this.Port.TabIndex = 6;
            this.Port.Text = "10086";
            // 
            // Serstatus
            // 
            this.Serstatus.Location = new System.Drawing.Point(156, 79);
            this.Serstatus.Name = "Serstatus";
            this.Serstatus.Size = new System.Drawing.Size(124, 27);
            this.Serstatus.TabIndex = 7;
            this.Serstatus.Text = "未启动";
            // 
            // ShowDetail
            // 
            this.ShowDetail.Location = new System.Drawing.Point(51, 206);
            this.ShowDetail.Multiline = true;
            this.ShowDetail.Name = "ShowDetail";
            this.ShowDetail.Size = new System.Drawing.Size(497, 386);
            this.ShowDetail.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "发送窗口大小：";
            // 
            // SendSize
            // 
            this.SendSize.Location = new System.Drawing.Point(156, 121);
            this.SendSize.Name = "SendSize";
            this.SendSize.Size = new System.Drawing.Size(84, 27);
            this.SendSize.TabIndex = 10;
            this.SendSize.Text = "5";
            // 
            // WindLoca1
            // 
            this.WindLoca1.Location = new System.Drawing.Point(156, 166);
            this.WindLoca1.Name = "WindLoca1";
            this.WindLoca1.Size = new System.Drawing.Size(420, 27);
            this.WindLoca1.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "接收窗口：";
            // 
            // server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 604);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.WindLoca1);
            this.Controls.Add(this.SendSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ShowDetail);
            this.Controls.Add(this.Serstatus);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.IPAddr);
            this.Controls.Add(this.closeserver);
            this.Controls.Add(this.startserver);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(1100, 200);
            this.Name = "server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "接收端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button startserver;
        private System.Windows.Forms.Button closeserver;
        private System.Windows.Forms.TextBox IPAddr;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.TextBox Serstatus;
        private System.Windows.Forms.TextBox ShowDetail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SendSize;
        private System.Windows.Forms.TextBox WindLoca1;
        private System.Windows.Forms.Label label5;
    }
}

