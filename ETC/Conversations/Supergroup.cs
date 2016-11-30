/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 15:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL;

namespace ETC.Conversations
{
	/// <summary>
	/// Description of Supergroup.
	/// </summary>
	public class Supergroup : Channel
	{
		public Supergroup(TLChannel c) : base(c){}
		
		public override async System.Threading.Tasks.Task WriteMessageAsync(TLSharp.Core.TelegramClient cli, string msg)
		{
			await cli.SendMessageAsync(new TLInputPeerChannel(){access_hash = GetAccessHashAsync().Result,channel_id=GetIdAsync().Result},msg);
		}
	}
}
