using Godot;
using System;
using System.IO;
using System.Reflection;

public partial class Game : Node2D
{
	/// Only run the mod loading code once to limit the amount of messages printed
	private bool _modLoaded = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_modLoaded)
		{
			return;
		}
		_modLoaded = true;

		// This is where Godot will place the mod's assembly by default
		var modAssemblyPath = Path.Combine(
			System.Environment.CurrentDirectory,
			"..",
			"GameMod",
			".godot",
			"mono",
			"temp",
			"bin",
			"Debug",
			"GameMod.dll"
		);

		// This is the location where the mod's pck file is manually exported to
		var modPckPath = Path.Combine(
			System.Environment.CurrentDirectory,
			"..",
			"GameMod",
			"GameMod.pck"
		);

		// Sanity checks
		if (!File.Exists(modAssemblyPath))
		{
			GD.PrintErr("Mod assembly not found at: " + modAssemblyPath);
			return;
		}
		if (!File.Exists(modPckPath))
		{
			GD.PrintErr("Mod pck not found at: " + modPckPath);
			return;
		}

		// Load the mod assembly before the pck file
		// This is the order that's listed here:
		// https://docs.godotengine.org/en/stable/tutorials/export/exporting_pcks.html
		GD.Print("Loading mod assembly from: " + modAssemblyPath);
		Assembly.LoadFile(modAssemblyPath);

		GD.Print("Loading mod pck from: " + modPckPath);
		var success = ProjectSettings.LoadResourcePack(modPckPath);
		if (success)
		{
			GD.Print("Loaded mod pck");
		}
		else
		{
			GD.Print("Failed to load mod pck");
		}

		// The call to `.Instantiate()` is where the script not found error occurs
		var packedScene = ResourceLoader.Load<PackedScene>("res://Foo.tscn");
		var scene = packedScene.Instantiate();
	}
}
