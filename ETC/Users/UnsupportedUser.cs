/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 17:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading.Tasks;
using ETC.Conversations;
using TLSharp.Core;

namespace ETC.Users
{
	/// <summary>
	/// Description of UnsupportedUser.
	/// </summary>
	public class UnsupportedUser : IUser
	{
		public UnsupportedUser()
		{
		}
		
		public virtual async Task<String> GetFirstNameAsync(TelegramClient c)
		{
			return "[UNSUPPORTED]";
		}
		
		public virtual async Task<String> GetLastNameAsync(TelegramClient c)
		{
			return "";
		}
		
		public virtual async Task<String> GetUserNameAsync(TelegramClient c)
		{
			return "";
		}
		
		public virtual async Task<String> GetPhoneAsync(TelegramClient c)
		{
			return "";
		}
		
		public virtual async Task<IConversation> GetConversationAsync(TelegramClient c)
		{
			return null;
		}
		
		public virtual async Task<int> GetIdAsync()
		{
			return 0;
		}
		
		public virtual async Task<long> GetAccessHashAsync()
		{
			return 0;
		}
	}
}
