/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 19:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using ETC.Conversations;
using ETC.Users;
using TLSharp.Core;
using System.Linq;

namespace ETC
{
	/// <summary>
	/// Description of ClientData.
	/// </summary>
	public class ClientData
	{
		List<IUser> m_users;
		public List<IUser> Users
		{
			get{
				return UsersDict.Values.ToList<IUser>();
			}
			set{
				//m_users = value;
				UsersDict = new Dictionary<int, IUser>(){};
				foreach(var v in value)
				{
					UsersDict.Add(v.GetIdAsync().Result,v);
				}
			}
		}
		public Dictionary<int,IConversation> ConvDict;
		public List<IConversation> Conversations
		{
			get{
				return ConvDict.Values.ToList<IConversation>();
			}
			set{
				ConvDict = new Dictionary<int, IConversation>(){};
				foreach(var c in value)
				{
					ConvDict.Add(c.GetIdAsync().Result,c);
				}
			}
		}
		public Dictionary<int,IUser> UsersDict;
		public TelegramClient Client;
		
		public ClientData(TelegramClient c)
		{
			Client = c;
		}
		
		public void AddUsers(List<IUser> users)
		{
			foreach(var u in users)
			{
				var id = u.GetIdAsync().Result;
				if(!UsersDict.ContainsKey(id))
				{					
					UsersDict.Add(id,u);
					//m_users.Add(u);
				}
			}
		}
		
		public List<IConversation> AddConversations(List<IConversation> conv)
		{
			var res = new List<IConversation>(){};
			foreach(var c in conv)
			{
				var id = c.GetIdAsync().Result;
				if(!ConvDict.ContainsKey(id))
				{
					ConvDict.Add(id,c);
					//m_conversations.Insert(0,c);
					res.Add(c);
				}
			}
			return res;
		}
	}
}
