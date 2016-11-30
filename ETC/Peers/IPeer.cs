/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 19:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading.Tasks;
using ETC.Conversations;

namespace ETC.Peers
{
	/// <summary>
	/// Description of IPeer.
	/// </summary>
	public interface IPeer
	{
		int Id{get;}
		ETC.ConversationType Type{get;}
		Task<IConversation> GetConversationAsync(ClientData cli);
	}
}
