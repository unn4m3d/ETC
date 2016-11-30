/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 18:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL;
using TeleSharp.TL.Users;
using System.Threading.Tasks;
using TLSharp.Core;
using ETC.Users;

namespace ETC.Messages
{
	/// <summary>
	/// Description of Message.
	/// </summary>
	public class Message : IMessage
	{
		private TLMessage m_msg;
		
		public Message(TLMessage m)
		{
			m_msg = m;
		}
		
		public async Task<IUser> GetSenderAsync(TelegramClient cli)
		{
		
		}
	}
}
