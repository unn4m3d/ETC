﻿/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 22:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Threading.Tasks;

using TeleSharp.TL;
using TLSharp.Core;

namespace ETC.Conversations
{
	/// <summary>
	/// Description of EmptyChat.
	/// </summary>
	public class EmptyChat : UnsupportedChat
	{
		private TLChatEmpty m_chat;
		public EmptyChat(TLChatEmpty c)
		{
			m_chat = c;
		}
		
		public override async Task<string> GetTitleAsync(TelegramClient cli)
		{
			return "[EMPTY_CHAT " + m_chat.id + "]"; 
		} 
		
		public override async Task<int> GetIdAsync()
		{
			return m_chat.id;
		}
		
		public override ConversationType Type
		{
			get
			{
				return ConversationType.Empty;
			}
		}
	}
}
