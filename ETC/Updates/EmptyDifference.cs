/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 01.12.2016
 * Time: 20:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL.Updates;
using System.Collections.Generic;
using ETC.Messages;
using ETC.Users;
using ETC.Conversations;

namespace ETC.Updates
{
	/// <summary>
	/// Description of EmptyDifference.
	/// </summary>
	public class EmptyDifference : IDifference
	{
		private int m_date;
		private int m_seq;
		
		public EmptyDifference(TLDifferenceEmpty d)
		{
			m_date = d.date;
			m_seq = d.seq;
		}
		
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
	}
}
