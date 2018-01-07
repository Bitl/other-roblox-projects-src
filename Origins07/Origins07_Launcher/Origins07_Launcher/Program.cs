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

namespace Origins07_Launcher
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
			string EXEName = System.AppDomain.CurrentDomain.FriendlyName;
			if (EXEName.Equals("Origins07_Launcher.exe") || EXEName.Equals("Origins07_DedicatedServer.exe"))
			{
				foreach (string s in args)
      			{
        			GlobalVars.SharedArgs = ProcessInput(s);
      			}
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (EXEName.Equals("Origins07_DedicatedServer.exe"))
			{
				Application.Run(new DedicatedServerForm());
			}
			else if (EXEName.Equals("Origins07_Customizer.exe"))
			{
				Application.Run(new NameForm());
			}
			else if (EXEName.Equals("Origins07_PlaySolo.exe"))
			{
				Application.Run(new SoloForm());
			}
			else
			{
				Application.Run(new MainForm());
			}
		}
	}
}
