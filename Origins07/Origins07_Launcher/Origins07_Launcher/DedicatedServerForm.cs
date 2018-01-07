/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 11/25/2017
 * Time: 7:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Open.Nat;

namespace Origins07_Launcher
{
	/// <summary>
	/// Description of DedicatedServerForm.
	/// </summary>
	public partial class DedicatedServerForm : Form
	{
		public DedicatedServerForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public async void StartUPNP()
		{
			try
			{
				string ExtractedArg = "";
				//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog.txt", GlobalVars.SharedArgs);
				ExtractedArg = GlobalVars.SharedArgs.Replace("origins07server://", "").Replace("origins07server", "").Replace("origins07", "").Replace("origins", "").Replace(":", "").Replace("/", "").Replace("?", "");
				//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog2.txt", ExtractedArg);
				string ConvertedArg = SecurityFuncs.Base64Decode(ExtractedArg);
				string[] SplitArg = ConvertedArg.Split('|');
				string port = SecurityFuncs.Base64Decode(SplitArg[0]);
				
    			var nat = new NatDiscoverer();
    			var cts = new CancellationTokenSource(5000);
    			var device = await nat.DiscoverDeviceAsync(PortMapper.Upnp, cts);
    			await device.CreatePortMapAsync(new Mapping(Protocol.Udp, Convert.ToInt32(port), Convert.ToInt32(port), "Origins07"));
			}
			catch (Exception)
            {
            }
		}
		
		public async void StopUPNP()
		{
			try
			{
    			var nat = new NatDiscoverer();
				var cts = new CancellationTokenSource(5000);
				var device = await nat.DiscoverDeviceAsync(PortMapper.Upnp, cts);

				foreach (var mapping in await device.GetAllMappingsAsync())
				{
     				if(mapping.Description.Contains("Origins07"))
     				{
        				await device.DeletePortMapAsync(mapping);
     				}
				}
			}
			catch (Exception)
            {
            }
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			string ExtractedArg = "";
			//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog.txt", GlobalVars.SharedArgs);
			ExtractedArg = GlobalVars.SharedArgs.Replace("origins07server://", "").Replace("origins07server", "").Replace("origins07", "").Replace("origins", "").Replace(":", "").Replace("/", "").Replace("?", "");
			//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog2.txt", ExtractedArg);
			string ConvertedArg = SecurityFuncs.Base64Decode(ExtractedArg);
			string[] SplitArg = ConvertedArg.Split('|');
			string port = SecurityFuncs.Base64Decode(SplitArg[0]);
			string ping = SecurityFuncs.Base64Decode(SplitArg[1]);
			string limit = SecurityFuncs.Base64Decode(SplitArg[2]);
			bool upnp = Convert.ToBoolean(SecurityFuncs.Base64Decode(SplitArg[3]));
			string id = SecurityFuncs.Base64Decode(SplitArg[4]);
			
			if(upnp == true )
			{
				StartUPNP();
			}
			ScriptType type = ScriptType.Server;
			ScriptGenerator.GenerateScriptForClient(type, ping, port, limit, id);
			//temp domain
			string exefile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Origins07_Server.exe";
			string quote = "\"";
			string args = "-script " + quote + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + ScriptGenerator.GetScriptNameForType(type) + quote + " -no3d " + quote + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\maps\\" + listBox1.SelectedItem.ToString() + quote;
			Process server = new Process();
			server.StartInfo.FileName = exefile;
			server.StartInfo.Arguments = args;
			server.EnableRaisingEvents = true;
			server.Exited += new EventHandler(ServerExited);
			server.Start();
		}
		
		void ServerExited(object sender, EventArgs e)
		{
			string ExtractedArg = "";
			//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog.txt", GlobalVars.SharedArgs);
			ExtractedArg = GlobalVars.SharedArgs.Replace("origins07server://", "").Replace("origins07server", "").Replace("origins07", "").Replace("origins", "").Replace(":", "").Replace("/", "").Replace("?", "");
			//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog2.txt", ExtractedArg);
			string ConvertedArg = SecurityFuncs.Base64Decode(ExtractedArg);
			string[] SplitArg = ConvertedArg.Split('|');
			bool upnp = Convert.ToBoolean(SecurityFuncs.Base64Decode(SplitArg[3]));
			if(upnp == true )
			{
				StopUPNP();
			}
		}
		
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
    		base.OnFormClosing(e);
    		string ExtractedArg = "";
			//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog.txt", GlobalVars.SharedArgs);
			ExtractedArg = GlobalVars.SharedArgs.Replace("origins07server://", "").Replace("origins07server", "").Replace("origins07", "").Replace("origins", "").Replace(":", "").Replace("/", "").Replace("?", "");
			//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog2.txt", ExtractedArg);
			string ConvertedArg = SecurityFuncs.Base64Decode(ExtractedArg);
			string[] SplitArg = ConvertedArg.Split('|');
			bool upnp = Convert.ToBoolean(SecurityFuncs.Base64Decode(SplitArg[3]));
    		if(upnp == true )
			{
    			StopUPNP();
    		}
		}
		
		void DedicatedServerFormLoad(object sender, EventArgs e)
		{
			string ExtractedArg = "";
			//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog.txt", GlobalVars.SharedArgs);
			ExtractedArg = GlobalVars.SharedArgs.Replace("origins07server://", "").Replace("origins07server", "").Replace("origins07", "").Replace("origins", "").Replace(":", "").Replace("/", "").Replace("?", "");
			//File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\arglog2.txt", ExtractedArg);
			string ConvertedArg = SecurityFuncs.Base64Decode(ExtractedArg);
			string[] SplitArg = ConvertedArg.Split('|');
			string port = SecurityFuncs.Base64Decode(SplitArg[0]);
			string limit = SecurityFuncs.Base64Decode(SplitArg[2]);
			bool upnp = Convert.ToBoolean(SecurityFuncs.Base64Decode(SplitArg[3]));
			
			string mapdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\maps";
			if (Directory.Exists(mapdir))
        	{
				DirectoryInfo dinfo = new DirectoryInfo(mapdir);
				FileInfo[] Files = dinfo.GetFiles("*.rbxl");
				foreach( FileInfo file in Files )
				{
   					listBox1.Items.Add(file.Name);
				}
				listBox1.SelectedItem = "Baseplate.rbxl";
			}
		}
	}
}
