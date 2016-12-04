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
using ETC.Updates;
using TeleSharp.TL;
using TeleSharp.TL.Auth;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Updates;
using TLSharp.Core;

namespace ETC
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public TreeNode All = new TreeNode("All");
		public ClientData Data;
		public IConversation Current;
		public TreeNode Users = new TreeNode("Users");
		public TreeNode Groups = new TreeNode("Groups");
		public TreeNode Supergroups = new TreeNode("Supergroups");
		public TreeNode Channels = new TreeNode("Channels");
		public TreeNode Bots = new TreeNode("Bots");
		public TLState CurrentState;
		private object m_nodelock = new object();
		
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
		
		//public List<IConversation> Conversations = new List<IConversation>(){};
		
		void UpdateTree()
		{
			foreach(var node in Chats.Nodes)
			{
				Debug.WriteLine("Updating {0} {1}",node.GetType().Name,(node as TreeNode).Text);
				try
				{
					var nodes = Data.Conversations.Select(
						x => {
							var n = new TreeNode(x.GetTitleAsync(Data.Client).Result);
							n.Tag = x; 
							n.ForeColor = Colors[x.Type];
							return n;
						}
					).ToList();
					Debug.WriteLine((node as TreeNode).Tag.GetType());
					(node as TreeNode).UpdateFrom(nodes);
									
				}
				catch(Exception exc)
				{
					Debug.WriteLine(exc.Message);
					Debug.WriteLine(exc.StackTrace);
				}
			}
		}
		
		public MainForm(TelegramClient c)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Data = new ClientData(c);
			
			
			var bw = new BackgroundWorker();
			bw.DoWork += (sender,e) => {
				try
				{
					CurrentState = Data.Client.SendDebugRequestAsync<TLState>(new TLRequestGetState()).Result;
					var s = sender as BackgroundWorker;
					s.ReportProgress(0);
					
					var req = new TLRequestGetDialogs()
					{
						offset_date = 0,
						limit = 5000,
						offset_peer = new TLInputPeerSelf()
					};
					try
					{
						var dialogs = Data.Client.SendDebugRequestAsync<TLAbsDialogs>(req).Result;
						s.ReportProgress(10);
						Data.Conversations = ConversationFactory.FromDialogs(dialogs);
						s.ReportProgress(20);
						Data.Users = UserFactory.FromDialogs(dialogs);
						s.ReportProgress(30);
					}
					catch(Exception ex)
					{
						Debug.WriteLine(ex.StackTrace);
						Debug.WriteLine(ex.Message);
					}
					
					s.ReportProgress(90);
					Bots.Tag = new Match<TreeNode>(x => (x.Tag as IConversation).Type == ConversationType.Bot);
					s.ReportProgress(92);
					Channels.Tag = new Match<TreeNode>(x => (x.Tag as IConversation).Type == ConversationType.Channel);
					s.ReportProgress(94);
					Groups.Tag = new Match<TreeNode>(x => (x.Tag as IConversation).Type == ConversationType.Chat);
					s.ReportProgress(96);
					Users.Tag = new Match<TreeNode>(x => (x.Tag as IConversation).Type == ConversationType.Private);
					s.ReportProgress(98);
					Supergroups.Tag = new Match<TreeNode>(x => (x.Tag as IConversation).Type == ConversationType.Supergroup);
					All.Tag = new Match<TreeNode>(x => true);
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
					//All = new TreeNode("All (" + Conversations.Count + ")");
					
					Chats.Nodes.Add(All);
					Chats.Nodes.Add(Bots);
					Chats.Nodes.Add(Channels);
					Chats.Nodes.Add(Groups);
					Chats.Nodes.Add(Supergroups);
					Chats.Nodes.Add(Users);
					UpdateTree();
					StatusLabel.Text = "Done";
				}
				
				UpdateTimer.Enabled = true;
			};
			bw.WorkerReportsProgress = true;
			bw.RunWorkerAsync();
			
			UpdateTimer.Tick += (sender, e) =>
			{ 
				var t = new Task(new Action( async() => {
					var req = new TLRequestGetDifference()
					{
						pts = CurrentState.pts,
						date = CurrentState.date,
						qts = CurrentState.qts,
					};
					
					var res = await Data.Client.SendDebugRequestAsync<TLAbsDifference>(req);
					Debug.WriteLine("Got diff");
					var diff = DifferenceFactory.FromDifference(res);
					
					var added_conv = Data.AddConversations(diff.GetConversations());
					
					var messages = diff.GetMessages();
					foreach(var msg in messages)
					{
						var cid = msg.ConversationId;
						
						if(Data.ConvDict.ContainsKey(cid))
						{
							Data.ConvDict[cid].UnreadCount++;
							if(cid == Current.GetIdAsync().Result)
							{
								await msg.PrepareForWritingAsync(Data);
								this.Invoke(new Action(() => msg.WriteToRichTextBox(ChatBox,Data)));
							}
						}
						else
						{
							Debug.WriteLine("Cannot find conversation {0} [{1}]",cid,diff.GetHashCode());
						}
					}
					Debug.WriteLine("Getting state");
					var st = await Data.Client.SendDebugRequestAsync<TLState>(new TLRequestGetState());
					//lock(m_nodelock)
						CurrentState = st;
					Debug.WriteLine("Updating nodes");
					if(added_conv.Count > 0)
						this.Invoke(new Action(
							() =>{
								this.UpdateTree();
					 		}
						));
					}));
				try
				{
					t.Start();
				}
				catch(Exception ex)
				{
					Debug.WriteLine(ex.StackTrace);
					Debug.WriteLine(ex.Message);
				}
			};
			//UpdateTimer.Start();
			
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
				Current = conv;
			};
			
			bw.ProgressChanged += (sender, e) => {
				Status.Text = "Loading " + e.ProgressPercentage + "%";
			};
			
			bw.RunWorkerAsync();
		}
		
		void LogOutMenuItemClick(object sender, EventArgs e)
		{
			Debug.WriteLine("LogOut");
			Data.Client.SendDebugRequestAsync<TLAbsBool>(new TLRequestLogOut()).Wait();
			Application.Exit();
		}
		
		
		void ChatsAfterSelect(object sender, TreeViewEventArgs e)
		{
			if(e.Node.Tag != null && e.Node.Tag is IConversation)
			{
				OpenConversation(e.Node.Tag as IConversation);
			}
		}
		
		void InputKeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				Debug.WriteLine("OnKeyUp {0}",e.KeyCode.ToString());
			
				if(e.KeyCode == Keys.Return)
				{
					//lock(TelegramClientExtension.client_lock)
					{
						UpdateTimer.Enabled = false;
						Current.WriteMessageAsync(Data.Client,InputBox.Text).Wait();
						
						ChatBox.SelectionFont = new Font(ChatBox.Font,FontStyle.Bold);
						ChatBox.SelectionColor = Color.Green;
						ChatBox.AppendText("<You> ");
						ChatBox.SelectionColor = Color.Black;
						ChatBox.SelectionFont = ChatBox.Font;
						ChatBox.AppendText(InputBox.Text.Trim() + "\n");
						InputBox.Text = "";
						UpdateTimer.Enabled = true;
					}
					
				}
			}
			catch(Exception ex)	
			{
				Debug.WriteLine(ex.Message);
				Debug.WriteLine(ex.StackTrace);
			}
		}
		
	}
}
