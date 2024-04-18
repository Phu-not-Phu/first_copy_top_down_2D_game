using Godot;
using System;

public partial class world : Node2D
{
    PathFollow2D pathFollow;
    CanvasLayer gameOverScreen;
    public override void _Ready()
    {
        pathFollow = GetNode<PathFollow2D>("Path2D/PathFollow2D");
        gameOverScreen = GetNode<CanvasLayer>("GameOver");
        Timer timer = GetNode<Timer>("Timer");
        timer.Timeout += () => Spawn_mod();
    }

    public void Spawn_mod()
    {
        PackedScene mob_scene = ResourceLoader.Load<PackedScene>("res://src/scenes/mob.tscn");
        var new_mob = (Node2D)mob_scene.Instantiate();

        RandomNumberGenerator random = new();

        pathFollow.ProgressRatio = random.RandfRange(0, 1);

        new_mob.GlobalPosition = pathFollow.GlobalPosition;
        AddChild(new_mob);
    }

    private void OnPlayerHealthDelepted()
    {
        gameOverScreen.Visible = true;
        GetTree().Paused = true;
    }
}
