/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 01.12.2016
 * Time: 21:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TeleSharp.TL.Updates;

namespace ETC.Updates
{
	/// <summary>
	/// Description of State.
	/// </summary>
	public class State
	{
		public TLState Data;
		public State(TLState s)
		{
			Data = s;
		}
	}
}
