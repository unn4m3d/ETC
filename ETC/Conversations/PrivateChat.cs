/*
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
		
		public PrivateChat(TLUser user)
		{
			m_user = user;
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
		
		public async Task WriteMessageAsync(TelegramClient cli,string message)
		{
			await cli.SendMessageAsync(new TLInputPeerUser(){access_hash = m_user.access_hash ?? 0, user_id=m_user.id},message);
		}
		
		public async Task<int> GetIdAsync()
		{
			return m_user.id;
		}
		
		public async Task<long> GetAccessHashAsync()
		{
			return m_user.access_hash ?? 0;
		}
		
		public async Task<DateTime> GetLastTimeAsync(TelegramClient cli)
		{
			return new DateTime(0L);
		}
	}
}
