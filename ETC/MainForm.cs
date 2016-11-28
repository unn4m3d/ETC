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
		
		
		public List<IConversation> conversations = new List<IConversation>(){};
		private List<String> titles = new List<String>(){};
		
		internal TLVector<TLAbsUser> GetUsers(TLAbsDialogs d)
		{
			if(typeof(TLDialogs) == d.GetType())
				return (d as TLDialogs).users;
			else
				return (d as TLDialogsSlice).users;
		}
		
		internal TLVector<TLAbsChat> GetChats(TLAbsDialogs d)
		{
			if(typeof(TLDialogs) == d.GetType())
				return (d as TLDialogs).chats;
			else
				return (d as TLDialogsSlice).chats;
		}
		
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
					
					var r = Client.GetUserDialogsAsync().Result;
					
					//var users = GetUsers(r).lists;
					var chats = GetChats(r).lists;
					
				
					/*for(int i = 0; i < users.Count; i++)
					{
						var current = ConversationFactory.FromUser(users[i]);
						titles.Add(current.GetTitleAsync(Client).Result);
						conversations.Add(current);
						s.ReportProgress(50*i/users.Count);
					}*/
					
					for(int i = 0; i < chats.Count; i++)
					{
						var current = ConversationFactory.FromChat(chats[i]);
						titles.Add(current.GetTitleAsync(Client).Result);
						conversations.Add(current);
						s.ReportProgress(50*i/chats.Count + 50);
					}
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
					StatusLabel.Text = "Done";
					foreach(var t in titles)
						ChatsView.Items.Add(t);
				}
				
			};
			bw.WorkerReportsProgress = true;
			bw.RunWorkerAsync();
		}
	}
}
