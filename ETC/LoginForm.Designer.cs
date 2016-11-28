/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 27.11.2016
 * Time: 18:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ETC
{
	partial class LoginForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.InputText = new System.Windows.Forms.TextBox();
			this.InfoText = new System.Windows.Forms.Label();
			this.BackButton = new System.Windows.Forms.Button();
			this.NextButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// InputText
			// 
			this.InputText.Location = new System.Drawing.Point(10, 14);
			this.InputText.Name = "InputText";
			this.InputText.Size = new System.Drawing.Size(262, 20);
			this.InputText.TabIndex = 0;
			// 
			// InfoText
			// 
			this.InfoText.Location = new System.Drawing.Point(10, 37);
			this.InfoText.Name = "InfoText";
			this.InfoText.Size = new System.Drawing.Size(262, 23);
			this.InfoText.TabIndex = 1;
			this.InfoText.Text = "Please enter your phone number";
			// 
			// BackButton
			// 
			this.BackButton.Location = new System.Drawing.Point(12, 63);
			this.BackButton.Name = "BackButton";
			this.BackButton.Size = new System.Drawing.Size(125, 23);
			this.BackButton.TabIndex = 2;
			this.BackButton.Text = "Quit";
			this.BackButton.UseVisualStyleBackColor = true;
			// 
			// NextButton
			// 
			this.NextButton.Location = new System.Drawing.Point(143, 63);
			this.NextButton.Name = "NextButton";
			this.NextButton.Size = new System.Drawing.Size(129, 23);
			this.NextButton.TabIndex = 3;
			this.NextButton.Text = "Next";
			this.NextButton.UseVisualStyleBackColor = true;
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 93);
			this.Controls.Add(this.NextButton);
			this.Controls.Add(this.BackButton);
			this.Controls.Add(this.InfoText);
			this.Controls.Add(this.InputText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "LoginForm";
			this.Text = "Login";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Button BackButton;
		private System.Windows.Forms.Label InfoText;
		private System.Windows.Forms.TextBox InputText;
	}
}
