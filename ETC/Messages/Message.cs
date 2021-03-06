﻿/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 18:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL;
using TeleSharp.TL.Users;
using System.Threading.Tasks;
using TLSharp.Core;
using ETC.Users;
using ETC.Peers;
using ETC.Conversations;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace ETC.Messages
{
	/// <summary>
	/// Description of Message.
	/// </summary>
	public class Message : IMessage
	{
		private TLMessage m_msg;
		#region Display Data
		private String m_sender;
		private String m_text;
		#endregion
		
		public int ConversationId
		{
			get{
				Debug.WriteLine("FROM {0} TO {1}",m_msg.from_id,PeerFactory.FromPeer(m_msg.to_id).Id);
				return m_msg.from_id.HasValue ? m_msg.from_id.Value : PeerFactory.FromPeer(m_msg.to_id).Id;
			}
		}
		
		public Message(TLMessage m)
		{
			m_msg = m;
		}
		
		public async Task<IUser> GetSenderAsync(ClientData cli)
		{
			var uid = m_msg.from_id ?? PeerFactory.FromPeer(m_msg.to_id).Id;
			if(cli.UsersDict.ContainsKey(uid))
				return cli.UsersDict[uid];
			else
				return new UnsupportedUser();
		}
		
		public async Task<IConversation> GetConversationAsync(ClientData cli)
		{
			return await new Task<IConversation>(
				() => cli.Conversations.Find(x => x.GetIdAsync().Result == PeerFactory.FromPeer(m_msg.to_id).Id)
			);
		}
		
		public async Task<String> GetTextAsync(ClientData cli)
		{
			return m_msg.message;
		}
		
		public async Task PrepareForWritingAsync(ClientData cli)
		{
			var s = await GetSenderAsync(cli);
			m_sender = (await s.GetFirstNameAsync(cli.Client) + " " + await s.GetLastNameAsync(cli.Client)).Trim();
			m_text = await GetTextAsync(cli);
		}
		
		public void WriteToRichTextBox(RichTextBox box, ClientData cli)
		{
			box.SelectionColor = Color.Blue;
			box.SelectionFont = new Font(box.Font,FontStyle.Bold);
			box.AppendText("<"+m_sender+"> ");
			box.SelectionColor = Color.Black;
			box.SelectionFont = box.Font;
			box.AppendText(m_text + "\n");
		}
		
		public int SequenceNumber()
		{
			return m_msg.id;
		}
	}
}
