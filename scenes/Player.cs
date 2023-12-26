using Godot;
using System;
using System.Diagnostics;

public class Player : Area2D
{
	[Export]
	public int Speed {get; set; } = 400;
	
	public Vector2 ScreenSize;
	
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var velocity = Vector2.Zero;
		velocity.x += Input.GetActionStrength("move_right");
		velocity.x -= Input.GetActionStrength("move_left");
		velocity.y -= Input.GetActionStrength("move_up");
		velocity.y += Input.GetActionStrength("move_down");
		
		if(velocity.Length() > 0){
			velocity = velocity.Normalized() * Speed;
		}

		Position += velocity * (float)delta;
		Position = new Vector2(
    		x: Mathf.Clamp(Position.x, 32, ScreenSize.x -32),
    		y: Mathf.Clamp(Position.y, 32, ScreenSize.y -32)
		);
	}
}
