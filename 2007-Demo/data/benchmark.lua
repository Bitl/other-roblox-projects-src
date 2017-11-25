settings().Rendering.Shadows = true
game.Workspace:InsertContent("rbxasset://../../data/benchmarklib.rbxm")
game.GuiRoot:Remove()
game:GetService("Visit")
game:GetService("RunService"):run()
game:GetService("NetworkClient")