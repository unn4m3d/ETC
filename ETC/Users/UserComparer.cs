/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 01.12.2016
 * Time: 18:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using ETC.Users;

namespace ETC.Users 
{
	/// <summary>
	/// Description of UserComparer.
	/// </summary>
	public class UserComparer : IEqualityComparer<IUser>
	{
		public bool Equals(IUser a, IUser b)
		{
			return a.GetIdAsync().Result == b.GetIdAsync().Result;
		}
		
		public int GetHashCode(IUser a)
		{
			return a.GetIdAsync().Result;
		}
	}
}
