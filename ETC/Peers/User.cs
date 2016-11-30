/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 21:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading.Tasks;
using ETC.Conversations;
using TeleSharp.TL;
using TeleSharp.TL.Users;

namespace ETC.Peers
{
	/// <summary>
	/// Description of User.
	/// </summary>
	public class User : IPeer
	{
		public int Id{get; protected set;}
		public ConversationType Type
		{
			get;
			protected set;
		}
		
		public User(int id, ConversationType type = ConversationType.Private)
		{
			Id = id;
			Type = type;
		}
		
		public async Task<IConversation> GetConversationAsync(ClientData cli)
		{
			var access_hash = await cli.Users.Find(x => x.GetIdAsync().Result == Id).GetAccessHashAsync();
			var req = new TLRequestGetUsers()
			{
				id = new TLVector<TLAbsInputUser>()
				{
					lists = {
						new TLInputUser()
						{
							user_id = Id,
							access_hash = access_hash
						}
					}
				},
			};
			
			
			var res = await cli.Client.SendRequestAsync<TLVector<TLAbsUser>>(req);
			return ConversationFactory.FromUser(res.lists[0]);
			
		}
	}
}
