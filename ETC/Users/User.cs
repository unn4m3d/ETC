/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 18:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading.Tasks;
using ETC.Conversations;
using TeleSharp.TL;
using TLSharp.Core;

namespace ETC.Users
{
	/// <summary>
	/// Description of User.
	/// </summary>
	public class User : IUser
	{
		private TLUser m_info;
		
		public User(TLUser user)
		{
			m_info = user;
		}
		
		public async Task<String> GetFirstNameAsync(TelegramClient client)
		{
			return m_info.first_name;
		}
		
		public async Task<String> GetLastNameAsync(TelegramClient client)
		{
			return m_info.last_name;
		}
		
		public async Task<String> GetUserNameAsync(TelegramClient client)
		{
			return m_info.username;
		}
		
		public async Task<String> GetPhoneAsync(TelegramClient client)
		{
			return m_info.phone;
		}
		
		public async Task<IConversation> GetConversationAsync(TelegramClient client)
		{
			return new PrivateChat(m_info);
		}
		
		public async Task<int> GetIdAsync()
		{
			return m_info.id;
		}
		
		public async Task<long> GetAccessHashAsync()
		{
			return m_info.access_hash ?? 0;
		}
	}
}
