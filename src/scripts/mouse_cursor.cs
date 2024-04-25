using Godot;
using System;

public partial class mouse_cursor : Node2D
{
    [Export] float cursor_scaleX;
    [Export] float cursor_scaleY;
    Texture empty_cursor = null;
    AnimatedSprite2D cursor_sprite;
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;
        cursor_sprite = GetNode<AnimatedSprite2D>("CanvasLayer/AnimatedSprite2D");
        cursor_sprite.ApplyScale(new Vector2(cursor_scaleX, cursor_scaleY));
    }

    public override void _Process(double delta)
    {
        var mouse_pos = GetGlobalMousePosition();
        cursor_sprite.GlobalPosition = mouse_pos;

        if (Input.IsActionJustPressed("escape"))
        {
            Input.MouseMode = Input.MouseModeEnum.Confined;
        }
    }
}
