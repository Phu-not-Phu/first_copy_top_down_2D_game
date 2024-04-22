using Godot;
using System;

public partial class gun : Area2D
{
    static Marker2D shootingPoint;
    AnimatedSprite2D sprite;
    AudioStreamPlayer2D gunSFX;

    public override void _Ready()
    {
        shootingPoint = GetNode<Marker2D>("%ShootingPoint");
        sprite = GetNode<AnimatedSprite2D>("WeaponPivot/AnimatedSprite2D");
        gunSFX = GetNode<AudioStreamPlayer2D>("Gun_SFX");

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
            
            if(target_enemy.GlobalPosition.X < GlobalPosition.X)
            {
                sprite.FlipV = true;
            }
            else
            {
                sprite.FlipV = false;
            }
        }
    }

    public void Shoot()
    {
        gunSFX.Play();

        PackedScene bullet = ResourceLoader.Load<PackedScene>("res://src/scenes/bullet.tscn");
        var new_bullet = (Node2D)bullet.Instantiate();

        new_bullet.GlobalPosition = shootingPoint.GlobalPosition;
        new_bullet.GlobalRotation = shootingPoint.GlobalRotation;

        shootingPoint.AddChild(new_bullet);
    }
}
