settings().Rendering.frameRateManager = 2;
settings().Rendering.graphicsMode = 2;
settings().Network.MaxSendBuffer = 1000000;
settings().Network.PhysicsReplicationUpdateRate = 1000000;
settings().Network.SendRate = 1000000;
rbxversion = version();
print("Origins06 version '" .. rbxversion .. "' loaded.");
--function made by rbxbanland
function newWaitForChild(newParent,name)
	local returnable = nil
	if newParent:FindFirstChild(name) then
		returnable = newParent:FindFirstChild(name)
	else 
		repeat wait() returnable = newParent:FindFirstChild(name)  until returnable ~= nil
	end
	return returnable
end

--we aren't doing anything with shirts or t-shirts or pants yet, we're only doing hats.
function LoadCharacterNew(playerApp,newChar)
	local charparts = {[1] = newWaitForChild(newChar,"Head"),[2] = newWaitForChild(newChar,"Torso"),[3] = newWaitForChild(newChar,"Left Arm"),[4] = newWaitForChild(newChar,"Right Arm"),[5] = newWaitForChild(newChar,"Left Leg"),[6] = newWaitForChild(newChar,"Right Leg")}
	for _,newVal in pairs(playerApp:GetChildren()) do
		pcall(function() 
		charparts[newVal.ColorIndex.Value].BrickColor = newVal.Value 
		end)
	end
end

function PlayerColors(Player,SkinPattern,LegsPattern,TorsoPattern)
	HeadColor=BrickColor.DarkGray();
	TorsoColor=BrickColor.DarkGray();
	LArmColor=BrickColor.DarkGray();
	LLegColor=BrickColor.DarkGray();
	RArmColor=BrickColor.DarkGray();
	RLegColor=BrickColor.DarkGray();
	if (Player.Name == "Bitl" and Player.userId == 1915355) then
		HeadColor=BrickColor.Red();
		TorsoColor=BrickColor.Red();
		LArmColor=BrickColor.Red();
		LLegColor=BrickColor.Red();
		RArmColor=BrickColor.DarkGray();
		RLegColor=BrickColor.DarkGray();
	elseif (Player.Name == "P4ris" and Player.userId == 69) then
		HeadColor=BrickColor.DarkGray();
		TorsoColor=BrickColor.DarkGray();
		LArmColor=BrickColor.DarkGray();
		LLegColor=BrickColor.DarkGray();
		RArmColor=BrickColor.DarkGray();
		RLegColor=BrickColor.DarkGray();
	elseif (Player.Name == "The Living Bee" and Player.userId == 2) then
		HeadColor=BrickColor.new("Cool yellow");
		TorsoColor=BrickColor.White();
		LArmColor=BrickColor.new("Cool yellow");
		LLegColor=BrickColor.White();
		RArmColor=BrickColor.new("Cool yellow");
		RLegColor=BrickColor.White();
	else
		if (SkinPattern==1) then
			HeadColor=BrickColor.Yellow();
			LArmColor=BrickColor.Yellow();
			RArmColor=BrickColor.Yellow();
		elseif (SkinPattern==2) then
			HeadColor=BrickColor.new("Cool yellow");
			LArmColor=BrickColor.new("Cool yellow");
			RArmColor=BrickColor.new("Cool yellow");
		elseif (SkinPattern==3) then
			HeadColor=BrickColor.new("Medium red");
			LArmColor=BrickColor.new("Medium red");
			RArmColor=BrickColor.new("Medium red");
		elseif (SkinPattern==4) then
			HeadColor=BrickColor.new("Light reddish violet");
			LArmColor=BrickColor.new("Light reddish violet");
			RArmColor=BrickColor.new("Light reddish violet");
		elseif (SkinPattern==5) then
			HeadColor=BrickColor.new("Dark orange");
			LArmColor=BrickColor.new("Dark orange");
			RArmColor=BrickColor.new("Dark orange");
		elseif (SkinPattern==6) then
			HeadColor=BrickColor.new("Nougat");
			LArmColor=BrickColor.new("Nougat");
			RArmColor=BrickColor.new("Nougat");
		end
		if (LegsPattern==1) then
			RLegColor=BrickColor.new("Br. yellowish green");
			LLegColor=BrickColor.new("Br. yellowish green");
		elseif (LegsPattern==2) then
			LLegColor=BrickColor.new("Pastel Blue");
			RLegColor=BrickColor.new("Pastel Blue");
		elseif (LegsPattern==3) then
			LLegColor=BrickColor.Blue();
			RLegColor=BrickColor.Blue();
		elseif (LegsPattern==4) then
			LLegColor=BrickColor.White();
			RLegColor=BrickColor.White();
		elseif (LegsPattern==5) then
			LLegColor=BrickColor.new("Light blue");
			RLegColor=BrickColor.new("Light blue");
		end
		if (TorsoPattern==1) then
			TorsoColor=BrickColor.new("Medium stone grey");
		elseif (TorsoPattern==2) then
			TorsoColor=BrickColor.DarkGray();
		elseif (TorsoPattern==3) then
			TorsoColor=BrickColor.White();
		elseif (TorsoPattern==4) then
			TorsoColor=BrickColor.Red();
		elseif (TorsoPattern==5) then
			TorsoColor=BrickColor.Green();
		elseif (TorsoPattern==6) then
			TorsoColor=BrickColor.Blue();
		elseif (TorsoPattern==7) then
			TorsoColor=BrickColor.new("Light blue");
		elseif (TorsoPattern==8) then
			TorsoColor=BrickColor.new("Pastel Blue");
		end
	end
	InitalizeClientAppearance(Player,HeadColor,TorsoColor,LArmColor,RArmColor,LLegColor,RLegColor);
