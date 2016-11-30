/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 20:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading.Tasks;
using ETC.Conversations;

namespace ETC.Peers
{
	/// <summary>
	/// Description of UnsupportedPeer.
	/// </summary>
	public class UnsupportedPeer : IPeer
	{
		public int Id
		{
			get
			{
				return 0;
			}
		}
		
		public ConversationType Type
		{
			get
			{
				return ConversationType.Unsupported;	
			}
		}
		
		public async Task<IConversation> GetConversationAsync(ClientData cli)
		{
			return new UnsupportedChat();
		}
	}
}
