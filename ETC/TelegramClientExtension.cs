/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 30.11.2016
 * Time: 21:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TLSharp.Core;
using TeleSharp.TL;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ETC
{
	/// <summary>
	/// Description of TelegramClientExtension.
	/// </summary>
	public static class TelegramClientExtension
	{
		public static async Task<T> SendDebugRequestAsync<T>(this TelegramClient c,TLMethod m)
		{
			Debug.WriteLine("Sending request {0} [{1}]",m.GetType().Name,m.Constructor);
			return await c.SendRequestAsync<T>(m);
		}
	}
}
