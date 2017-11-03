settings().Rendering.Shadows = true
local plr = game.Players:CreateLocalPlayer(0)
plr:LoadCharacter()
game.Workspace.MOTD.Parent = plr.Character
game:GetService("Visit")
game:GetService("RunService"):run()
game:GetService("NetworkClient")