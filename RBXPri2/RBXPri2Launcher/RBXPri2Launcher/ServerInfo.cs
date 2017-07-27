/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 5/14/2017
 * Time: 9:14 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;

namespace RBXPri2Launcher
{
	/// <summary>
	/// Description of ServerInfo.
	/// </summary>
	public partial class ServerInfo : Form
	{
		public ServerInfo()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ServerInfoLoad(object sender, EventArgs e)
		{
        	textBox1.AppendText("Client: " + GlobalVars.SelectedClient);
        	textBox1.AppendText(Environment.NewLine);
        	textBox1.AppendText("IP: " + GetExternalIPAddress());
        	textBox1.AppendText("Port: " + GlobalVars.RobloxPort.ToString());
        	textBox1.AppendText(Environment.NewLine);
			textBox1.AppendText("Map: " + GlobalVars.Map);
        	textBox1.AppendText(Environment.NewLine);
			textBox1.AppendText("Version: RBXPri2: " + GlobalVars.Version);
		}
		
		string GetExternalIPAddress()
		{
    		string ipAddress;
			using (WebClient wc = new WebClient())
			{
				try
  				{
    				ipAddress = wc.DownloadString("http://icanhazip.com/");
  				}
				catch (Exception)
  				{
    				ipAddress = "localhost" + Environment.NewLine;
  				}
			}

    		return ipAddress;
		}
	}
}
