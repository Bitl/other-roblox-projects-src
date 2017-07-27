/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 6/6/2017
 * Time: 11:12 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Origins06_Launcher
{
	/// <summary>
	/// Description of SecurityFuncs.
	/// </summary>
	public class SecurityFuncs
	{
		public SecurityFuncs()
		{
		}
		
		public static void WriteConfigValues()
		{
			string[] lines = { 
				GlobalVars.Name.ToString(),
				GlobalVars.UserID.ToString()
			};
			File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\PlayerConfig.txt", Base64Encode(string.Join("|",lines)));
		}
		
		public static void ReadConfigValues()
		{
			string line1;

			using(StreamReader reader = new StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\PlayerConfig.txt")) 
			{
    			line1 = reader.ReadLine();
			}
			
			string ConvertedLine = Base64Decode(line1);
			string[] result = ConvertedLine.Split('|');
			
			GlobalVars.Name = result[0];
			
			GlobalVars.UserID = Convert.ToInt32(result[1]);
			
			if (GlobalVars.UserID == 0)
			{
				GeneratePlayerID();
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
		
		public static int GeneratePlayerSkinColor()
		{
			CryptoRandom random = new CryptoRandom();
			int randmode = random.Next(1,6);
			return randmode;
		}
		
		public static int GeneratePlayerLegColor()
		{
			CryptoRandom random = new CryptoRandom();
			int randmode = random.Next(1,5);
			return randmode;
		}
		
		public static int GeneratePlayerTorsoColor()
		{
			CryptoRandom random = new CryptoRandom();
			int randmode = random.Next(1,8);
			return randmode;
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
		
		public static bool checkClientMD5()
		{
    		using (var md5 = MD5.Create())
    		{
    			using (var stream = File.OpenRead(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Origins06_Client.exe"))
        		{
    				byte[] hash = md5.ComputeHash(stream);
    				string clientMD5 = BitConverter.ToString(hash).Replace("-", "");
            		if (clientMD5.Equals(GlobalVars.ClientMD5))
            		{
            			return true;
            		}
            		else
            		{
            			return false;
            		}
        		}
    		}
		}
	}
}
