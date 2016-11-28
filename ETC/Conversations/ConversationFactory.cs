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
using System.Diagnostics;
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
	}
}
