/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 01.12.2016
 * Time: 21:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL.Updates;
using System.Collections.Generic;
using ETC.Messages;
using ETC.Users;
using ETC.Conversations;
using System.Linq;

namespace ETC.Updates
{
	/// <summary>
	/// Description of Difference.
	/// </summary>
	public class Difference : IDifference
	{
		private TLDifference m_diff;
		
		public Difference(TLDifference d)
		{
			m_diff = d;
		}
		
		public List<IUser> GetUsers()
		{
			return m_diff.users.lists.Select(x => UserFactory.FromUser(x)).ToList();
		}
		
		public List<IMessage> GetMessages()
		{
			return m_diff.new_messages.lists.Select(x => MessageFactory.FromMessage(x)).ToList();
		}
		
		public List<IConversation> GetConversations()
		{
			return m_diff.chats.lists.Select(x => ConversationFactory.FromChat(x)).ToList();
		}
	}
}
