using Godot;
using System;

public partial class death_effect : Node2D
{
    public override void _Ready()
    {
        Timer timer = GetNode<Timer>("Timer");
        timer.Timeout += () => QueueFree();
    }
}
