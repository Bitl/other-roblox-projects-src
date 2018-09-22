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
using System.Diagnostics;

namespace RBX2007_Launcher
{
	/// <summary>
	/// Description of SoloForm.
	/// </summary>
	public partial class SoloForm : Form
	{
		public static string SelectedPart = "Head";
		public string[,] ColorArray;
		
		public SoloForm()
		{
			InitializeComponent();
			ColorArray = new string[32, 2] {
					{ "1", ColorButton7.BackColor.ToString() }, 
					{ "208", ColorButton8.BackColor.ToString() },
					{ "194", ColorButton9.BackColor.ToString() }, 
					{ "199", ColorButton10.BackColor.ToString() },
					{ "26", ColorButton14.BackColor.ToString() },
					{ "21", ColorButton13.BackColor.ToString() },
					{ "24", ColorButton12.BackColor.ToString() },
					{ "226", ColorButton11.BackColor.ToString() },
					{ "23", ColorButton18.BackColor.ToString() },
					{ "107", ColorButton17.BackColor.ToString() },
					{ "102", ColorButton16.BackColor.ToString() },
					{ "11", ColorButton15.BackColor.ToString() },
					{ "45", ColorButton22.BackColor.ToString() },
					{ "135", ColorButton21.BackColor.ToString() },
					{ "106", ColorButton20.BackColor.ToString() },
					{ "105", ColorButton19.BackColor.ToString() },
					{ "141", ColorButton26.BackColor.ToString() },
					{ "28", ColorButton25.BackColor.ToString() },
					{ "37", ColorButton24.BackColor.ToString() },
					{ "119", ColorButton23.BackColor.ToString() },
					{ "29", ColorButton30.BackColor.ToString() },
					{ "151", ColorButton29.BackColor.ToString() },
					{ "38", ColorButton28.BackColor.ToString() },
					{ "192", ColorButton27.BackColor.ToString() },
					{ "104", ColorButton34.BackColor.ToString() },
					{ "9", ColorButton33.BackColor.ToString() },
					{ "101", ColorButton32.BackColor.ToString() },
					{ "5", ColorButton31.BackColor.ToString() },
					{ "153", ColorButton38.BackColor.ToString() },
					{ "217", ColorButton37.BackColor.ToString() },
					{ "18", ColorButton36.BackColor.ToString() },
					{ "125", ColorButton35.BackColor.ToString() }
			};
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.Name = textBox1.Text;	
		}
		
		void NameFormLoad(object sender, EventArgs e)
		{
			if (!File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + GlobalVars.Config))
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
			
