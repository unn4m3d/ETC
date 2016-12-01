/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 23:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using ETC.Conversations;
using ETC.Users;

namespace ETC.Messages
{
	/// <summary>
	/// Description of UnsupportedMessage.
	/// </summary>
	public class UnsupportedMessage : IMessage
	{
		public async Task<String> GetTextAsync(ClientData cli)
		{
			return "";
		}
		
		public async Task<IUser> GetSenderAsync(ClientData cli)
		{
			return new UnsupportedUser();
		}
		
		public async Task PrepareForWritingAsync(ClientData cli)
		{
			/* Nothing to do here */
		}
		
		public void WriteToRichTextBox(RichTextBox box, ClientData cli)
		{
			box.SelectionFont = new Font(box.Font,FontStyle.Bold);
			box.SelectionColor = Color.Red;
			box.AppendText("[UNSUPPORTED MESSAGE]\n");
			box.SelectionFont = box.Font;
			box.SelectionColor = Color.Black;
		}
		
		
		public async Task<IConversation> GetConversationAsync(ClientData cli)
		{
			return new UnsupportedChat();
		}
		
		public int SequenceNumber()
		{
			return 0;
		}
	}
}
