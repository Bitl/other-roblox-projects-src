/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 6/4/2017
 * Time: 5:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Origins06_Launcher
{
	/// <summary>
	/// Description of NameForm.
	/// </summary>
	public partial class NameForm : Form
	{
		public NameForm()
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
			GlobalVars.Name = textBox1.Text;	
		}
		
		void NameFormLoad(object sender, EventArgs e)
		{
			if (!File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\PlayerConfig.txt"))
			{
				SecurityFuncs.GeneratePlayerID();
				SecurityFuncs.WriteConfigValues();
				SecurityFuncs.ReadConfigValues();
			}
			else
			{
				SecurityFuncs.ReadConfigValues();
			}
			textBox1.Text = GlobalVars.Name;
		}
		
		void Button1Click(object sender, EventArgs e)
		{	
			this.Close();
		}
		
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
    		base.OnFormClosing(e);
    		SecurityFuncs.WriteConfigValues();
    		GlobalVars.ReadyToLaunch = true;
		}
	}
}
