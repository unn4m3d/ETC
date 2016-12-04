/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 01.12.2016
 * Time: 21:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using TeleSharp.TL.Updates;

namespace ETC.Updates
{
	/// <summary>
	/// Description of DifferenceFactory.
	/// </summary>
	public class DifferenceFactory
	{
		public static IDifference FromDifference(TLAbsDifference diff)
		{
			if(diff is TLDifference)
				return new Difference(diff as TLDifference);
			else if(diff is TLDifferenceEmpty)
				return new EmptyDifference(diff as TLDifferenceEmpty);
			else
				return new DifferenceSlice(diff as TLDifferenceSlice);
		}
		
		public static IDifference FromChannelDifference(TLAbsChannelDifference diff)
		{
			Debug.WriteLine("{0} [isEmpty:{1} {2}]",diff.GetType().Name,diff is TLChannelDifferenceEmpty,diff.Constructor);
			if(diff is TLChannelDifference)
				return new ChannelDifference(diff as TLChannelDifference);
			else if(diff is TLChannelDifferenceTooLong)
				return new ChannelDifferenceTooLong(diff as TLChannelDifferenceTooLong);
			else
				return new EmptyChannelDifference(diff as TLChannelDifferenceEmpty);
		}
	}
}
