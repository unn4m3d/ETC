/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 22:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL;
using ETC.Conversations;
using System.Threading.Tasks;

namespace ETC.Peers
{
	/// <summary>
	/// Description of PeerChat.
	/// </summary>
	public class PeerChat : IPeer
	{
		private TLPeerChat m_chat;
		
		public int Id{get{return m_chat.chat_id;}}
		public ConversationType Type{get{return ConversationType.Chat;}}
		
		public async Task<IConversation> GetConversationAsync(ClientData cli)
		{
			return cli.Conversations.Find(x => x.GetIdAsync().Result == Id);
		}
		
		public PeerChat(TLPeerChat c)
		{
			m_chat = c;
		}
	}
}
