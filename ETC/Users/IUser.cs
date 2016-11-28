/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 15:49
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
	/// Interface for users
	/// </summary>
	public interface IUser
	{
		Task<String> GetFirstNameAsync(TelegramClient client);
		Task<String> GetLastNameAsync(TelegramClient client);
		Task<String> GetUserNameAsync(TelegramClient client);
		Task<String> GetPhoneAsync(TelegramClient client);
		Task<IConversation> GetConversationAsync(TelegramClient client);
		Task<int> GetIdAsync();
		Task<long> GetAccessHashAsync();
	}
}
