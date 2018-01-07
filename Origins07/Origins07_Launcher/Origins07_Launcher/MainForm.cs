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

namespace Origins07_Launcher
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
			if (EXEName.Equals("Origins07_Installer.exe"))
			{
     			try
      			{
     				label1.Text = "Installing...";
     				string loadstring = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Origins07_Launcher.exe";
     				string loadstring2 = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Origins07_DedicatedServer.exe";
        			RegisterURLProtocol("origins07", loadstring, "Origins07 Client");
        			RegisterURLProtocol("origins07local", loadstring, "Origins07 Play Solo Mode");
        			RegisterURLProtocol("origins07server", loadstring2, "Origins07 Dedicated Server");
        			progressBar1.Style = ProgressBarStyle.Blocks;
        			for (int i=0; i<100; i+=10)
					{
        				progressBar1.Value += 10;
        			}
        			label1.Text = "Installation Complete!";
        			label2.Text = "Congratulations, you can now play games!";
      			}
      			catch (Exception)
      			{
        			label1.Text = "Installation Failed.";
        			label2.Text = "Did you launch the launcher as an administrator?";
      			}		
			}
			else
			{
				if (!File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + GlobalVars.Config))
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
			if (GlobalVars.SharedArgs.Contains("origins07local://"))
			{
				string ExtractedArg = "";
				//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog.txt", GlobalVars.SharedArgs);
				ExtractedArg = GlobalVars.SharedArgs.Replace("origins07local://", "").Replace("origins07local", "").Replace("origins07", "").Replace("origins", "").Replace(":", "").Replace("/", "").Replace("?", "");
				//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog2.txt", ExtractedArg);
				string ConvertedArg = SecurityFuncs.Base64Decode(ExtractedArg);
				ScriptType type = ScriptType.Solo;
            	ScriptGenerator.GenerateScriptForClient(type);
				bool IsValid = SecurityFuncs.checkClientMD5();
				if (IsValid == true)
				{
					//temp domain
					string exefile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Origins07_Client.exe";
					string quote = "\"";
					string args = "-script " + quote + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + ScriptGenerator.GetScriptNameForType(type) + quote  + " " + quote + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\maps\\" + ConvertedArg + quote;
        			Process.Start(exefile, args);
        			this.Close();
				}
				else
				{
					label1.Text = "Cannot Launch Game.";
        			label2.Text = "The client has been detected as modified.";
				}
			}
			else
			{
				string ExtractedArg = "";
				ExtractedArg = GlobalVars.SharedArgs.Replace("origins07://", "").Replace("origins07", "").Replace("origins", "").Replace(":", "").Replace("/", "").Replace("?", "");
				//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog2.txt", ExtractedArg);
				string ConvertedArg = SecurityFuncs.Base64Decode(ExtractedArg);
				string[] SplitArg = ConvertedArg.Split('|');
				string ip = SecurityFuncs.Base64Decode(SplitArg[0]);
				string port = SecurityFuncs.Base64Decode(SplitArg[1]);
				ScriptType type = ScriptType.Join;
            	ScriptGenerator.GenerateScriptForClient(type, ip, port);
				bool IsValid = SecurityFuncs.checkClientMD5();
				if (IsValid == true)
				{
					//temp domain
					string exefile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Origins07_Client.exe";
					string quote = "\"";
					string args = "-script " + quote + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + ScriptGenerator.GetScriptNameForType(type) + quote;
        			Process.Start(exefile, args);
        			this.Close();
				}
				else
				{
					label1.Text = "Cannot Launch Game.";
        			label2.Text = "The client has been detected as modified.";
				}
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
