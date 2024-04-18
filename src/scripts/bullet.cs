using Godot;
using System;

public partial class bullet : Area2D
{

    [Export] public float speed = 1000;
    const float MAX_TRAVEL_DISTANCE = 1000;
    float travelled_distance = 0;

    public override void _PhysicsProcess(double delta)
    {
        var direction = Vector2.Right.Rotated(Rotation);
        Position += direction * speed * (float)delta;

        travelled_distance += speed * (float)delta;

        if (travelled_distance > MAX_TRAVEL_DISTANCE)
        {
            QueueFree();
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        QueueFree();
        if (body.HasMethod("Take_damage"))
        {
            body.Call("Take_damage");
        }
    }
}
