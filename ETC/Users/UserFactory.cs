/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 16:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using TeleSharp.TL.Messages;
using TeleSharp.TL;
using TeleSharp.TL.Channels;
using TeleSharp.TL.Users;
using System.Diagnostics;

namespace ETC.Users
{
	/// <summary>
	/// Description of UserFactory.
	/// </summary>
	public class UserFactory
	{
		public static List<IUser> FromChannelParticipants(TLChannelParticipants p)
		{
			return p.users.lists.Select(x => UserFactory.FromUser(x)).ToList();
		}
		
		public static IUser FromUser(TLAbsUser user)
		{
			var type = user.GetType();
			if(type == typeof(TLUserEmpty))
			{
				return new EmptyUser(user as TLUserEmpty);
			}
			else if(type == typeof(TLUser))
			{
				return new User(user as TLUser);
			}
			else
			{
				Debug.WriteLine("Unknown user type {0} [{1}]",type.Name,user.Constructor);
				return new UnsupportedUser();
			}
		}
		
		public static List<IUser> FromDialogs(TLAbsDialogs d)
		{
			var users = Conversations.ConversationFactory.GetUsers(d);
			return users.lists.Select(x => FromUser(x)).ToList();
		}
		
		internal static List<TLAbsUser> TLUsersFromMessages(TLAbsMessages m)
		{
			if(m.GetType() == typeof(TLMessages))
				return (m as TLMessages).users.lists;
			else if(m.GetType() == typeof(TLMessagesSlice))
				return (m as TLMessagesSlice).users.lists;
			else if(m.GetType() == typeof(TLChannelMessages))
				return (m as TLChannelMessages).users.lists;
			else
			{
				Debug.WriteLine("Unknown Messages Type : {0} ({1})", m.GetType().Name, m.Constructor);
				return new List<TLAbsUser>();
			}
		}
		
		public static List<IUser> FromMessages(TLAbsMessages m)
		{
			return TLUsersFromMessages(m).Select(x => FromUser(x)).ToList();
		}
	}
}
