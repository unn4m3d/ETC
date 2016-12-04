/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 01.12.2016
 * Time: 21:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL.Updates;
using ETC.Users;
using ETC.Messages;
using ETC.Conversations;
using System.Collections.Generic;

namespace ETC.Updates
{
	/// <summary>
	/// Description of EmptyChannelDifference.
	/// </summary>
	public class EmptyChannelDifference : IDifference
	{
		private TLChannelDifferenceEmpty m_diff;
		 
		public List<IUser> GetUsers()
		{
			return new List<IUser>(){};
		}
		
		public List<IMessage> GetMessages()
		{
			return new List<IMessage>(){};
		}
		
		public List<IConversation> GetConversations()
		{
			return new List<IConversation>(){};
		}
		
		public EmptyChannelDifference(TLChannelDifferenceEmpty d)
		{
			m_diff = d;
		}
		
		public State GetState()
		{
			return null;
		}
	}
}
