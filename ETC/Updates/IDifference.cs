/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 01.12.2016
 * Time: 20:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ETC.Users;
using ETC.Conversations;
using ETC.Messages;
using System.Collections.Generic;

namespace ETC.Updates
{
	/// <summary>
	/// Description of IDifference.
	/// </summary>
	public interface IDifference
	{
		List<IUser> GetUsers();
		List<IMessage> GetMessages();
		List<IConversation> GetConversations();
	}
}
