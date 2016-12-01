/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 15:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ETC.Conversations;
using ETC.Users;
using System.Threading.Tasks;
using TLSharp.Core;
using System.Windows.Forms;

namespace ETC.Messages
{
	/// <summary>
	/// Interface for Messages
	/// </summary>
	public interface IMessage
	{
		Task<String> GetTextAsync(ClientData client);
		void WriteToRichTextBox(RichTextBox t, ClientData cli);
		Task<IUser> GetSenderAsync(ClientData cli);
		Task<IConversation> GetConversationAsync(ClientData cli);
		int SequenceNumber();
		Task PrepareForWritingAsync(ClientData cli);
	}
}