			string hatdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\content\\hats";
        	if (Directory.Exists(hatdir))
        	{
        		DirectoryInfo dinfo = new DirectoryInfo(hatdir);
				FileInfo[] Files = dinfo.GetFiles("*.rbxm");
				foreach( FileInfo file in Files )
				{
					if (file.Name.Equals(String.Empty))
					{
   						continue;
					}
					
					listBox1.Items.Add(file.Name);
				}
				listBox1.SelectedItem = GlobalVars.HatName;
        		Image icon1 = Image.FromFile(hatdir + @"\\" + GlobalVars.HatName.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
        	}
        	
        	string mapdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\maps";
			if (Directory.Exists(mapdir))
        	{
        		DirectoryInfo dinfo = new DirectoryInfo(mapdir);
				FileInfo[] Files = dinfo.GetFiles("*.rbxl");
				foreach( FileInfo file in Files )
				{
   					listBox2.Items.Add(file.Name);
				}
				listBox2.SelectedItem = "Baseplate.rbxl";
			}
        	
        	PartSelectionLabel2.Text = SelectedPart;
        	HeadButton1.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_HeadColor);
			TorsoButton2.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_TorsoColor);
			RArmButton3.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightArmColor);
			LArmButton4.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftArmColor);
			RLegButton5.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightLegColor);
			LLegButton6.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftLegColor);
			comboBox1.SelectedIndex = GlobalVars.AASamples;
			checkBox1.Checked = GlobalVars.Shadows;
			
			if (GlobalVars.EXEName == "RobloxShaders.exe")
			{
				checkBox2.Checked = true;
			}
			else
			{
				checkBox2.Checked = false;
			}
			
			if (GlobalVars.AnimatedCharacter == false)
			{
				groupBox1.Enabled = true;
				if (GlobalVars.UseRandomColors == true)
				{
					label9.Enabled = false;
					button3.Enabled = false;
					button4.Enabled = false;
					button5.Enabled = false;
					button6.Enabled = false;
					button7.Enabled = false;
					button9.Enabled = false;
					button10.Enabled = false;
					button11.Enabled = false;
					panel1Head.Enabled = false;
					panel2Torso.Enabled = false;
					panel3LLeg.Enabled = false;
					panel4LArm.Enabled = false;
					panel5RLeg.Enabled = false;
					panel6RArm.Enabled = false;
					checkBox4.Checked = true;
					SetColorsToPreset(1);
					GlobalVars.PlayerColorPreset = 1;
				}
				else
				{
					label9.Enabled = true;
					button3.Enabled = true;
					button4.Enabled = true;
					button5.Enabled = true;
					button6.Enabled = true;
					button7.Enabled = true;
					button9.Enabled = true;
					button10.Enabled = true;
					button11.Enabled = true;
					panel1Head.Enabled = true;
					panel2Torso.Enabled = true;
					panel3LLeg.Enabled = true;
					panel4LArm.Enabled = true;
					panel5RLeg.Enabled = true;
					panel6RArm.Enabled = true;
					checkBox4.Checked = false;
				}
				ToggleCustomization(false);
				checkBox3.Checked = true;
			}
			else
			{
				groupBox1.Enabled = false;
				if (GlobalVars.UseRandomColors == true)
				{
					checkBox4.Checked = true;
				}
				ToggleCustomization(true);
				checkBox3.Checked = false;
			}
			
			SetColorsToPreset(GlobalVars.PlayerColorPreset);
		}
		
		void ToggleCustomization(bool toggleval)
		{
			label3.Enabled = toggleval;
			PartLabel1.Enabled = toggleval;
			PartSelectionLabel2.Enabled = toggleval;
			HeadButton1.Enabled = toggleval;
			TorsoButton2.Enabled = toggleval;
			RArmButton3.Enabled = toggleval;
			LArmButton4.Enabled = toggleval;
			RLegButton5.Enabled = toggleval;
			LLegButton6.Enabled = toggleval;
			ColorButton7.Enabled = toggleval;
			ColorButton8.Enabled = toggleval;
			ColorButton9.Enabled = toggleval;
			ColorButton10.Enabled = toggleval;
			ColorButton14.Enabled = toggleval;
			ColorButton13.Enabled = toggleval;
			ColorButton12.Enabled = toggleval;
			ColorButton11.Enabled = toggleval;
			ColorButton18.Enabled = toggleval;
			ColorButton17.Enabled = toggleval;
			ColorButton16.Enabled = toggleval;
			ColorButton15.Enabled = toggleval;
			ColorButton22.Enabled = toggleval;
			ColorButton21.Enabled = toggleval;
			ColorButton20.Enabled = toggleval;
			ColorButton19.Enabled = toggleval;
			ColorButton26.Enabled = toggleval;
			ColorButton25.Enabled = toggleval;
			ColorButton24.Enabled = toggleval;
			ColorButton23.Enabled = toggleval;
			ColorButton30.Enabled = toggleval;
			ColorButton29.Enabled = toggleval;
			ColorButton28.Enabled = toggleval;
			ColorButton27.Enabled = toggleval;
			ColorButton34.Enabled = toggleval;
			ColorButton33.Enabled = toggleval;
			ColorButton32.Enabled = toggleval;
			ColorButton31.Enabled = toggleval;
			ColorButton38.Enabled = toggleval;
			ColorButton37.Enabled = toggleval;
			ColorButton36.Enabled = toggleval;
			ColorButton35.Enabled = toggleval;
			RandColorsButton39.Enabled = toggleval;
			ResetColorsButton40.Enabled = toggleval;
			label2.Enabled = toggleval;
			listBox1.Enabled = toggleval;
        	pictureBox1.Enabled = toggleval;
        	button8.Enabled = toggleval;
			button2.Enabled = toggleval;
		}
		
		Color ConvertStringtoColor(string CString)
		{
			var p = CString.Split(new char[]{',',']'});

			int A = Convert.ToInt32(p[0].Substring(p[0].IndexOf('=') + 1));
			int R = Convert.ToInt32(p[1].Substring(p[1].IndexOf('=') + 1));
			int G = Convert.ToInt32(p[2].Substring(p[2].IndexOf('=') + 1));
			int B = Convert.ToInt32(p[3].Substring(p[3].IndexOf('=') + 1));
			
			return Color.FromArgb(A,R,G,B);
		}
		
		void ChangeColorOfPart(int ColorID, Color ButtonColor)
		{
			if (SelectedPart == "Head")
			{
				GlobalVars.HeadColor = ColorID;
				GlobalVars.ColorMenu_HeadColor = ButtonColor.ToString();
				HeadButton1.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_HeadColor);
			}
			else if (SelectedPart == "Torso")
			{
				GlobalVars.TorsoColor = ColorID;
				GlobalVars.ColorMenu_TorsoColor = ButtonColor.ToString();
				TorsoButton2.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_TorsoColor);
			}
			else if (SelectedPart == "Right Arm")
			{
				GlobalVars.RightArmColor = ColorID;
				GlobalVars.ColorMenu_RightArmColor = ButtonColor.ToString();
				RArmButton3.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightArmColor);
			}
			else if (SelectedPart == "Left Arm")
			{
				GlobalVars.LeftArmColor = ColorID;
				GlobalVars.ColorMenu_LeftArmColor = ButtonColor.ToString();
				LArmButton4.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftArmColor);
			}
			else if (SelectedPart == "Right Leg")
			{
				GlobalVars.RightLegColor = ColorID;
				GlobalVars.ColorMenu_RightLegColor = ButtonColor.ToString();
				RLegButton5.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightLegColor);
			}
			else if (SelectedPart == "Left Leg")
			{
				GlobalVars.LeftLegColor = ColorID;
				GlobalVars.ColorMenu_LeftLegColor = ButtonColor.ToString();
				LLegButton6.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftLegColor);
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{	
			SecurityFuncs.WriteConfigValues();
            ScriptGenerator.GenerateScriptForClient();
			//temp domain
			string exefile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + GlobalVars.EXEName;
			string quote = "\"";
			string args = "-script " + quote + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + GlobalVars.ScriptLuaFile + quote  + " " + quote + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\maps\\" + listBox2.SelectedItem.ToString() + quote;
        	Process.Start(exefile, args);
		}
		
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
    		base.OnFormClosing(e);
    		SecurityFuncs.WriteConfigValues();
		}
		
		void ListBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			string hatdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\content\\hats";
        	if (Directory.Exists(hatdir))
        	{
        		GlobalVars.HatName = listBox1.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.HatName.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
        	}
		}
		
		void HeadButton1Click(object sender, EventArgs e)
		{
			SelectedPart = "Head";
			PartSelectionLabel2.Text = SelectedPart;
		}
		
		void TorsoButton2Click(object sender, EventArgs e)
		{
			SelectedPart = "Torso";
			PartSelectionLabel2.Text = SelectedPart;
		}
		
		void RArmButton3Click(object sender, EventArgs e)
		{
			SelectedPart = "Right Arm";
			PartSelectionLabel2.Text = SelectedPart;
		}
		
		void LArmButton4Click(object sender, EventArgs e)
		{
			SelectedPart = "Left Arm";
			PartSelectionLabel2.Text = SelectedPart;
		}
		
		void RLegButton5Click(object sender, EventArgs e)
		{
			SelectedPart = "Right Leg";
			PartSelectionLabel2.Text = SelectedPart;
		}
		
		void LLegButton6Click(object sender, EventArgs e)
		{
			SelectedPart = "Left Leg";
			PartSelectionLabel2.Text = SelectedPart;
		}
		
		void ColorButton7Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton7.BackColor;
			int colorID = 1;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton8Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton8.BackColor;
			int colorID = 208;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton9Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton9.BackColor;
			int colorID = 194;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton10Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton10.BackColor;
			int colorID = 199;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton14Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton14.BackColor;
			int colorID = 26;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton13Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton13.BackColor;
			int colorID = 21;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton12Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton12.BackColor;
			int colorID = 24;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton11Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton11.BackColor;
			int colorID = 226;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton18Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton18.BackColor;
			int colorID = 23;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton17Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton17.BackColor;
			int colorID = 107;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton16Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton16.BackColor;
			int colorID = 102;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton15Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton15.BackColor;
			int colorID = 11;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton22Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton22.BackColor;
			int colorID = 45;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton21Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton21.BackColor;
			int colorID = 135;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton20Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton20.BackColor;
			int colorID = 106;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton19Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton19.BackColor;
			int colorID = 105;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton26Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton26.BackColor;
			int colorID = 141;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton25Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton25.BackColor;
			int colorID = 28;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton24Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton24.BackColor;
			int colorID = 37;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton23Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton23.BackColor;
			int colorID = 119;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton30Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton30.BackColor;
			int colorID = 29;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton29Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton29.BackColor;
			int colorID = 151;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton28Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton28.BackColor;
			int colorID = 38;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton27Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton27.BackColor;
			int colorID = 192;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton34Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton34.BackColor;
			int colorID = 104;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton33Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton33.BackColor;
			int colorID = 9;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton32Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton32.BackColor;
			int colorID = 101;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton31Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton31.BackColor;
			int colorID = 5;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton38Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton38.BackColor;
			int colorID = 153;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton37Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton37.BackColor;
			int colorID = 217;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton36Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton36.BackColor;
			int colorID = 18;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void ColorButton35Click(object sender, EventArgs e)
		{
			Color ButtonColor = ColorButton35.BackColor;
			int colorID = 125;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void RandColorsButton39Click(object sender, EventArgs e)
		{
			Random rand = new Random();
			int RandomColor;
			
			for (int i=1; i <= 6; i++)
			{
				RandomColor = rand.Next(ColorArray.GetLength(0));
				if (i == 1)
				{
					GlobalVars.HeadColor = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_HeadColor = ColorArray[RandomColor, 1];
					HeadButton1.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_HeadColor);
				}
				else if (i == 2)
				{
					GlobalVars.TorsoColor = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_TorsoColor = ColorArray[RandomColor, 1];
					TorsoButton2.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_TorsoColor);
				}
				else if (i == 3)
				{
					GlobalVars.RightArmColor = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_RightArmColor = ColorArray[RandomColor, 1];
					RArmButton3.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightArmColor);
				}
				else if (i == 4)
				{
					GlobalVars.LeftArmColor = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_LeftArmColor = ColorArray[RandomColor, 1];
					LArmButton4.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftArmColor);
				}
				else if (i == 5)
				{
					GlobalVars.RightLegColor = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_RightLegColor = ColorArray[RandomColor, 1];
					RLegButton5.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightLegColor);
				}
				else if (i == 6)
				{
					GlobalVars.LeftLegColor = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_LeftLegColor = ColorArray[RandomColor, 1];
					LLegButton6.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftLegColor);
				}
			}			
		}
		
		void ResetColorsButton40Click(object sender, EventArgs e)
		{
			GlobalVars.HeadColor = 24;
			GlobalVars.TorsoColor = 23;
			GlobalVars.LeftArmColor = 24;
			GlobalVars.RightArmColor = 24;
			GlobalVars.LeftLegColor = 119;
			GlobalVars.RightLegColor = 119;
			GlobalVars.ColorMenu_HeadColor = "Color [A=255, R=245, G=205, B=47]";
			GlobalVars.ColorMenu_TorsoColor = "Color [A=255, R=13, G=105, B=172]";
			GlobalVars.ColorMenu_LeftArmColor = "Color [A=255, R=245, G=205, B=47]";
			GlobalVars.ColorMenu_RightArmColor = "Color [A=255, R=245, G=205, B=47]";
			GlobalVars.ColorMenu_LeftLegColor = "Color [A=255, R=164, G=189, B=71]";
			GlobalVars.ColorMenu_RightLegColor = "Color [A=255, R=164, G=189, B=71]";
			HeadButton1.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_HeadColor);
			TorsoButton2.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_TorsoColor);
			RArmButton3.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightArmColor);
			LArmButton4.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftArmColor);
			RLegButton5.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightLegColor);
			LLegButton6.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftLegColor);		
		}
		
		void Button8Click(object sender, EventArgs e)
		{
			string hatdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\content\\hats";
        	if (Directory.Exists(hatdir))
        	{
        		Random random = new Random();
				int randomHat1  = random.Next(listBox1.Items.Count);
				listBox1.SelectedItem = listBox1.Items[randomHat1];
        		GlobalVars.HatName = listBox1.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.HatName.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
        	}
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			string hatdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\content\\hats";
        	if (Directory.Exists(hatdir))
        	{
				listBox1.SelectedItem = "NoHat.rbxm";
        		GlobalVars.HatName = listBox1.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.HatName.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
        	}
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			GlobalVars.AASamples = comboBox1.SelectedIndex;			
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.Shadows = checkBox1.Checked;			
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox2.Checked)
			{
				DialogResult dialogResult = MessageBox.Show("Some low-end graphics cards such as integrated graphics cards and cards from the late 1990s to the mid 2000s may have issues with these shaders. Do you wish to activate this setting anyways?", "RBX2007 - Graphics Card Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				    
				if (dialogResult == DialogResult.No)
				{
				   checkBox2.Checked = false;
				   GlobalVars.EXEName = "RobloxDefault.exe";
				   return;
				}
				
				GlobalVars.EXEName = "RobloxShaders.exe";
			}
			else
			{
				GlobalVars.EXEName = "RobloxDefault.exe";
			}
		}
		
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox3.Checked)
			{
				groupBox1.Enabled = true;
				ToggleCustomization(false);
				GlobalVars.AnimatedCharacter = false;
			}
			else
			{
				groupBox1.Enabled = false;
				ToggleCustomization(true);
				GlobalVars.AnimatedCharacter = true;
			}
		}
		
		void CheckBox4CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox4.Checked)
			{
				label9.Enabled = false;
				button3.Enabled = false;
				button4.Enabled = false;
				button5.Enabled = false;
				button6.Enabled = false;
				button7.Enabled = false;
				button9.Enabled = false;
				button10.Enabled = false;
				button11.Enabled = false;
				panel1Head.Enabled = false;
				panel2Torso.Enabled = false;
				panel3LLeg.Enabled = false;
				panel4LArm.Enabled = false;
				panel5RLeg.Enabled = false;
				panel6RArm.Enabled = false;
				GlobalVars.UseRandomColors = true;
				SetColorsToPreset(1);
				GlobalVars.PlayerColorPreset = 1;
			}
			else
			{
				label9.Enabled = true;
				button3.Enabled = true;
				button4.Enabled = true;
				button5.Enabled = true;
				button6.Enabled = true;
				button7.Enabled = true;
				button9.Enabled = true;
				button10.Enabled = true;
				button11.Enabled = true;
				panel1Head.Enabled = true;
				panel2Torso.Enabled = true;
				panel3LLeg.Enabled = true;
				panel4LArm.Enabled = true;
				panel5RLeg.Enabled = true;
				panel6RArm.Enabled = true;
				GlobalVars.UseRandomColors = false;
			}
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			SetColorsToPreset(1);
			GlobalVars.PlayerColorPreset = 1;
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			SetColorsToPreset(2);
			GlobalVars.PlayerColorPreset = 2;
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			SetColorsToPreset(3);
			GlobalVars.PlayerColorPreset = 3;
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			SetColorsToPreset(4);
			GlobalVars.PlayerColorPreset = 4;
		}
		
		void Button11Click(object sender, EventArgs e)
		{
			SetColorsToPreset(5);
			GlobalVars.PlayerColorPreset = 5;
		}
		
		void Button10Click(object sender, EventArgs e)
		{
			SetColorsToPreset(6);
			GlobalVars.PlayerColorPreset = 6;
		}
		
		void Button9Click(object sender, EventArgs e)
		{
			SetColorsToPreset(7);
			GlobalVars.PlayerColorPreset = 7;
		}
		
		void Button7Click(object sender, EventArgs e)
		{
			SetColorsToPreset(8);
			GlobalVars.PlayerColorPreset = 8;
		}
		
		void SetColorsToPreset(int preset)
		{
			if (preset == 1)
			{
				panel1Head.BackColor = Color.FromArgb(245, 205, 48);
				panel2Torso.BackColor = Color.FromArgb(163, 162, 165);
				panel3LLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel4LArm.BackColor = Color.FromArgb(245, 205, 48);
				panel5RLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel6RArm.BackColor = Color.FromArgb(245, 205, 48);
			}
			else if (preset == 2)
			{
				panel1Head.BackColor = Color.FromArgb(245, 205, 48);
				panel2Torso.BackColor = Color.FromArgb(196, 112, 160);
				panel3LLeg.BackColor = Color.FromArgb(232, 186, 200);
				panel4LArm.BackColor = Color.FromArgb(245, 205, 48);
				panel5RLeg.BackColor = Color.FromArgb(232, 186, 200);
				panel6RArm.BackColor = Color.FromArgb(245, 205, 48);	
			}
			else if (preset == 3)
			{
				panel1Head.BackColor = Color.FromArgb(245, 205, 48);
				panel2Torso.BackColor = Color.FromArgb(13, 105, 172);
				panel3LLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel4LArm.BackColor = Color.FromArgb(245, 205, 48);
				panel5RLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel6RArm.BackColor = Color.FromArgb(245, 205, 48);
			}
			else if (preset == 4)
			{
				panel1Head.BackColor = Color.FromArgb(245, 205, 48);
				panel2Torso.BackColor = Color.FromArgb(196, 112, 160);
				panel3LLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel4LArm.BackColor = Color.FromArgb(245, 205, 48);
				panel5RLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel6RArm.BackColor = Color.FromArgb(245, 205, 48);
			}
			else if (preset == 5)
			{
				panel1Head.BackColor = Color.FromArgb(245, 205, 48);
				panel2Torso.BackColor = Color.FromArgb(128, 187, 219);
				panel3LLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel4LArm.BackColor = Color.FromArgb(245, 205, 48);
				panel5RLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel6RArm.BackColor = Color.FromArgb(245, 205, 48);	
			}
			else if (preset == 6)
			{
				panel1Head.BackColor = Color.FromArgb(160, 95, 53);
				panel2Torso.BackColor = Color.FromArgb(163, 162, 165);
				panel3LLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel4LArm.BackColor = Color.FromArgb(160, 95, 53);
				panel5RLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel6RArm.BackColor = Color.FromArgb(160, 95, 53);
			}
			else if (preset == 7)
			{
				panel1Head.BackColor = Color.FromArgb(174, 122, 89);
				panel2Torso.BackColor = Color.FromArgb(164, 189, 71);
				panel3LLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel4LArm.BackColor = Color.FromArgb(174, 122, 89);
				panel5RLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel6RArm.BackColor = Color.FromArgb(174, 122, 89);	
			}
			else if (preset == 8)
			{
				panel1Head.BackColor = Color.FromArgb(232, 186, 200);
				panel2Torso.BackColor = Color.FromArgb(163, 162, 165);
				panel3LLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel4LArm.BackColor = Color.FromArgb(232, 186, 200);
				panel5RLeg.BackColor = Color.FromArgb(164, 189, 71);
				panel6RArm.BackColor = Color.FromArgb(232, 186, 200);
			}
		}
	}
}
