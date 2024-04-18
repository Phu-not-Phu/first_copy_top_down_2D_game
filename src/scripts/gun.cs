using Godot;
using System;

public partial class gun : Area2D
{
    static Marker2D shootingPoint;

    public override void _Ready()
    {
        shootingPoint = GetNode<Marker2D>("%ShootingPoint");
        Timer timer = GetNode<Timer>("Timer");
        timer.Timeout += () => Shoot();
    }

    public override void _PhysicsProcess(double delta)
    {
        var enemies_in_range = GetOverlappingBodies();
        if (enemies_in_range.Count > 0)
        {
            var target_enemy = enemies_in_range[0];
            LookAt(target_enemy.GlobalPosition);
        }
    }

    public static void Shoot()
    {
        PackedScene bullet = ResourceLoader.Load<PackedScene>("res://src/scenes/bullet.tscn");
        var new_bullet = (Node2D)bullet.Instantiate();

        new_bullet.GlobalPosition = shootingPoint.GlobalPosition;
        new_bullet.GlobalRotation = shootingPoint.GlobalRotation;

        shootingPoint.AddChild(new_bullet);
    }
}
