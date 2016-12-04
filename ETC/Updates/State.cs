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
		public int Pts{get;protected set;}
		public int Qts{get;protected set;}
		public int Date{get;protected set;}
		public State(int pts, int qts, int date)
		{
			Date = date;
			Pts = pts;
			Qts = qts;
		}
		
		public TLState GetState()
		{
			return new TLState(){qts = Qts, pts = Pts};
		}
	}
}
