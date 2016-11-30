/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 27.11.2016
 * Time: 17:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using TeleSharp.TL;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Messages;
using TLSharp.Core;
using ETC.Conversations;

namespace ETC
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public TelegramClient Client;
		public TreeNode All;
		
		public Dictionary<Type,Color> Colors = new Dictionary<Type, Color>()
		{
			{typeof(PrivateChat),		Color.Blue},
			{typeof(Channel),			Color.Green},
			{typeof(EmptyChat),     	Color.DarkGray},
			{typeof(UnsupportedChat),	Color.Red},
			{typeof(Chat),				Color.Purple},
			{typeof(Supergroup),		Color.HotPink}
		};
		
		public List<IConversation> Conversations = new List<IConversation>(){};
		
		public MainForm(TelegramClient c)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Client = c;	
			var bw = new BackgroundWorker();
			bw.DoWork += (sender,e) => {
				try
				{
					var s = sender as BackgroundWorker;
					s.ReportProgress(0);
					
					Conversations = ConversationFactory.FromDialogs(Client.GetUserDialogsAsync().Result);
					s.ReportProgress(100);
				}
				catch(Exception ex)
				{
					Debug.WriteLine(ex.StackTrace);
					Debug.WriteLine(ex.Message);
				}
			};
			
			bw.ProgressChanged += (sender,args) => {
				StatusLabel.Text = "Loading... " + args.ProgressPercentage + "%";
			};
			
			bw.RunWorkerCompleted += (sender,args) => {
				if(args.Error != null)
				{
					Debug.WriteLine(args.Error.Source);
					Debug.WriteLine(args.Error.StackTrace);
					Debug.WriteLine(args.Error.Message);
					throw new Exception("Error!",args.Error);
				}
				else
				{
					All = new TreeNode("All (" + Conversations.Count + ")");
					StatusLabel.Text = "Done";
					foreach(var conv in Conversations)
					{
						var node = new TreeNode(conv.GetTitleAsync(Client).Result);
						node.Tag = conv;
						node.ForeColor = Colors[conv.GetType()];
						All.Nodes.Add(node);
					}
					Chats.Nodes.Add(All);
				}
				
			};
			bw.WorkerReportsProgress = true;
			bw.RunWorkerAsync();
		}
	}
}
