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
		internal static object client_lock = new object();
		public static async Task<T> SendDebugRequestAsync<T>(this TelegramClient c,TLMethod m) where T : class
		{
			Debug.WriteLine("Sending request {0} [{1}]",m.GetType().Name,m.Constructor);
			lock(TelegramClientExtension.client_lock) // TODO: Separate locks for clients
			{
				Debug.WriteLine("Entered lock block");
				try
				{
					Debug.WriteLine("Sending...");
					var r = c.SendRequestAsync<T>(m).Result;
					Debug.WriteLine("Got response " + r.ToString());
					return r;
				}
				catch(Exception e)
				{
					Debug.WriteLine(e.StackTrace);
					Debug.WriteLine(e.Message);
					Debug.WriteLine(e.InnerException.ToString());
					return null;
				}
			}
		}
		
		public static string ToString(this TLObject obj)
		{
			return obj.Constructor.ToString();
		}
	}
}
