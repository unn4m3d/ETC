/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 15:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TLSharp.Core;
using System.Threading.Tasks;
using ETC.Users;
using ETC.Messages;
using System.Collections.Generic;

namespace ETC.Conversations
{
	/// <summary>
	/// Interface for conversations(chats, supergroups, channels, groups etc)
	/// </summary>
	public interface IConversation
	{
		Task<String> GetTitleAsync(TelegramClient client);
		Task<String> GetLinkAsync(TelegramClient client);
		Task<List<IUser>> GetParticipantsAsync(TelegramClient client);
		Task<int> GetParticipantsCountAsync(TelegramClient client);
		Task WriteMessageAsync(TelegramClient client, String msg);
		Task<int> GetIdAsync();
		Task<long> GetAccessHashAsync();
		Task<DateTime> GetLastTimeAsync(TelegramClient cli);
	}
}
