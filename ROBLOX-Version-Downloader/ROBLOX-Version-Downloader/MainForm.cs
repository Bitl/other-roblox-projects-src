/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 5/20/2017
 * Time: 3:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Windows;
using System.IO;

namespace ROBLOX_Version_Downloader
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
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.VersionHash = textBox1.Text;
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			backgroundWorker1.RunWorkerAsync();
		}
		
		void BackgroundWorker1DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			string remoteUri = "http://setup.roblox.com/";
			var links = new[] {"null"};
			if (GlobalVars.Type == 1)
			{
				links = new[] {
					remoteUri + GlobalVars.VersionHash + "-RobloxApp.zip", 
					remoteUri + GlobalVars.VersionHash + "-Libraries.zip", 
					remoteUri + GlobalVars.VersionHash + "-content-music.zip",
					remoteUri + GlobalVars.VersionHash + "-redist.zip",
					remoteUri + GlobalVars.VersionHash + "-content-sky.zip",
					remoteUri + GlobalVars.VersionHash + "-content-sounds.zip",
					remoteUri + GlobalVars.VersionHash + "-content-fonts.zip",
					remoteUri + GlobalVars.VersionHash + "-content-textures.zip"
				};
			}
			else if (GlobalVars.Type == 2)
			{
				links = new[] {
					remoteUri + GlobalVars.VersionHash + "-RobloxApp.zip", 
					remoteUri + GlobalVars.VersionHash + "-Libraries.zip", 
					remoteUri + GlobalVars.VersionHash + "-content-music.zip",
					remoteUri + GlobalVars.VersionHash + "-redist.zip",
					remoteUri + GlobalVars.VersionHash + "-content-sky.zip",
					remoteUri + GlobalVars.VersionHash + "-content-sounds.zip",
					remoteUri + GlobalVars.VersionHash + "-content-fonts.zip",
					remoteUri + GlobalVars.VersionHash + "-content-textures.zip",
					remoteUri + GlobalVars.VersionHash + "-content-textures2.zip"
				};
			}
			/*
			else if (GlobalVars.Type == 3)
			{
				links = new[] {
					remoteUri + GlobalVars.VersionHash + "-RobloxApp.zip", 
					remoteUri + GlobalVars.VersionHash + "-Libraries.zip", 
					remoteUri + GlobalVars.VersionHash + "-content-music.zip",
					remoteUri + GlobalVars.VersionHash + "-redist.zip",
					remoteUri + GlobalVars.VersionHash + "-content-sky.zip",
					remoteUri + GlobalVars.VersionHash + "-content-sounds.zip",
					remoteUri + GlobalVars.VersionHash + "-content-fonts.zip",
					remoteUri + GlobalVars.VersionHash + "-content-textures.zip",
					remoteUri + GlobalVars.VersionHash + "-content-textures2.zip",
					remoteUri + GlobalVars.VersionHash + "-content-shaders.zip",
					remoteUri + GlobalVars.VersionHash + "-content-particles.zip"
				};
			}
			else if (GlobalVars.Type == 4)
			{
				links = new[] {
					remoteUri + GlobalVars.VersionHash + "-RobloxApp.zip", 
					remoteUri + GlobalVars.VersionHash + "-Libraries.zip", 
					remoteUri + GlobalVars.VersionHash + "-content-music.zip",
					remoteUri + GlobalVars.VersionHash + "-redist.zip",
					remoteUri + GlobalVars.VersionHash + "-content-sky.zip",
					remoteUri + GlobalVars.VersionHash + "-content-sounds.zip",
					remoteUri + GlobalVars.VersionHash + "-content-fonts.zip",
					remoteUri + GlobalVars.VersionHash + "-content-textures.zip",
					remoteUri + GlobalVars.VersionHash + "-content-textures2.zip",
					remoteUri + GlobalVars.VersionHash + "-content-shaders.zip",
					remoteUri + GlobalVars.VersionHash + "-content-particles.zip",
					remoteUri + GlobalVars.VersionHash + "-BuiltInPlugins.zip"
				};
			}
			*/

   			var percentProgressStep = 0;

   			using (var client = new WebClient()) 
   			foreach (string l in links) 
     		{
   				string filename = l.Replace((remoteUri + GlobalVars.VersionHash + "-"),"");
   				label2.Text = "Downloading: " + filename;
       			client.DownloadFile(l, filename);
       			percentProgressStep += 15;
       			backgroundWorker1.ReportProgress(percentProgressStep);
     		} 
		}
		
		void BackgroundWorker1ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
   		 	progressBar1.Value = e.ProgressPercentage;
   		 	
   		 	if (progressBar1.Value >= 100)
   		 	{
   		 		progressBar1.Value = progressBar1.Maximum;
   		 		label2.Text = "Download Complete!";
   		 	}
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked == true)
			{
				checkBox2.Checked = false;
				GlobalVars.Type = 1;
			}
			else if (checkBox1.Checked == false)
			{
				GlobalVars.Type = 0;
			}
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox2.Checked == true)
			{
				checkBox1.Checked = false;
				GlobalVars.Type = 2;
			}
			else if (checkBox2.Checked == false)
			{
				GlobalVars.Type = 0;
			}
		}
	}
}
