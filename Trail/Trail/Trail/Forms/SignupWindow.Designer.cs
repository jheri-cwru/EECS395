namespace Trail.Forms
{
    partial class SignupWindow
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
            this.applicationLogo = new System.Windows.Forms.PictureBox();
            this.passwordEntry = new System.Windows.Forms.TextBox();
            this.usernameEntry = new System.Windows.Forms.TextBox();
            this.signupButton = new System.Windows.Forms.Button();
            this.versionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.applicationLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // applicationLogo
            // 
            this.applicationLogo.Image = global::Trail.Properties.Resources.Trail_TempLogo;
            this.applicationLogo.Location = new System.Drawing.Point(102, 62);
            this.applicationLogo.Name = "applicationLogo";
            this.applicationLogo.Size = new System.Drawing.Size(180, 180);
            this.applicationLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.applicationLogo.TabIndex = 0;
            this.applicationLogo.TabStop = false;
            // 
            // passwordEntry
            // 
            this.passwordEntry.Location = new System.Drawing.Point(98, 327);
            this.passwordEntry.Name = "passwordEntry";
            this.passwordEntry.Size = new System.Drawing.Size(189, 20);
            this.passwordEntry.TabIndex = 1;
            // 
            // usernameEntry
            // 
            this.usernameEntry.Location = new System.Drawing.Point(98, 291);
            this.usernameEntry.Name = "usernameEntry";
            this.usernameEntry.Size = new System.Drawing.Size(189, 20);
            this.usernameEntry.TabIndex = 2;
            // 
            // signupButton
            // 
            this.signupButton.Location = new System.Drawing.Point(145, 376);
            this.signupButton.Name = "signupButton";
            this.signupButton.Size = new System.Drawing.Size(94, 30);
            this.signupButton.TabIndex = 3;
            this.signupButton.Text = "Link Account";
            this.signupButton.UseVisualStyleBackColor = true;
            this.signupButton.Click += new System.EventHandler(this.signupButton_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(178, 526);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(28, 13);
            this.versionLabel.TabIndex = 4;
            this.versionLabel.Text = "v1.0";
            this.versionLabel.UseMnemonic = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Enter your email and master password below to sign in / up";
            // 
            // SignupWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.signupButton);
            this.Controls.Add(this.usernameEntry);
            this.Controls.Add(this.passwordEntry);
            this.Controls.Add(this.applicationLogo);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SignupWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.applicationLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox applicationLogo;
        private System.Windows.Forms.TextBox passwordEntry;
        private System.Windows.Forms.TextBox usernameEntry;
        private System.Windows.Forms.Button signupButton;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label label2;
    }
}