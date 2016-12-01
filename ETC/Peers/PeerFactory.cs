/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 21:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using System.Collections.Generic;
using System.Linq;

namespace ETC.Peers
{
	/// <summary>
	/// Description of PeerFactory.
	/// </summary>
	public class PeerFactory
	{
		public static IPeer FromPeer(TLAbsPeer peer)
		{
			var type = peer.GetType();
			if(type == typeof(TLPeerUser))
			{
				return new PeerUser((peer as TLPeerUser).user_id, ConversationType.Private);
			}
			else
			{
				return new UnsupportedPeer();
			}
		}
		
		internal static List<TLAbsPeer> TLPeersFromDialogs(TLAbsDialogs d)
		{
			if(d.GetType() == typeof(TLDialogs))
			{
				return (d as TLDialogs).dialogs.lists.Select(x => x.peer).ToList();
			}
			else
			{
				return (d as TLDialogsSlice).dialogs.lists.Select(x => x.peer).ToList();
			}
		}
		
		public static List<IPeer> FromDialogs(TLAbsDialogs d)
		{
			return TLPeersFromDialogs(d).Select(x => FromPeer(x)).ToList();
		}
	}
}
