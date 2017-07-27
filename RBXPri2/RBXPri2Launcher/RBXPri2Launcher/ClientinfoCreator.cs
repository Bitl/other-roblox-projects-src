﻿/*
 * Created by SharpDevelop.
 * User: BITL-Gaming
 * Date: 11/28/2016
 * Time: 7:55 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace RBXPri2Launcher
{
	/// <summary>
	/// Description of ClientinfoCreator.
	/// </summary>
	public partial class ClientinfoEditor : Form
	{
		public ClientinfoEditor()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked == true)
			{
				GlobalVars.ClientCreator_UsesPlayerName = true;
			}
			else if (checkBox1.Checked == false)
			{
				GlobalVars.ClientCreator_UsesPlayerName = false;
			}
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox2.Checked == true)
			{
				GlobalVars.ClientCreator_UsesID = true;
			}
			else if (checkBox2.Checked == false)
			{
				GlobalVars.ClientCreator_UsesID = false;
			}
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.ClientCreator_SelectedClientDesc = textBox1.Text;
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog())
        	{
				ofd.Filter = "Text files (*.txt)|*.txt";
            	ofd.FilterIndex = 2;
            	ofd.FileName = "clientinfo.txt";
            	ofd.Title = "Load clientinfo.txt";
            	if (ofd.ShowDialog() == DialogResult.OK)
            	{
					string line1;
					string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6;

					using(StreamReader reader = new StreamReader(ofd.FileName)) 
					{
    					line1 = reader.ReadLine();
					}
			
					if (!SecurityFuncs.IsBase64String(line1))
						return;
					
					string ConvertedLine = SecurityFuncs.Base64Decode(line1);
					string[] result = ConvertedLine.Split('|');
					Decryptline1 = SecurityFuncs.Base64Decode(result[0]);
    				Decryptline2 = SecurityFuncs.Base64Decode(result[1]);
    				Decryptline3 = SecurityFuncs.Base64Decode(result[2]);
    				Decryptline4 = SecurityFuncs.Base64Decode(result[3]);
    				Decryptline5 = SecurityFuncs.Base64Decode(result[4]);
    				Decryptline6 = SecurityFuncs.Base64Decode(result[5]);
					
					Boolean bline1 = Convert.ToBoolean(Decryptline1);
					GlobalVars.ClientCreator_UsesPlayerName = bline1;
					
					Boolean bline2 = Convert.ToBoolean(Decryptline2);
					GlobalVars.ClientCreator_UsesID = bline2;
					
					Boolean bline3 = Convert.ToBoolean(Decryptline3);
					GlobalVars.ClientCreator_LoadsAssetsOnline = bline3;
					
					Boolean bline4 = Convert.ToBoolean(Decryptline4);
					GlobalVars.ClientCreator_LegacyMode = bline4;
					
					GlobalVars.ClientCreator_SelectedClientMD5 = Decryptline5;
					
					GlobalVars.ClientCreator_SelectedClientDesc = Decryptline6;
					
					checkBox1.Checked = GlobalVars.ClientCreator_UsesPlayerName;
					checkBox2.Checked = GlobalVars.ClientCreator_UsesID;
					checkBox5.Checked = GlobalVars.ClientCreator_LoadsAssetsOnline;
					checkBox3.Checked = GlobalVars.ClientCreator_LegacyMode;
					textBox2.Text = GlobalVars.ClientCreator_SelectedClientMD5.ToUpper();
					textBox1.Text = GlobalVars.ClientCreator_SelectedClientDesc;
            	}
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog())
        	{
            	sfd.Filter = "Text files (*.txt)|*.txt";
            	sfd.FilterIndex = 2;
            	sfd.FileName = "clientinfo.txt";
            	sfd.Title = "Save clientinfo.txt";

            	if (sfd.ShowDialog() == DialogResult.OK)
            	{
            		string[] lines = { 
            			SecurityFuncs.Base64Encode(GlobalVars.ClientCreator_UsesPlayerName.ToString()),
            			SecurityFuncs.Base64Encode(GlobalVars.ClientCreator_UsesID.ToString()),
            			SecurityFuncs.Base64Encode(GlobalVars.ClientCreator_LoadsAssetsOnline.ToString()),
            			SecurityFuncs.Base64Encode(GlobalVars.ClientCreator_LegacyMode.ToString()),
            			SecurityFuncs.Base64Encode(GlobalVars.ClientCreator_SelectedClientMD5.ToString()),
            			SecurityFuncs.Base64Encode(GlobalVars.ClientCreator_SelectedClientDesc.ToString())
            		};
            		File.WriteAllText(sfd.FileName, SecurityFuncs.Base64Encode(string.Join("|",lines)));
            	}     
			}			
		}
		
		void ClientinfoCreatorLoad(object sender, EventArgs e)
		{
			
		}
		
		void CheckBox5CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox5.Checked == true)
			{
				GlobalVars.ClientCreator_LoadsAssetsOnline = true;
			}
			else if (checkBox5.Checked == false)
			{
				GlobalVars.ClientCreator_LoadsAssetsOnline = false;
			}
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			GlobalVars.ClientCreator_UsesPlayerName = false;
			GlobalVars.ClientCreator_UsesID = false;
			GlobalVars.ClientCreator_LoadsAssetsOnline = false;
			GlobalVars.ClientCreator_LegacyMode = false;
			GlobalVars.ClientCreator_SelectedClientDesc = "";
			GlobalVars.ClientCreator_SelectedClientMD5 = "";
			checkBox1.Checked = GlobalVars.ClientCreator_UsesPlayerName;
			checkBox2.Checked = GlobalVars.ClientCreator_UsesID;
			checkBox5.Checked = GlobalVars.ClientCreator_LoadsAssetsOnline;
			checkBox3.Checked = GlobalVars.ClientCreator_LegacyMode;
			textBox2.Text = GlobalVars.ClientCreator_SelectedClientMD5.ToUpper();
			textBox1.Text = GlobalVars.ClientCreator_SelectedClientDesc;
		}
		
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox3.Checked == true)
			{
				GlobalVars.ClientCreator_LegacyMode = true;
			}
			else if (checkBox3.Checked == false)
			{
				GlobalVars.ClientCreator_LegacyMode = false;
			}
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			textBox2.Text = textBox2.Text.ToUpper();
			GlobalVars.ClientCreator_SelectedClientMD5 = textBox2.Text.ToUpper();
		}
	}
}
