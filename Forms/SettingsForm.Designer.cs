namespace edwirc.Forms
{
    partial class SettingsForm
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
            this.labelUInfo = new System.Windows.Forms.Label();
            this.panelUserInfo = new System.Windows.Forms.Panel();
            this.labelRealName = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.realNameBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.userBox = new System.Windows.Forms.TextBox();
            this.nickBox = new System.Windows.Forms.TextBox();
            this.labelNick = new System.Windows.Forms.Label();
            this.panelUserInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelUInfo
            // 
            this.labelUInfo.AutoSize = true;
            this.labelUInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUInfo.Location = new System.Drawing.Point(12, 9);
            this.labelUInfo.Name = "labelUInfo";
            this.labelUInfo.Size = new System.Drawing.Size(171, 16);
            this.labelUInfo.TabIndex = 0;
            this.labelUInfo.Text = "Global User Information";
            // 
            // panelUserInfo
            // 
            this.panelUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUserInfo.Controls.Add(this.labelRealName);
            this.panelUserInfo.Controls.Add(this.labelPassword);
            this.panelUserInfo.Controls.Add(this.labelUser);
            this.panelUserInfo.Controls.Add(this.realNameBox);
            this.panelUserInfo.Controls.Add(this.passwordBox);
            this.panelUserInfo.Controls.Add(this.userBox);
            this.panelUserInfo.Controls.Add(this.nickBox);
            this.panelUserInfo.Controls.Add(this.labelNick);
            this.panelUserInfo.Location = new System.Drawing.Point(15, 29);
            this.panelUserInfo.Name = "panelUserInfo";
            this.panelUserInfo.Size = new System.Drawing.Size(457, 120);
            this.panelUserInfo.TabIndex = 1;
            // 
            // labelRealName
            // 
            this.labelRealName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRealName.AutoSize = true;
            this.labelRealName.Location = new System.Drawing.Point(3, 89);
            this.labelRealName.Name = "labelRealName";
            this.labelRealName.Size = new System.Drawing.Size(61, 13);
            this.labelRealName.TabIndex = 7;
            this.labelRealName.Text = "Real name:";
            this.labelRealName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPassword
            // 
            this.labelPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(3, 63);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "Password:";
            this.labelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelUser
            // 
            this.labelUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(3, 37);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(32, 13);
            this.labelUser.TabIndex = 5;
            this.labelUser.Text = "User:";
            this.labelUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // realNameBox
            // 
            this.realNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.realNameBox.Location = new System.Drawing.Point(67, 86);
            this.realNameBox.Name = "realNameBox";
            this.realNameBox.Size = new System.Drawing.Size(387, 20);
            this.realNameBox.TabIndex = 4;
            this.realNameBox.Leave += new System.EventHandler(this.infoBox_Leave);
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordBox.Location = new System.Drawing.Point(67, 60);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(387, 20);
            this.passwordBox.TabIndex = 3;
            this.passwordBox.UseSystemPasswordChar = true;
            this.passwordBox.Leave += new System.EventHandler(this.infoBox_Leave);
            // 
            // userBox
            // 
            this.userBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userBox.Location = new System.Drawing.Point(67, 34);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(387, 20);
            this.userBox.TabIndex = 2;
            this.userBox.Leave += new System.EventHandler(this.infoBox_Leave);
            // 
            // nickBox
            // 
            this.nickBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nickBox.Location = new System.Drawing.Point(67, 8);
            this.nickBox.Name = "nickBox";
            this.nickBox.Size = new System.Drawing.Size(387, 20);
            this.nickBox.TabIndex = 1;
            this.nickBox.Leave += new System.EventHandler(this.infoBox_Leave);
            // 
            // labelNick
            // 
            this.labelNick.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNick.AutoSize = true;
            this.labelNick.Location = new System.Drawing.Point(3, 11);
            this.labelNick.Name = "labelNick";
            this.labelNick.Size = new System.Drawing.Size(58, 13);
            this.labelNick.TabIndex = 0;
            this.labelNick.Text = "Nickname:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.panelUserInfo);
            this.Controls.Add(this.labelUInfo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.panelUserInfo.ResumeLayout(false);
            this.panelUserInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUInfo;
        private System.Windows.Forms.Panel panelUserInfo;
        private System.Windows.Forms.Label labelRealName;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.TextBox realNameBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.TextBox nickBox;
        private System.Windows.Forms.Label labelNick;










    }
}