end

function InitalizeClientAppearance(Player,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID)
	local newCharApp = Instance.new("IntValue",Player);
	newCharApp.Name = "Appearance";
	--BODY COLORS
	for i=1,6,1 do
		local BodyColor = Instance.new("BrickColorValue",newCharApp);
		if (i == 1) then
			if (HeadColorID ~= nil) then
				BodyColor.Value = HeadColorID;
			else
				BodyColor.Value = BrickColor.DarkGray();
			end
			BodyColor.Name = "HeadColor";
		elseif (i == 2) then
			if (TorsoColorID ~= nil) then
				BodyColor.Value = TorsoColorID;
			else
				BodyColor.Value = BrickColor.DarkGray();
			end
			BodyColor.Name = "TorsoColor";
		elseif (i == 3) then
			if (LeftArmColorID ~= nil) then
				BodyColor.Value = LeftArmColorID;
			else
				BodyColor.Value = BrickColor.DarkGray();
			end
			BodyColor.Name = "LeftArmColor";
		elseif (i == 4) then
			if (RightArmColorID ~= nil) then
				BodyColor.Value = RightArmColorID;
			else
				BodyColor.Value = BrickColor.DarkGray();
			end
			BodyColor.Name = "RightArmColor";
		elseif (i == 5) then
			if (LeftLegColorID ~= nil) then
				BodyColor.Value = LeftLegColorID;
			else
				BodyColor.Value = BrickColor.DarkGray();
			end
			BodyColor.Name = "LeftLegColor";
		elseif (i == 6) then
			if (RightLegColorID ~= nil) then
				BodyColor.Value = RightLegColorID;
			else
				BodyColor.Value = BrickColor.DarkGray();
			end
			BodyColor.Name = "RightLegColor";
		end
		local indexValue = Instance.new("NumberValue");
		indexValue.Name = "ColorIndex";
		indexValue.Parent = BodyColor;
		indexValue.Value = i;
	end
end

