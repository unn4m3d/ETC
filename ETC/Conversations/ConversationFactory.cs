/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 21:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using ETC.Users;

namespace ETC.Conversations
{
	/// <summary>
	/// Description of ConversationFactory.
	/// </summary>
	public class ConversationFactory
	{
		public static IConversation FromChat(TLAbsChat chat)
		{
			var type = chat.GetType();
			if(type == typeof(TLChat))
			{
				return new Chat(chat as TLChat);
			}
			else if(type == typeof(TLChannel))
			{
				return new Channel(chat as TLChannel);
			}
			else if(type == typeof(TLChatEmpty))
			{
				return new EmptyChat(chat as TLChatEmpty);
			}
			else
			{
				Debug.WriteLine("Unsupported chat type : {0}.{1}",type.Namespace,type.Name);
				return new UnsupportedChat();
			}
		}
		
		public static IConversation FromUser(TLAbsUser user)
		{
			var type = user.GetType();
			if(type == typeof(TLUser))
			{
				return new PrivateChat(user as TLUser);
			}
			else
			{
				Debug.WriteLine("Unsupported user conv type : {0}.{1}",type.Namespace,type.Name);
				return new UnsupportedChat();
			}
		}
		
		internal static TLVector<TLAbsUser> GetUsers(TLAbsDialogs d)
		{
			if(typeof(TLDialogs) == d.GetType())
				return (d as TLDialogs).users;
			else
				return (d as TLDialogsSlice).users;
		}
		
		internal static TLVector<TLAbsChat> GetChats(TLAbsDialogs d)
		{
			if(typeof(TLDialogs) == d.GetType())
				return (d as TLDialogs).chats;
			else
				return (d as TLDialogsSlice).chats;
		}
		
		public static List<IConversation> FromDialogs(TLAbsDialogs d, bool exclude_users = false)
		{
			var items = GetChats(d).lists.Select(x => FromChat(x)).ToList();
			if(!exclude_users)
			{
				items.AddRange(GetUsers(d).lists.Select(x => FromUser(x)));
			}
			return items;
		}
	}
}
