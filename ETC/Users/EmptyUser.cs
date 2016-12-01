/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 28.11.2016
 * Time: 17:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;

namespace ETC.Users
{
	/// <summary>
	/// Description of EmptyUser.
	/// </summary>
	public class EmptyUser : UnsupportedUser
	{
		
		private int m_id;
		
		public EmptyUser(TLUserEmpty u)
		{
			m_id = u.id;
		}
		
		public override async Task<String> GetFirstNameAsync(TelegramClient c)
		{
			return await new Task<String>(
				()=>"[EMPTY : " + m_id + "]"
			);
		}
		
		public override async Task<int> GetIdAsync()
		{
			return await new Task<int>(()=>m_id);
		}
	
	}
}
