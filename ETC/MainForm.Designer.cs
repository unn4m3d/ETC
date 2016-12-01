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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.MainToolBar = new System.Windows.Forms.ToolStrip();
			this.TelegramMenu = new System.Windows.Forms.ToolStripDropDownButton();
			this.LogOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Status = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.Chats = new System.Windows.Forms.TreeView();
			this.WindowPanel = new System.Windows.Forms.TableLayoutPanel();
			this.ChatBox = new System.Windows.Forms.RichTextBox();
			this.InputBox = new System.Windows.Forms.TextBox();
			this.MainToolBar.SuspendLayout();
			this.Status.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.WindowPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainToolBar
			// 
			this.MainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.TelegramMenu});
			this.MainToolBar.Location = new System.Drawing.Point(0, 0);
			this.MainToolBar.Name = "MainToolBar";
			this.MainToolBar.Size = new System.Drawing.Size(704, 25);
			this.MainToolBar.TabIndex = 0;
			this.MainToolBar.Text = "toolStrip1";
			// 
			// TelegramMenu
			// 
			this.TelegramMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.TelegramMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.LogOutMenuItem});
			this.TelegramMenu.Image = ((System.Drawing.Image)(resources.GetObject("TelegramMenu.Image")));
			this.TelegramMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TelegramMenu.Name = "TelegramMenu";
			this.TelegramMenu.Size = new System.Drawing.Size(70, 22);
			this.TelegramMenu.Text = "Telegram";
			// 
			// LogOutMenuItem
			// 
			this.LogOutMenuItem.Name = "LogOutMenuItem";
			this.LogOutMenuItem.Size = new System.Drawing.Size(117, 22);
			this.LogOutMenuItem.Text = "Log Out";
			this.LogOutMenuItem.Click += new System.EventHandler(this.LogOutMenuItemClick);
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
			this.splitContainer1.Panel1.Controls.Add(this.Chats);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.WindowPanel);
			this.splitContainer1.Size = new System.Drawing.Size(704, 374);
			this.splitContainer1.SplitterDistance = 121;
			this.splitContainer1.TabIndex = 2;
			// 
			// Chats
			// 
			this.Chats.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Chats.Location = new System.Drawing.Point(0, 0);
			this.Chats.Name = "Chats";
			this.Chats.Size = new System.Drawing.Size(121, 374);
			this.Chats.TabIndex = 0;
			this.Chats.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ChatsAfterSelect);
			// 
			// WindowPanel
			// 
			this.WindowPanel.ColumnCount = 1;
			this.WindowPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.WindowPanel.Controls.Add(this.ChatBox, 0, 0);
			this.WindowPanel.Controls.Add(this.InputBox, 0, 1);
			this.WindowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WindowPanel.Location = new System.Drawing.Point(0, 0);
			this.WindowPanel.Name = "WindowPanel";
			this.WindowPanel.RowCount = 2;
			this.WindowPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.WindowPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.WindowPanel.Size = new System.Drawing.Size(579, 374);
			this.WindowPanel.TabIndex = 0;
			// 
			// ChatBox
			// 
			this.ChatBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChatBox.Location = new System.Drawing.Point(3, 3);
			this.ChatBox.Name = "ChatBox";
			this.ChatBox.ReadOnly = true;
			this.ChatBox.Size = new System.Drawing.Size(573, 338);
			this.ChatBox.TabIndex = 0;
			this.ChatBox.Text = "";
			// 
			// InputBox
			// 
			this.InputBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InputBox.Location = new System.Drawing.Point(3, 347);
			this.InputBox.Name = "InputBox";
			this.InputBox.Size = new System.Drawing.Size(573, 20);
			this.InputBox.TabIndex = 1;
			this.InputBox.KeyUp += InputKeyUp;
			this.InputBox.AcceptsReturn = true;
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
			this.MainToolBar.ResumeLayout(false);
			this.MainToolBar.PerformLayout();
			this.Status.ResumeLayout(false);
			this.Status.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.WindowPanel.ResumeLayout(false);
			this.WindowPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox InputBox;
		private System.Windows.Forms.RichTextBox ChatBox;
		private System.Windows.Forms.TableLayoutPanel WindowPanel;
		private System.Windows.Forms.ToolStripMenuItem LogOutMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton TelegramMenu;
		private System.Windows.Forms.TreeView Chats;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.StatusStrip Status;
		private System.Windows.Forms.ToolStrip MainToolBar;
	}
}
