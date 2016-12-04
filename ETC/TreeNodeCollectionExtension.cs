/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 02.12.2016
 * Time: 18:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ETC
{
	public delegate bool Match<T>(T m);
	
	/// <summary>
	/// Description of TreeNodeCollectionExtension.
	/// </summary>
	public static class TreeNodeCollectionExtension
	{
		public static void UpdateFrom(this TreeNode c, List<TreeNode> other, Match<TreeNode> match)
		{
			c.Nodes.Clear();
			for(int i = 0; i < other.Count; i++)
			{
				if(match(other[i]))
					c.Nodes.Add(other[i]);
			}
		}
		
		public static void UpdateFrom(this TreeNode c, List<TreeNode> other)
		{
			c.UpdateFrom(other,c.Tag as Match<TreeNode>);
		}
	}
}
