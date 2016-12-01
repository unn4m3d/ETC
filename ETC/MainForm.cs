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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using ETC.Conversations;
using ETC.Peers;
using ETC.Users;
using ETC.Messages;
using TeleSharp.TL;
using TeleSharp.TL.Auth;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Messages;
using TLSharp.Core;

namespace ETC
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public TelegramClient Client;
		public TreeNode All;
		public ClientData Data;
		
		public Dictionary<ConversationType,Color> Colors = new Dictionary<ConversationType, Color>()
		{
			{ConversationType.Empty,		Color.Black},
			{ConversationType.Chat,			Color.Blue},
			{ConversationType.Private, 		Color.SaddleBrown},
			{ConversationType.Channel,  	Color.Green},
			{ConversationType.Supergroup, 	Color.DarkViolet},
			{ConversationType.Unsupported,  Color.Red},
			{ConversationType.Bot, 			Color.Cyan}
		};
		
		public List<IConversation> Conversations = new List<IConversation>(){};
		
		public MainForm(TelegramClient c)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Client = c;	
			Data = new ClientData(c);
			
			var users = new TreeNode("Users");
			var groups = new TreeNode("Groups");
			var supergroups = new TreeNode("Supergroups");
			var channels = new TreeNode("Channels");
			var bots = new TreeNode("Bots");
			
			
			var bw = new BackgroundWorker();
			bw.DoWork += (sender,e) => {
				try
				{
					var s = sender as BackgroundWorker;
					s.ReportProgress(0);
					
					var req = new TLRequestGetDialogs()
					{
						offset_date = 0,
						offset_peer = new TLInputPeerSelf(),
						limit = 5000,
					};
					
					var dialogs = Client.SendDebugRequestAsync<TLAbsDialogs>(req).Result;
					s.ReportProgress(10);
					Conversations = ConversationFactory.FromDialogs(dialogs);
					Data.Conversations = Conversations;
					s.ReportProgress(20);
					Data.Users = UserFactory.FromDialogs(dialogs);
					s.ReportProgress(30);
					
					for(int i = 0; i < Conversations.Count; i++)
					{
						var conv = Conversations[i];
						var node = new TreeNode(conv.GetTitleAsync(Client).Result);
						node.Tag = conv;
						node.ForeColor = Colors[conv.Type];
						switch(conv.Type)
						{
							case ConversationType.Bot:
								bots.Nodes.Add(node);
								break;
							case ConversationType.Channel:
								channels.Nodes.Add(node);
								break;
							case ConversationType.Chat:
								groups.Nodes.Add(node);
								break;
							case ConversationType.Private:
								users.Nodes.Add(node);
								break;
							case ConversationType.Supergroup:
								supergroups.Nodes.Add(node);
								break;
						}
						s.ReportProgress((90-30)*i/Conversations.Count + 30);
					}
					s.ReportProgress(90);
					bots.Text += " (" + bots.Nodes.Count + ")";
					s.ReportProgress(92);
					channels.Text += " (" + channels.Nodes.Count + ")";
					s.ReportProgress(94);
					groups.Text += " (" + groups.Nodes.Count + ")";
					s.ReportProgress(96);
					users.Text += " (" + users.Nodes.Count + ")";
					s.ReportProgress(98);
					supergroups.Text += " (" + supergroups.Nodes.Count + ")";
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
						node.ForeColor = Colors[conv.Type];
						All.Nodes.Add(node);
					}
					Chats.Nodes.Add(All);
					Chats.Nodes.Add(bots);
					Chats.Nodes.Add(channels);
					Chats.Nodes.Add(groups);
					Chats.Nodes.Add(supergroups);
					Chats.Nodes.Add(users);
				}
				
			};
			bw.WorkerReportsProgress = true;
			bw.RunWorkerAsync();
		}
		
		void OpenConversation(IConversation conv)
		{
			var bw = new BackgroundWorker();
			bw.WorkerReportsProgress = true;
			var m = new List<IMessage>(){};
			bw.DoWork += (sender,e) => {
				m =  conv.GetLastMessagesAsync(Data,0,50).Result;
				var s = sender as BackgroundWorker;
				s.ReportProgress(10);
				m.Reverse();
				s.ReportProgress(25);
				for(int i = 0; i < m.Count; i++)
				{
					m[i].PrepareForWritingAsync(Data).Wait();
					s.ReportProgress((100-25)*i/m.Count + 25);
				}
				s.ReportProgress(100);
			};
			Status.Text = "Loading...";
			bw.RunWorkerCompleted += (sender,e) => {
				Status.Text = "Done";
				ChatBox.Text  ="";
				foreach(var msg in m)
				{
					Debug.Write(".");
					msg.WriteToRichTextBox(ChatBox,Data);
				}
				Debug.WriteLine("");
				ChatBox.ScrollToCaret();
			};
			
			bw.ProgressChanged += (sender, e) => {
				Status.Text = "Loading " + e.ProgressPercentage + "%";
			};
			
			bw.RunWorkerAsync();
		}
		
		void LogOutMenuItemClick(object sender, EventArgs e)
		{
			Debug.WriteLine("LogOut");
			Client.SendDebugRequestAsync<TLAbsBool>(new TLRequestLogOut()).Wait();
			Application.Exit();
		}
		
		
		void ChatsAfterSelect(object sender, TreeViewEventArgs e)
		{
			if(e.Node.Tag != null && e.Node.Tag is IConversation)
			{
				OpenConversation(e.Node.Tag as IConversation);
			}
		}
	}
}
