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
    }

    public override void _PhysicsProcess(double delta)
    {
        var mouse_pos = GetGlobalMousePosition();
        LookAt(mouse_pos);

        if (Input.IsActionPressed("shoot"))
        {
            Shoot();
        }

        if (mouse_pos.X < GlobalPosition.X)
        {
            sprite.FlipV = true;
        }
        else
        {
            sprite.FlipV = false;
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
