> Verified on Windows 10 v21H2 and Godot v4.0.2 (C#)

Tracked at https://github.com/godotengine/godot/issues/76074

# Steps
0. Clone the repository
1. Open the mod project in Godot (`GameMod/project.godot`)
2. Build the mod project. This should generate a `GameMod.dll` file at
   `GameMod/.godot/mono/temp/bin/Debug/GameMod.dll`.
3. Export the project via `Project > Export`
   1. Download the export templates if needed
   2. Export for Windows Desktop or Linux/X11
      1. This MCVE was verified on Windows only, but the project where this bug
         was originally encountered has the same behavior on Windows and Linux.
   3. Export the project as a .pck file to `GameMod/GameMod.pck`
4. Close the mod project and open the Game project (`GameCore/project.godot`)
5. Launch the game

Once the game launches, an error should appear under the debugger tab:
```
E 0:00:00:0724   Godot.NativeInterop.NativeFuncs.generated.cs:332 @ void Godot.NativeInterop.NativeFuncs.godotsharp_method_bind_ptrcall(IntPtr , IntPtr , System.Void** , System.Void* ): Cannot instance script because the associated class could not be found. Script: 'res://Foo.cs'. Make sure the script exists and contains a class definition with a name that matches the filename of the script exactly (it's case-sensitive).
  <C++ Error>    Method/function failed. Returning: false
  <C++ Source>   modules/mono/csharp_script.cpp:2301 @ can_instantiate()
  <Stack Trace>  Godot.NativeInterop.NativeFuncs.generated.cs:332 @ void Godot.NativeInterop.NativeFuncs.godotsharp_method_bind_ptrcall(IntPtr , IntPtr , System.Void** , System.Void* )
                 NativeCalls.cs:3435 @ Godot.GodotObject Godot.NativeCalls.godot_icall_1_371(IntPtr , IntPtr , Int32 )
                 PackedScene.cs:126 @ Godot.Node Godot.PackedScene.Instantiate(Godot.PackedScene+GenEditState )
                 Game.cs:77 @ void Game._Process(Double )
                 Node.cs:1795 @ Boolean Godot.Node.InvokeGodotClassMethod(Godot.NativeInterop.godot_string_name& , Godot.NativeInterop.NativeVariantPtrArgs , Godot.NativeInterop.godot_variant& )
                 CanvasItem.cs:1371 @ Boolean Godot.CanvasItem.InvokeGodotClassMethod(Godot.NativeInterop.godot_string_name& , Godot.NativeInterop.NativeVariantPtrArgs , Godot.NativeInterop.godot_variant& )
                 Node2D.cs:516 @ Boolean Godot.Node2D.InvokeGodotClassMethod(Godot.NativeInterop.godot_string_name& , Godot.NativeInterop.NativeVariantPtrArgs , Godot.NativeInterop.godot_variant& )
                 Game_ScriptMethods.generated.cs:31 @ Boolean Game.InvokeGodotClassMethod(Godot.NativeInterop.godot_string_name& , Godot.NativeInterop.NativeVariantPtrArgs , Godot.NativeInterop.godot_variant& )
                 CSharpInstanceBridge.cs:24 @ Godot.NativeInterop.godot_bool Godot.Bridge.CSharpInstanceBridge.Call(IntPtr , Godot.NativeInterop.godot_string_name* , Godot.NativeInterop.godot_variant** , Int32 , Godot.NativeInterop.godot_variant_call_error* , Godot.NativeInterop.godot_variant* )
```
