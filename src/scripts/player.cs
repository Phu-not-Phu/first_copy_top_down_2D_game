using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Signal]
	public delegate void HealthDeleptedEventHandler();

	const float DAMAGE_RATE = 50.0f;
	[Export] public float speed = 200;
	[Export] public float health = 100;
	public AnimationPlayer animPlayer;
	public Area2D hurtbox;
	public ProgressBar healthBar;

	public override void _Ready()
	{
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		hurtbox = GetNode<Area2D>("Hurtbox");
		healthBar = GetNode<ProgressBar>("HealthBar");

		animPlayer.Play("idle");
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction = Input.GetVector("left", "right", "up", "down");
		Vector2 velocity = Velocity;
		velocity = direction * speed;

		if (velocity.Length() > 0)
		{
			animPlayer.Play("walk");
		}
		else
		{
			animPlayer.Play("idle");
		}

		Velocity = velocity;
		MoveAndSlide();

		var overlapping_mobs = hurtbox.GetOverlappingBodies();
		if (overlapping_mobs.Count > 0)
		{
			health -= DAMAGE_RATE * overlapping_mobs.Count * (float)delta;
			healthBar.Value = health;

			if (health <= 0)
			{
				EmitSignal(nameof(HealthDelepted));
			}
		}
	}
}
