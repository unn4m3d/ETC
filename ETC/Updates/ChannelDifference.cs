/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 01.12.2016
 * Time: 22:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL.Updates;
using System.Collections.Generic;
using ETC.Users;
using ETC.Conversations;
using ETC.Messages;
using System.Linq;

namespace ETC.Updates
{
	/// <summary>
	/// Description of ChannelDifference.
	/// </summary>
	public class ChannelDifference : IDifference
	{
		private TLChannelDifference m_diff;
		
		public ChannelDifference(TLChannelDifference d)
		{
			m_diff = d;
		}
		
		public List<IConversation> GetConversations()
		{
			return m_diff.chats.lists.Select(x => ConversationFactory.FromChat(x)).ToList();
		}
		
		public List<IUser> GetUsers()
		{
			return m_diff.users.lists.Select(x => UserFactory.FromUser(x)).ToList();
		}
		
		public List<IMessage> GetMessages()
		{
			return m_diff.new_messages.lists.Select(x => MessageFactory.FromMessage(x)).ToList();
		}
		
		public State GetState()
		{
			return new State(
				new TLState()
				{
			    	pts = m_diff.pts,
					qts = 0,
					seq = 0					
			    }
			);
		}
	}
}
