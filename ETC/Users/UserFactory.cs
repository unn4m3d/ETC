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
				return new UnsupportedUser();
			}
		}
		
		public static List<IUser> FromDialogs(TLAbsDialogs d)
		{
			var users = Conversations.ConversationFactory.GetUsers(d);
			return users.lists.Select(x => FromUser(x)).ToList();
		}
	}
}
