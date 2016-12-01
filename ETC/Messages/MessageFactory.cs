/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 01.12.2016
 * Time: 13:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TeleSharp.TL.Channels;
using TeleSharp.TL;
using TeleSharp.TL.Messages;

namespace ETC.Messages
{
	/// <summary>
	/// Description of MessageFactory.
	/// </summary>
	public class MessageFactory
	{
		public static IMessage FromMessage(TLAbsMessage m)
		{
			var type = m.GetType();
			if(type == typeof(TLMessage))
			{
				return new Message(m as TLMessage);
			}
			else
			{
				return new UnsupportedMessage();
			}
		}
		
		internal static List<TLAbsMessage> AbsMessageList(TLAbsMessages m)
		{
			if(m.GetType() == typeof(TLMessages))
				return (m as TLMessages).messages.lists;
			else if(m.GetType() == typeof(TLMessagesSlice))
				return (m as TLMessagesSlice).messages.lists;
			else if(m.GetType() == typeof(TLChannelMessages))
				return (m as TLChannelMessages).messages.lists;
			else
			{
				Debug.WriteLine("Unknown Messages Type : {0} ({1})", m.GetType().Name, m.Constructor);
				return new List<TLAbsMessage>();
			}
		}
		
		public static List<IMessage> FromMessages(TLAbsMessages m)
		{
			return AbsMessageList(m).Select(x => FromMessage(x)).ToList();
		}
	}
}
