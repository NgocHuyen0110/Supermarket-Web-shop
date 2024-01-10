namespace RobertHejinDesktop
{
	partial class LoginFormcs
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
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPasswod = new System.Windows.Forms.Label();
            this.tbPasswordLogin = new System.Windows.Forms.TextBox();
            this.tbEmailLogin = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(498, 55);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(135, 32);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "Login Form";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(168, 213);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(76, 32);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email:";
            // 
            // lblPasswod
            // 
            this.lblPasswod.AutoSize = true;
            this.lblPasswod.Location = new System.Drawing.Point(168, 351);
            this.lblPasswod.Name = "lblPasswod";
            this.lblPasswod.Size = new System.Drawing.Size(116, 32);
            this.lblPasswod.TabIndex = 2;
            this.lblPasswod.Text = "Password:";
            // 
            // tbPasswordLogin
            // 
            this.tbPasswordLogin.Location = new System.Drawing.Point(401, 344);
            this.tbPasswordLogin.Name = "tbPasswordLogin";
            this.tbPasswordLogin.Size = new System.Drawing.Size(358, 39);
            this.tbPasswordLogin.TabIndex = 3;
            this.tbPasswordLogin.UseSystemPasswordChar = true;
            // 
            // tbEmailLogin
            // 
            this.tbEmailLogin.Location = new System.Drawing.Point(401, 206);
            this.tbEmailLogin.Name = "tbEmailLogin";
            this.tbEmailLogin.Size = new System.Drawing.Size(358, 39);
            this.tbEmailLogin.TabIndex = 4;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(483, 486);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(150, 46);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // LoginFormcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 693);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbEmailLogin);
            this.Controls.Add(this.tbPasswordLogin);
            this.Controls.Add(this.lblPasswod);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblLogin);
            this.Name = "LoginFormcs";
            this.Text = "LoginFormcs";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Label lblLogin;
		private Label lblEmail;
		private Label lblPasswod;
		private TextBox tbPasswordLogin;
		private TextBox tbEmailLogin;
		private Button btnLogin;
	}
}