settings().Rendering.Shadows = true
game.Workspace:InsertContent("rbxasset://../../data/libraries.rbxm")
local plr = game.Players:CreateLocalPlayer(0)
plr:LoadCharacter()
local Colors = game.Workspace.Colors:Clone()
Colors.Parent = plr.Character
game.Workspace.MOTD.Parent = plr.Character
game:GetService("Visit")
game:GetService("RunService"):run()
game:GetService("NetworkClient")