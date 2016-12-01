/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 21:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ETC.Messages;
using ETC.Users;
using TLSharp.Core;

namespace ETC.Conversations
{
	/// <summary>
	/// Description of UnsupportedChat.
	/// </summary>
	public class UnsupportedChat : IConversation
	{
		public UnsupportedChat()
		{
		}
		
		public virtual async Task<String> GetTitleAsync(TelegramClient cli)
		{
			return "[UNSUPPORTED]";
		}
		
		public async Task<List<IUser>> GetParticipantsAsync(TelegramClient cli)
		{
			return new List<IUser>(){};
		}
		
		public async Task<int> GetParticipantsCountAsync(TelegramClient cli)
		{
			return 0;
		}
		
		public async Task<String> GetLinkAsync(TelegramClient cli)
		{
			return "";
		}
		
		public async Task WriteMessageAsync(TelegramClient cli, string message)
		{
			
		}
		
		public virtual async Task<int> GetIdAsync()
		{
			return 0;
		}
		
		public virtual async Task<long> GetAccessHashAsync()
		{
			return 0L;
		}
		
		public async Task<DateTime> GetLastTimeAsync(TelegramClient cli)
		{
			return new DateTime(0L);
		}
		
		public virtual ConversationType Type
		{
			get
			{
				return ConversationType.Unsupported;
			}
		}
		
		public async Task<List<IMessage>> GetLastMessagesAsync(ClientData cli, int offset,int count)
		{
			return new List<IMessage>(){};
		}
	}
}
