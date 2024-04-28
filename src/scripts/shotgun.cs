using Godot;
using System;

public partial class shotgun : Area2D
{
	static Marker2D shootingPoint;
	AnimatedSprite2D sprite;
	AnimationPlayer animPlayer;
	bool isShooting = false;

	public override void _Ready()
	{
		shootingPoint = GetNode<Marker2D>("%ShootingPoint");
		sprite = GetNode<AnimatedSprite2D>("WeaponPivot/AnimatedSprite2D");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public override void _PhysicsProcess(double delta)
	{
		var mouse_pos = GetGlobalMousePosition();
		LookAt(mouse_pos);

		if (isShooting == false)
		{
			if (Input.IsActionPressed("shoot"))
			{
				animPlayer.Play("shoot");
			}
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
		SetShootingTrue();
		
		PackedScene bullet = ResourceLoader.Load<PackedScene>("res://src/scenes/bullet.tscn");
		var new_bullet_middle = (Node2D)bullet.Instantiate();
		var new_bullet_top = (Node2D)bullet.Instantiate();
		var new_bullet_bottom = (Node2D)bullet.Instantiate();

		new_bullet_middle.GlobalPosition = shootingPoint.GlobalPosition;
		new_bullet_middle.GlobalRotation = shootingPoint.GlobalRotation;

		new_bullet_top.GlobalPosition = shootingPoint.GlobalPosition;
		new_bullet_top.GlobalRotation = shootingPoint.GlobalRotation + Mathf.DegToRad(-5);

		new_bullet_bottom.GlobalPosition = shootingPoint.GlobalPosition;
		new_bullet_bottom.GlobalRotation = shootingPoint.GlobalRotation + Mathf.DegToRad(5);

		shootingPoint.AddChild(new_bullet_top);
		shootingPoint.AddChild(new_bullet_middle);
		shootingPoint.AddChild(new_bullet_bottom);
	}

	public void SetShootingTrue()
	{
		isShooting = true;
	}	

	public void SetShootingFalse()
	{
		isShooting = false;
	}
}
