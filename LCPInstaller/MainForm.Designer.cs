namespace LCPInstaller
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.installLCProxyBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dragPanel = new System.Windows.Forms.Panel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.dateTimer = new System.Windows.Forms.Timer(this.components);
            this.joinDiscordLabel = new System.Windows.Forms.Label();
            this.closeApplication = new System.Windows.Forms.Label();
            this.removeLCProxyBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // installLCProxyBtn
            // 
            this.installLCProxyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.installLCProxyBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.installLCProxyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.installLCProxyBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installLCProxyBtn.ForeColor = System.Drawing.Color.White;
            this.installLCProxyBtn.Location = new System.Drawing.Point(12, 182);
            this.installLCProxyBtn.Name = "installLCProxyBtn";
            this.installLCProxyBtn.Size = new System.Drawing.Size(477, 29);
            this.installLCProxyBtn.TabIndex = 0;
            this.installLCProxyBtn.Text = "Install LCProxy";
            this.installLCProxyBtn.UseVisualStyleBackColor = false;
            this.installLCProxyBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(202, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "lcproxy.me";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // dragPanel
            // 
            this.dragPanel.BackColor = System.Drawing.Color.LightCoral;
            this.dragPanel.Location = new System.Drawing.Point(-19, -51);
            this.dragPanel.Name = "dragPanel";
            this.dragPanel.Size = new System.Drawing.Size(576, 55);
            this.dragPanel.TabIndex = 3;
            this.dragPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragPanel_MouseDown);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.Color.Silver;
            this.timeLabel.Location = new System.Drawing.Point(180, 112);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(140, 13);
            this.timeLabel.TabIndex = 4;
            this.timeLabel.Text = "00:00:00 00 November 2021";
            // 
            // dateTimer
            // 
            this.dateTimer.Tick += new System.EventHandler(this.dateTimer_Tick);
            // 
            // joinDiscordLabel
            // 
            this.joinDiscordLabel.AutoSize = true;
            this.joinDiscordLabel.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joinDiscordLabel.ForeColor = System.Drawing.Color.Silver;
            this.joinDiscordLabel.Location = new System.Drawing.Point(181, 251);
            this.joinDiscordLabel.Name = "joinDiscordLabel";
            this.joinDiscordLabel.Size = new System.Drawing.Size(138, 13);
            this.joinDiscordLabel.TabIndex = 5;
            this.joinDiscordLabel.Text = "Need help? Join our Discord";
            this.joinDiscordLabel.Click += new System.EventHandler(this.joinDiscordLabel_Click);
            // 
            // closeApplication
            // 
            this.closeApplication.AutoSize = true;
            this.closeApplication.Font = new System.Drawing.Font("Cascadia Code", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeApplication.ForeColor = System.Drawing.Color.Silver;
            this.closeApplication.Location = new System.Drawing.Point(487, 6);
            this.closeApplication.Name = "closeApplication";
            this.closeApplication.Size = new System.Drawing.Size(13, 15);
            this.closeApplication.TabIndex = 6;
            this.closeApplication.Text = "X";
            this.closeApplication.Click += new System.EventHandler(this.closeApplication_Click);
            // 
            // removeLCProxyBtn
            // 
            this.removeLCProxyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.removeLCProxyBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.removeLCProxyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeLCProxyBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.removeLCProxyBtn.ForeColor = System.Drawing.Color.White;
            this.removeLCProxyBtn.Location = new System.Drawing.Point(12, 217);
            this.removeLCProxyBtn.Name = "removeLCProxyBtn";
            this.removeLCProxyBtn.Size = new System.Drawing.Size(477, 29);
            this.removeLCProxyBtn.TabIndex = 7;
            this.removeLCProxyBtn.Text = "Remove LCProxy";
            this.removeLCProxyBtn.UseVisualStyleBackColor = false;
            this.removeLCProxyBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(501, 273);
            this.Controls.Add(this.removeLCProxyBtn);
            this.Controls.Add(this.closeApplication);
            this.Controls.Add(this.joinDiscordLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.dragPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.installLCProxyBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Install LCProxy";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button installLCProxyBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel dragPanel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Timer dateTimer;
        private System.Windows.Forms.Label joinDiscordLabel;
        private System.Windows.Forms.Label closeApplication;
        private System.Windows.Forms.Button removeLCProxyBtn;
    }
}