function CSR06Server(Port,PlayerLimit)
	Server = game:GetService("NetworkServer")
	RunService = game:GetService("RunService")
	Server:start(Port, 20)
	RunService:run();
	game:GetService("Players").MaxPlayers = PlayerLimit;
	game:GetService("Players").PlayerAdded:connect(function(Player)
	if (game:GetService("Players").NumPlayers > game:GetService("Players").MaxPlayers) then
		local message = Instance.new("Message")
		message.Text = "You were kicked. Reason: Too many players on server."
		message.Parent = Player
		wait(2)
		Player:remove()
		print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Too many players on server.");
	else
		print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' added");
		Player:LoadCharacter();
		LoadCharacterNew(newWaitForChild(Player,"Appearance"),Player.Character);
	end
		while true do 
			wait(0.001)
			if (Player.Character ~= nil) then
				if (Player.Character.Humanoid.Health == 0) then
					wait(5)
					Player:LoadCharacter()
					LoadCharacterNew(newWaitForChild(Player,"Appearance"),Player.Character);
				elseif (Player.Character.Parent == nil) then 
					wait(5)
					Player:LoadCharacter() -- to make sure nobody is deleted.
					LoadCharacterNew(newWaitForChild(Player,"Appearance"),Player.Character);
				end
			end
		end
	end)
	game:GetService("Players").PlayerRemoving:connect(function(Player)
		print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' leaving")	
	end)
	game:GetService("RunService"):Run();
	pcall(function() game.Close:connect(function() Server:Stop(); end) end);
	Server.IncommingConnection:connect(IncommingConnection);
end

function CSR06Connect(UserID,ServerIP,ServerPort,PlayerName,SkinPattern,LegsPattern,TorsoPattern)
	pcall(function() game:SetPlaceID(-1, false) end);
	local suc, err = pcall(function()
		client = game:GetService("NetworkClient")
		player = game:GetService("Players"):CreateLocalPlayer(UserID) 
		player:SetSuperSafeChat(false);
		pcall(function() player:SetUnder13(false) end);
		pcall(function() player:SetAccountAge(365) end);
		player.CharacterAppearance=0;
		pcall(function() player.Name=PlayerName or ""; end);
		game:GetService("Visit");
		PlayerColors(player,SkinPattern,LegsPattern,TorsoPattern);
	end)
	
	local function dieerror(errmsg)
		game:SetMessage(errmsg)
		wait(math.huge)
	end

	if not suc then
		dieerror(err)
	end

	local function disconnect(peer,lostconnection)
		game:SetMessage("You have lost connection to the game")
	end
	
	local function connected(url, replicator)
		replicator.Disconnection:connect(disconnect)
		local marker = nil
		local suc, err = pcall(function()
			game:SetMessageBrickCount()
			marker = replicator:SendMarker()
		end)
		if not suc then
			dieerror(err)
		end
		marker.Received:connect(function()
			local suc, err = pcall(function()
				game:ClearMessage()
			end)
			if not suc then
				dieerror(err)
			end
		end)
	end

	local function rejected()
		dieerror("Failed to connect to the Game. (Connection rejected)")
	end

	local function failed(peer, errcode, why)
		dieerror("Failed to connect to the Game. (ID="..errcode.." ["..why.."])")
	end

	local suc, err = pcall(function()
		game:SetMessage("Connecting to server...");
		client.ConnectionAccepted:connect(connected)
		client.ConnectionRejected:connect(rejected)
		client.ConnectionFailed:connect(failed)
		client:Connect(ServerIP,ServerPort, 0, 20)
		game.GuiRoot.MainMenu["Toolbox"]:Remove()
		game.GuiRoot.MainMenu["Edit Mode"]:Remove()
		game.GuiRoot.RightPalette.ReportAbuse:Remove()
		game.GuiRoot.ChatMenuPanel:Remove()
	end)

	if not suc then
		local x = Instance.new("Message")
		x.Text = err
		x.Parent = workspace
		wait(math.huge)
		end
end

_G.CSR06Server=CSR06Server;
_G.CSR06Connect=CSR06Connect;