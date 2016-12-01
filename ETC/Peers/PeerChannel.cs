/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 23:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading.Tasks;
using TeleSharp.TL;
using ETC.Conversations;

namespace ETC.Peers
{
	/// <summary>
	/// Description of PeerChannel.
	/// </summary>
	public class PeerChannel : IPeer
	{
		private TLPeerChannel m_chat;
		
		public int Id{get{return m_chat.channel_id;}}
		public ConversationType Type{get{return ConversationType.Channel;}}
		
		public async Task<IConversation> GetConversationAsync(ClientData cli)
		{
			return cli.Conversations.Find(x => x.GetIdAsync().Result == Id);
		}
		
		public PeerChannel(TLPeerChannel c)
		{
			m_chat = c;
		}
	}
}
