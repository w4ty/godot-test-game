using Godot;
using System;
using System.Diagnostics;

public class Player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();

	[Export]
	public int Speed {get; set; } = 400;
	
	public Vector2 ScreenSize;
	
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Position = new Vector2(640, 600);
	}

	public void Start(Vector2 position){
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
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

	private void OnBodyEntered(Node2D body){
		Hide();
		EmitSignal("Hit");
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("Disabled", true);
	}
}
