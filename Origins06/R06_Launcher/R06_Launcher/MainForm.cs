/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 5/21/2017
 * Time: 4:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Origins06_Launcher
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{	
			string EXEName = System.AppDomain.CurrentDomain.FriendlyName;
			if (EXEName.Equals("Origins06_Installer.exe"))
			{
     			try
      			{
     				label1.Text = "Installing URI...";
     				string loadstring = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Origins06_Launcher.exe";
        			RegisterURLProtocol("Origins06", loadstring, "Origins06 Client");
        			progressBar1.Style = ProgressBarStyle.Blocks;
        			for (int i=0; i<100; i+=10)
					{
        				progressBar1.Value += 10;
        			}
        			label1.Text = "Installation Complete!";
        			label2.Text = "You can now play games. You may now close this window.";
      			}
      			catch (Exception)
      			{
        			label1.Text = "Installation Failed.";
        			label2.Text = "Did you launch the launcher as administrator?";
      			}		
			}
			else
			{
				if (!File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\PlayerConfig.txt"))
				{
					NameForm name = new NameForm();
					name.ShowDialog();
					System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);
				}
				else
				{
					SecurityFuncs.ReadConfigValues();
					GlobalVars.ReadyToLaunch = true;
					System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);
				}
			}
		}
		
		void StartGame()
		{
			//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog.txt", GlobalVars.SharedArgs);
			string ExtractedArg = GlobalVars.SharedArgs.Replace("origins06://", "").Replace("origins06", "").Replace("origins", "").Replace(":", "").Replace("/", "").Replace("?", "");
			//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog2.txt", ExtractedArg);
			string ConvertedArg = SecurityFuncs.Base64Decode(ExtractedArg);
			string[] SplitArg = ConvertedArg.Split('|');
			string ip = SecurityFuncs.Base64Decode(SplitArg[0]);
			bool IsValid = SecurityFuncs.checkClientMD5();
			if (IsValid == true)
			{
				//temp domain
				string luafile = GlobalVars.JoinLink;
				string exefile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Origins06_Client.exe";
				string quote = "\"";
				string args = "-script " + quote + "dofile('" + luafile + "'); _G.CSR06Connect(" + GlobalVars.UserID + ",'" + ip + "'," + SplitArg[1] + ",'" + GlobalVars.Name + "'," + SecurityFuncs.GeneratePlayerSkinColor() + "," + SecurityFuncs.GeneratePlayerLegColor() + "," + SecurityFuncs.GeneratePlayerTorsoColor() + ");" + quote;
        		Process.Start(exefile, args);
        		this.Close();
			}
			else
			{
				label1.Text = "Cannot launch client.";
        		label2.Text = "The client has been detected as modified.";
			}
		}
		
		private void CheckIfFinished(object state)
    	{
			if (GlobalVars.ReadyToLaunch == false)
			{
				System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);
			}
			else
			{
				label1.Text = "Launching Game...";
				label2.Text = "Your game is now loading...";
				StartGame();
			}
    	}
		
		private static void RegisterURLProtocol(string protocolName, string applicationPath, string description)
    	{
      		RegistryKey subKey = Registry.ClassesRoot.CreateSubKey(protocolName);
      		subKey.SetValue((string) null, (object) description);
      		subKey.SetValue("URL Protocol", (object) string.Empty);
      		Registry.ClassesRoot.CreateSubKey(protocolName + "\\Shell");
      		Registry.ClassesRoot.CreateSubKey(protocolName + "\\Shell\\open");
      		Registry.ClassesRoot.CreateSubKey(protocolName + "\\Shell\\open\\command").SetValue((string) null, (object) ("\"" + applicationPath + "\" \"%1\""));
    	}
	}
}
