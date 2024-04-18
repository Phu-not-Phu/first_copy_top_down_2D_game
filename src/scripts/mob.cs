using Godot;
using System;

public partial class mob : CharacterBody2D
{
    [Export] public float speed = 100;
    [Export] public int health = 3;
    CharacterBody2D player;
    public AnimationPlayer animPlayer;

    public override void _Ready()
    {
        player = GetNode<CharacterBody2D>("/root/World/Player");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }
    public override void _PhysicsProcess(double delta)
    {
        var direction = GlobalPosition.DirectionTo(player.GlobalPosition);
        Vector2 velocity = Velocity;
        velocity = direction * speed;

        if (velocity.Length() > 0)
        {
            animPlayer.Play("walk");
        }
        else
        {
            animPlayer.Play("RESET");
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    public void Take_damage()
    {
        health -= 1;
        if (health <= 0)
        {
            QueueFree();

            PackedScene deathEffect = ResourceLoader.Load<PackedScene>("res://src/scenes/death_effect.tscn");
            var new_deathEffect = (Node2D)deathEffect.Instantiate();
            new_deathEffect.GlobalPosition = GlobalPosition;
            GetParent().AddChild(new_deathEffect);
        }
    }
}
