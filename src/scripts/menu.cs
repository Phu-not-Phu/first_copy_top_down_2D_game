using Godot;
using System;

public partial class menu : Control
{
    private void OnPlayPressed()
    {
        GetTree().ChangeSceneToFile("res://src/scenes/world.tscn");
    }


    private void OnOptionsPressed()
    {
        // Replace with function body.
    }


    private void OnExitPressed()
    {
        GetTree().Quit();
    }
}



