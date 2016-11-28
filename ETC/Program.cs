/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 27.11.2016
 * Time: 17:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace ETC
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		internal static TLSharp.Core.TelegramClient client;
		private const int ApiID = 30541;
		private const string ApiHash = "7b9938d28746c8e5f122e83b7d8f7a90";
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			
			client = new TLSharp.Core.TelegramClient(ApiID,ApiHash,new TLSharp.Core.FileSessionStore(),"user"); // TODO: Fix this
			
			client.ConnectAsync().Wait();
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			if(client.IsUserAuthorized())
				Application.Run(new MainForm(client));
			else
				Application.Run(new LoginForm(client));
		}
		
	}
}
