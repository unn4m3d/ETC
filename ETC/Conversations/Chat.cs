/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 16:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using ETC.Messages;
using ETC.Users;
using TeleSharp.TL;
using TeleSharp.TL.Channels;
using TeleSharp.TL.Messages;
using TLSharp.Core;

namespace ETC.Conversations
{
	/// <summary>
	/// Description of Chat.
	/// </summary>
	public class Chat : IConversation
	{
		private TLChat m_chat;
		public long UnreadCount{get;set;}
		
		public Chat(TLChat c)
		{
			m_chat = c;
			UnreadCount = 0L;
		}
		
		public async Task<String> GetLinkAsync(TelegramClient cli)
		{
			return "";
		}

		public async Task<String> GetTitleAsync(TelegramClient client)
		{
			return m_chat.title;
		}
		
		public async Task<int> GetParticipantsCountAsync(TelegramClient client)
		{
			return m_chat.participants_count;
		}
		
		public async Task<List<IUser>> GetParticipantsAsync(TelegramClient client)
		{
			var users = new List<IUser>(){};
			try
			{
				var req = new TLRequestGetParticipants()
				{
					channel = m_chat.migrated_to,
					offset = 0,
					limit  = m_chat.participants_count					
				};
				
				var res = await client.SendDebugRequestAsync<TLChannelParticipants>(req);
				users = UserFactory.FromChannelParticipants(res);
			}
			catch(Exception e)
			{
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
			}
			return users;
		}
		
		public async Task WriteMessageAsync(TelegramClient cli, String msg)
		{
			var peer = new TLInputPeerChat()
			{
				chat_id = m_chat.id
			};
			var req = new TLRequestSendMessage()
			{
				peer = peer,
				message = msg,
				random_id = TLSharp.Core.Utils.Helpers.GenerateRandomLong()
			};
			new Task(new Action(() => {
			                    	cli.SendDebugRequestAsync<object>(req).Wait();
			                    })).Start();
		}
		
		public async Task<int> GetIdAsync()
		{
			return m_chat.id;
		}
		
		public async Task<long> GetAccessHashAsync()
		{
			return 0L;
		}
		
		public async Task<DateTime> GetLastTimeAsync(TelegramClient cli)
		{
			return new DateTime((long)m_chat.date);
		}
		
		public ConversationType Type
		{
			get
			{
				return ConversationType.Chat;
			}
		}
		
		public async Task<List<IMessage>> GetLastMessagesAsync(ClientData cli, int offset, int count)
		{
			var req = new TLRequestGetHistory()
			{
				peer = new TLInputPeerChat()
				{
					chat_id = m_chat.id,
				},
				offset_id = offset,
				limit = count
			};
			var res = await cli.Client.SendDebugRequestAsync<TLAbsMessages>(req);
			cli.AddUsers(UserFactory.FromMessages(res));
			return MessageFactory.FromMessages(res);
		}
	}
}
