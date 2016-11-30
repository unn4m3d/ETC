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
using ETC.Users;
using TLSharp.Core;

namespace ETC
{
	/// <summary>
	/// Description of ClientData.
	/// </summary>
	public class ClientData
	{
		public List<IUser> Users;
		public TelegramClient Client;
		
		public ClientData(TelegramClient c)
		{
			Client = c;
		}
	}
}
