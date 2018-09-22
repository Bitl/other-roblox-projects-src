/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 6/6/2017
 * Time: 11:12 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Cypher_Launcher
{
	public static class GlobalVars
	{
		public static string SharedArgs;
		public static string Name = "Player";
		public static string HatName = "NoHat.rbxm";
		public static int HeadColor = 24;
		public static int TorsoColor = 23;
		public static int LeftArmColor = 24;
		public static int RightArmColor = 24;
		public static int LeftLegColor = 119;
		public static int RightLegColor = 119;
		public static string ColorMenu_HeadColor = "Color [A=255, R=245, G=205, B=47]";
		public static string ColorMenu_TorsoColor = "Color [A=255, R=13, G=105, B=172]";
		public static string ColorMenu_LeftArmColor = "Color [A=255, R=245, G=205, B=47]";
		public static string ColorMenu_RightArmColor = "Color [A=255, R=245, G=205, B=47]";
		public static string ColorMenu_LeftLegColor = "Color [A=255, R=164, G=189, B=71]";
		public static string ColorMenu_RightLegColor = "Color [A=255, R=164, G=189, B=71]";
		public static string ScriptLuaFile = "\\game.dat";
		public static string Config = "config.dat";
		public static int UserID = 0;
		public static int AASamples = 0;
		public static bool Shadows = false;
		public static int BotCount = 16;
	}
	
	public class SecurityFuncs
	{
		public SecurityFuncs()
		{
		}
		
		public static void WriteConfigValues()
		{
			string[] lines = { 
				GlobalVars.Name.ToString(),
				GlobalVars.UserID.ToString(),
				GlobalVars.HatName.ToString(),
				GlobalVars.HeadColor.ToString(),
				GlobalVars.TorsoColor.ToString(),
				GlobalVars.LeftArmColor.ToString(),
				GlobalVars.RightArmColor.ToString(),
				GlobalVars.LeftLegColor.ToString(),
				GlobalVars.RightLegColor.ToString(),
				GlobalVars.ColorMenu_HeadColor.ToString(),
				GlobalVars.ColorMenu_TorsoColor.ToString(),
				GlobalVars.ColorMenu_LeftArmColor.ToString(),
				GlobalVars.ColorMenu_RightArmColor.ToString(),
				GlobalVars.ColorMenu_LeftLegColor.ToString(),
				GlobalVars.ColorMenu_RightLegColor.ToString(),
				GlobalVars.BotCount.ToString(),
				GlobalVars.AASamples.ToString(),
				GlobalVars.Shadows.ToString()
			};
			File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + GlobalVars.Config, Base64Encode(string.Join("|",lines)));
		}
		
		public static void ReadConfigValues()
		{
			string line1;

			using(StreamReader reader = new StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + GlobalVars.Config)) 
			{
    			line1 = reader.ReadLine();
			}
			
			string ConvertedLine = Base64Decode(line1);
			string[] result = ConvertedLine.Split('|');
			
			GlobalVars.Name = result[0];
			GlobalVars.UserID = Convert.ToInt32(result[1]);
			GlobalVars.HatName = result[2];
			GlobalVars.HeadColor = Convert.ToInt32(result[3]);
			GlobalVars.TorsoColor = Convert.ToInt32(result[4]);
			GlobalVars.LeftArmColor = Convert.ToInt32(result[5]);
			GlobalVars.RightArmColor = Convert.ToInt32(result[6]);
			GlobalVars.LeftLegColor = Convert.ToInt32(result[7]);
			GlobalVars.RightLegColor = Convert.ToInt32(result[8]);
			GlobalVars.ColorMenu_HeadColor = result[9];
			GlobalVars.ColorMenu_TorsoColor = result[10];
			GlobalVars.ColorMenu_LeftArmColor = result[11];
			GlobalVars.ColorMenu_RightArmColor = result[12];
			GlobalVars.ColorMenu_LeftLegColor = result[13];
			GlobalVars.ColorMenu_RightLegColor = result[14];
			GlobalVars.BotCount = Convert.ToInt32(result[15]);
			GlobalVars.AASamples = Convert.ToInt32(result[16]);
			GlobalVars.Shadows = Convert.ToBoolean(result[17]);
			
			if (GlobalVars.UserID == 0)
			{
				GeneratePlayerID();
				WriteConfigValues();
			}
		}
		
		public static void GeneratePlayerID()
		{
			CryptoRandom random = new CryptoRandom();
			int randomID = 0;
			int randIDmode = random.Next(0,7);
			if (randIDmode == 0)
			{
				randomID = random.Next(0, 99);
			}
			else if (randIDmode == 1)
			{
				randomID = random.Next(0, 999);
			}
			else if (randIDmode == 2)
			{
				randomID = random.Next(0, 9999);
			}
			else if (randIDmode == 3)
			{
				randomID = random.Next(0, 99999);
			}
			else if (randIDmode == 4)
			{
				randomID = random.Next(0, 999999);
			}
			else if (randIDmode == 5)
			{
				randomID = random.Next(0, 9999999);
			}
			else if (randIDmode == 6)
			{
				randomID = random.Next(0, 99999999);
			}
			else if (randIDmode == 7)
			{
				randomID = random.Next();
			}
			//2147483647 is max id.
			GlobalVars.UserID = randomID;
		}
		
		public static string Base64Decode(string base64EncodedData) 
		{
  			var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
  			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
		
		public static string Base64Encode(string plainText) 
		{
  			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
  			return System.Convert.ToBase64String(plainTextBytes);
		}
	}
	
	public class ScriptGenerator
	{
		public ScriptGenerator()
		{
		}

		public static void GenerateScriptForClient()
		{
			//next, generate the header functions.

			SecurityFuncs.ReadConfigValues();
			
			int aasamples = GlobalVars.AASamples.Equals(0) ? 1 : GlobalVars.AASamples.Equals(1) ? 4 : GlobalVars.AASamples.Equals(2) ? 8 : 1;
			
			string header = MultiLine(
					"function newWaitForChild(newParent,name)",
					"local returnable = nil",
					"if newParent:FindFirstChild(name) then",
					"returnable = newParent:FindFirstChild(name)",
					"else",
					"repeat wait() returnable = newParent:FindFirstChild(name)  until returnable ~= nil",
					"end",
					"return returnable",
					"end",
					"settings().Rendering.Shadows = " + GlobalVars.Shadows.ToString().ToLower(),
					"settings().Rendering.AASamples = " + aasamples
             		);

			string playersettings = MultiLine(
                	"UserID = " + GlobalVars.UserID,
                	"PlayerName = '" + GlobalVars.Name + "'",
                	"Hat1ID = '" + GlobalVars.HatName + "'",
                	"HeadColorID = " + GlobalVars.HeadColor,
                	"TorsoColorID = " + GlobalVars.TorsoColor,
                	"LeftArmColorID = " + GlobalVars.LeftArmColor,
                	"RightArmColorID = " + GlobalVars.RightArmColor,
                	"LeftLegColorID = " + GlobalVars.LeftLegColor,
                	"RightLegColorID = " + GlobalVars.RightLegColor,
                	"BotCount = " + GlobalVars.BotCount,
            		);

			//add customization funcs
			string customizationgen = MultiLine(
				    "function InitalizeClientAppearance(Player,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,HatID)",
					"local newCharApp = Instance.new('IntValue',Player)",
					"newCharApp.Name = 'Appearance'",
					"for i=1,6,1 do",
					"local BodyColor = Instance.new('BrickColorValue',newCharApp)",
					"if (i == 1) then",
					"if (HeadColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(HeadColorID)",
					"BodyColor.Name = 'HeadColor (ID: '..HeadColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'HeadColor (ID: 1)'",
					"end",
					"elseif (i == 2) then",
					"if (TorsoColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(TorsoColorID)",
					"BodyColor.Name = 'TorsoColor (ID: '..TorsoColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'TorsoColor (ID: 1)'",
					"end",
					"elseif (i == 3) then",
					"if (LeftArmColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(LeftArmColorID)",
					"BodyColor.Name = 'LeftArmColor (ID: '..LeftArmColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'LeftArmColor (ID: 1)'",
					"end",
					"elseif (i == 4) then",
					"if (RightArmColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(RightArmColorID)",
					"BodyColor.Name = 'RightArmColor (ID: '..RightArmColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'RightArmColor (ID: 1)'",
					"end",
					"elseif (i == 5) then",
					"if (LeftLegColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(LeftLegColorID)",
					"BodyColor.Name = 'LeftLegColor (ID: '..LeftLegColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'LeftLegColor (ID: 1)'",
					"end",
					"elseif (i == 6) then",
					"if (RightLegColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(RightLegColorID)",
					"BodyColor.Name = 'RightLegColor (ID: '..RightLegColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'RightLegColor (ID: 1)'",
					"end",
					"end",
					"local typeValue = Instance.new('NumberValue')",
					"typeValue.Name = 'CustomizationType'",
					"typeValue.Parent = BodyColor",
					"typeValue.Value = 1",
					"local indexValue = Instance.new('NumberValue')",
					"indexValue.Name = 'ColorIndex'",
					"indexValue.Parent = BodyColor",
					"indexValue.Value = i",
					"end",
					"local newHat = Instance.new('StringValue',newCharApp)",
					"if (HatID ~= nil) then",
					"newHat.Value = HatID",
					"newHat.Name = HatID",
					"else",
					"newHat.Value = 'NoHat.rbxm'",
					"newHat.Name = 'NoHat.rbxm'",
					"end",
					"local typeValue = Instance.new('NumberValue')",
					"typeValue.Name = 'CustomizationType'",
					"typeValue.Parent = newHat",
					"typeValue.Value = 2",
					"end",
                    "function LoadCharacterNew(playerApp,newChar)",
					"local charparts = {[1] = newWaitForChild(newChar,'Head'),[2] = newWaitForChild(newChar,'Torso'),[3] = newWaitForChild(newChar,'Left Arm'),[4] = newWaitForChild(newChar,'Right Arm'),[5] = newWaitForChild(newChar,'Left Leg'),[6] = newWaitForChild(newChar,'Right Leg')}",
					"for _,newVal in pairs(playerApp:GetChildren()) do",
					"newWaitForChild(newVal,'CustomizationType')",
					"local customtype = newVal:FindFirstChild('CustomizationType')",
					"if (customtype.Value == 1) then ",
					"pcall(function()",
					"newWaitForChild(newVal,'ColorIndex')",
					"local colorindex = newVal:FindFirstChild('ColorIndex')",
					"charparts[colorindex.Value].BrickColor = newVal.Value ",
					"end)",
					"elseif (customtype.Value == 2)  then",
					"pcall(function()",
					"local newHat = game.Workspace:InsertContent('rbxasset://hats/'..newVal.Value)",
					"if newHat[1] then ",
					"if newHat[1].className == 'Hat' then",
					"newHat[1].Parent = newChar",
					"else",
					"newHat[1]:remove()",
					"end",
					"end",
					"end)",
					"end",
					"end",
					"end"
					);
			
			//string botcode = MultiLine(
			//		);

			//finally, we generate the actual script code.

			string code = MultiLine(
					"game:GetService('RunService'):run()",
					"local plr = game.Players:CreateLocalPlayer(UserID)",
					"plr.Name = PlayerName",
					"plr:LoadCharacter()",
					"pcall(function() plr:SetUnder13(false) end)",
					"pcall(function() plr:SetAccountAge(365) end)",
					"InitalizeClientAppearance(plr,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,Hat1ID)",
					"LoadCharacterNew(newWaitForChild(plr,'Appearance'),plr.Character)",
					"game:GetService('Visit')",
					"while true do",
					"wait(0.001)",
					"if (plr.Character ~= nil) then",
					"if (plr.Character.Humanoid.Health == 0) then",
					"wait(5)",
					"plr:LoadCharacter()",
					"LoadCharacterNew(newWaitForChild(plr,'Appearance'),plr.Character,plr.Backpack)",
					"elseif (plr.Character.Parent == nil) then ",
					"wait(5)",
					"plr:LoadCharacter()",
					"LoadCharacterNew(newWaitForChild(plr,'Appearance'),plr.Character,plr.Backpack)",
					"end",
					"end",
					"end"
					);

			string scriptfile = MultiLine(
				header,
				playersettings,
				customizationgen,
				code
				);
			
			List<string> list = new List<string>(Regex.Split(scriptfile, Environment.NewLine));
			string[] convertedList = list.ToArray();
			File.WriteAllLines(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + GlobalVars.ScriptLuaFile, convertedList);
		}

		static string MultiLine(params string[] args)
		{
			return string.Join(Environment.NewLine, args);
		}
	}
	
	///<summary>
	/// Represents a pseudo-random number generator, a device that produces random data.
	///</summary>
	class CryptoRandom : RandomNumberGenerator
	{
		private static RandomNumberGenerator r;

		///<summary>
		/// Creates an instance of the default implementation of a cryptographic random number generator that can be used to generate random data.
		///</summary>
		public CryptoRandom()
 		{ 
  			r = RandomNumberGenerator.Create();
 		}

 		///<summary>
 		/// Fills the elements of a specified array of bytes with random numbers.
 		///</summary>
 		///<param name=”buffer”>An array of bytes to contain random numbers.</param>
 		public override void GetBytes(byte[] buffer)
 		{
  			r.GetBytes(buffer);
 		}
 	
 		///
		/// Fills an array of bytes with a cryptographically strong random sequence of nonzero values.
		///
		/// The array to fill with cryptographically strong random nonzero bytes
		public override void GetNonZeroBytes(byte[] data)
		{
			r.GetNonZeroBytes(data);
		}

 		///<summary>
 		/// Returns a random number between 0.0 and 1.0.
 		///</summary>
 		public double NextDouble()
 		{
  			byte[] b = new byte[4];
  			r.GetBytes(b);
  			return (double)BitConverter.ToUInt32(b, 0) / UInt32.MaxValue;
 		}

 		///<summary>
 		/// Returns a random number within the specified range.
 		///</summary>
 		///<param name=”minValue”>The inclusive lower bound of the random number returned.</param>
 		///<param name=”maxValue”>The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
 		public int Next(int minValue, int maxValue)
 		{
  			return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
 		}

 		///<summary>
 		/// Returns a nonnegative random number.
 		///</summary>
 		public int Next()
 		{
  			return Next(0, Int32.MaxValue);
 		}

 		///<summary>
 		/// Returns a nonnegative random number less than the specified maximum
 		///</summary>
 		///<param name=”maxValue”>The inclusive upper bound of the random number returned. maxValue must be greater than or equal 0</param>
 		public int Next(int maxValue)
 		{
  			return Next(0, maxValue);
 		}
	}
}
