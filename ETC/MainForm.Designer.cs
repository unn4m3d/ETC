/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 27.11.2016
 * Time: 17:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ETC
{
	partial class MainForm
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
			this.MainToolBar = new System.Windows.Forms.ToolStrip();
			this.Status = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ChatsView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.Status.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainToolBar
			// 
			this.MainToolBar.Location = new System.Drawing.Point(0, 0);
			this.MainToolBar.Name = "MainToolBar";
			this.MainToolBar.Size = new System.Drawing.Size(704, 25);
			this.MainToolBar.TabIndex = 0;
			this.MainToolBar.Text = "toolStrip1";
			// 
			// Status
			// 
			this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.StatusLabel});
			this.Status.Location = new System.Drawing.Point(0, 399);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(704, 22);
			this.Status.TabIndex = 1;
			this.Status.Text = "StatusStrip";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(59, 17);
			this.StatusLabel.Text = "Loading...";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 25);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ChatsView);
			this.splitContainer1.Size = new System.Drawing.Size(704, 374);
			this.splitContainer1.SplitterDistance = 121;
			this.splitContainer1.TabIndex = 2;
			// 
			// ChatsView
			// 
			this.ChatsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1});
			this.ChatsView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChatsView.GridLines = true;
			this.ChatsView.Location = new System.Drawing.Point(0, 0);
			this.ChatsView.Name = "ChatsView";
			this.ChatsView.Size = new System.Drawing.Size(121, 374);
			this.ChatsView.TabIndex = 0;
			this.ChatsView.UseCompatibleStateImageBehavior = false;
			this.ChatsView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Chats";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(704, 421);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.Status);
			this.Controls.Add(this.MainToolBar);
			this.Name = "MainForm";
			this.Text = "ETC";
			this.Status.ResumeLayout(false);
			this.Status.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.ListView ChatsView;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.StatusStrip Status;
		private System.Windows.Forms.ToolStrip MainToolBar;
	}
}
