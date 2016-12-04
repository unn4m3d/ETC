/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 22:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Threading.Tasks;
using ETC.Messages;
using System.Collections.Generic;
using TeleSharp.TL;
using TLSharp.Core;

namespace ETC.Conversations
{
	/// <summary>
	/// Description of EmptyChat.
	/// </summary>
	public class EmptyChat : UnsupportedChat
	{
		private TLChatEmpty m_chat;
		public long UnreadCount = 0L;
		public EmptyChat(TLChatEmpty c)
		{
			m_chat = c;
			UnreadCount = 0L;
		}
		
		public override async Task<string> GetTitleAsync(TelegramClient cli)
		{
			return "[EMPTY_CHAT]"; 
		} 
		
		public override async Task<int> GetIdAsync()
		{
			return m_chat.id;
		}
		
		public override ConversationType Type
		{
			get
			{
				return ConversationType.Empty;
			}
		}
		
		public async Task<List<IMessage>> GetLastMessagesAsync(ClientData cli, int offset,int count)
		{
			return new List<IMessage>(){};
		}
	}
}
