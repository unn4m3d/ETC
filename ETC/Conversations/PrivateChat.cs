﻿/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 21:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETC.Messages;
using ETC.Users;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;

namespace ETC.Conversations
{
	/// <summary>
	/// Description of PrivateChat.
	/// </summary>
	public class PrivateChat : IConversation
	{
		private TLUser m_user;
		public long UnreadCount{get;set;}
		
		public PrivateChat(TLUser user)
		{
			m_user = user;
			UnreadCount = 0L;
		}
		
		public async Task<String> GetTitleAsync(TelegramClient client)
		{
			return m_user.first_name + " " + m_user.last_name;
		}
		
		public async Task<String> GetLinkAsync(TelegramClient client)
		{
			if(m_user.username.Trim().Length == 0)
				return "";
			else
				return "https://telegram.me/" + m_user.username.Trim().Replace("@","");
		}
		
		public async Task<List<IUser>> GetParticipantsAsync(TelegramClient client)
		{
			return new List<IUser>(){UserFactory.FromUser(m_user)};
		}
		
		public async Task<int> GetParticipantsCountAsync(TelegramClient client)
		{
			return 1;
		}
		
		public async Task WriteMessageAsync(TelegramClient cli,string msg)
		{
			var peer = new TLInputPeerUser()
			{
				user_id = m_user.id,
				access_hash = await GetAccessHashAsync()
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
			return m_user.id;
		}
		
		public async Task<long> GetAccessHashAsync()
		{
			return m_user.access_hash.Value;
		}
		
		public async Task<DateTime> GetLastTimeAsync(TelegramClient cli)
		{
			return new DateTime(0L);
		}
		
		public ConversationType Type
		{
			get
			{
				return m_user.bot ?
					ConversationType.Bot :
					ConversationType.Private;
			}
		}
		
		public async Task<List<IMessage>> GetLastMessagesAsync(ClientData cli, int offset, int count)
		{
			var req = new TLRequestGetHistory()
			{
				peer = new TLInputPeerUser()
				{
					user_id = m_user.id,
					access_hash = m_user.access_hash ?? 0
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
