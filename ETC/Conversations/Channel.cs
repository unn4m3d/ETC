/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 21:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ETC.Users;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Channels;
using TLSharp.Core;
using System.Collections.Generic;
using System.Linq;

namespace ETC.Conversations
{
	/// <summary>
	/// Description of Channel.
	/// </summary>
	public class Channel : IConversation
	{
		private TLChannel m_channel;
		
		public Channel(TLChannel c)
		{
			m_channel = c;
		}
		
		public async Task<String> GetTitleAsync(TelegramClient cli)
		{
			return m_channel.title;
		}
		
		public async Task<String> GetUserNameAsync(TelegramClient cli)
		{
			return m_channel.username;
		}
		
		public async Task<List<IUser>> GetParticipantsAsync(TelegramClient cli)
		{
			var req = new TLRequestGetParticipants()
			{
				channel = new TLInputChannel()
				{
					channel_id = m_channel.id,
					access_hash = m_channel.access_hash ?? 0
				},
				offset = 0,
				limit  = 5000
			};
			
			var res = await cli.SendRequestAsync<TLChannelParticipants>(req);
			return res.users.lists.Select(x => UserFactory.FromUser(x)).ToList();
		}
		
		public async Task<int> GetParticipantsCountAsync(TelegramClient cli)
		{
			return (await GetParticipantsAsync(cli)).Count; // FIXME
		}
		
		public async Task<int> GetIdAsync()
		{
			return m_channel.id;
		}
		
		public async Task<long> GetAccessHashAsync()
		{
			return m_channel.access_hash ?? 0;
		}
		
		public virtual async Task WriteMessageAsync(TelegramClient cli, string msg)
		{
			// await cli.SendMessageAsync(new TLInputPeerChannel(){access_hash = m_channel.access_hash ?? 0,channel_id=m_channel.id},msg);		
		}
		
		public async Task<String> GetLinkAsync(TelegramClient cli)
		{
			if(m_channel.username.Trim().Length == 0)
				return "";
			else
				return "https://telegram.me/" + m_channel.username.Trim().Replace("@","");
		}
		
		public async Task<DateTime> GetLastTimeAsync(TelegramClient cli)
		{
			return new DateTime((long)m_channel.date);
		}
		
		public ConversationType Type
		{
			get
			{
				return m_channel.megagroup ?
					ConversationType.Supergroup :
					ConversationType.Channel;
			}
		}
		
	}
}
