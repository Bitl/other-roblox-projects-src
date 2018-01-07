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

namespace Origins07_Launcher
{
	/// <summary>
	/// Description of NameForm.
	/// </summary>
	public partial class NameForm : Form
	{
		public static string SelectedPart = "Head";
		public string[,] ColorArray;
		
		public NameForm()
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
        	
        	PartSelectionLabel2.Text = SelectedPart;
        	HeadButton1.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_HeadColor);
			TorsoButton2.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_TorsoColor);
			RArmButton3.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightArmColor);
			LArmButton4.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftArmColor);
			RLegButton5.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightLegColor);
			LLegButton6.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftLegColor);
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
			this.Close();
		}
		
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
    		base.OnFormClosing(e);
    		SecurityFuncs.WriteConfigValues();
    		GlobalVars.ReadyToLaunch = true;
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
	}
}
