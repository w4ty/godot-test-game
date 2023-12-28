using Godot;
using System;
using System.Diagnostics;

public class Player : Area2D, IFireable
{
	[Signal]
	public delegate void HitEventHandler();

	[Export]
	public int Speed { get; set; } = 400;
	
	[Export]
	public PackedScene Projectile { get; set; }

	public Vector2 ScreenSize;
	
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Hide();
	}

	public void Start(Vector2 position){
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}

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
		if(Input.IsActionJustPressed("base_fire")){
			Fire();
		}
	}

	private void OnBodyEntered(Node2D body){
		Hide();
		EmitSignal("HitEventHandler");
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("Disabled", true);
	}

	public void Fire(){
		Projectile projectile = Projectile.Instance<Projectile>();
		projectile.Position = Position;
		Owner.AddChild(projectile);
		GD.Print("Fired");
	}
}
