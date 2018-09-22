/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 5/21/2017
 * Time: 4:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace RBX2007_Launcher
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		static string ProcessInput(string s)
    	{
       		return s;
    	}
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new SoloForm());
		}
	}
}
