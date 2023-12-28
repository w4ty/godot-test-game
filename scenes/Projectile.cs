using Godot;
using System;

public class Projectile : Area2D, IProjectile
{
    [Export]
    public int Speed { get; set; }

    public void Move(float delta){
        Position = new Vector2(Position.x, Position.y - (1 * Speed) * delta);
    }
    public override void _Process(float delta)
    {
        Move(delta);
    }
    private void OnVisibilityNotifier2DScreenExited(){
        QueueFree();
    }
}
