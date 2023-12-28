using Godot;
using System;

public class Mob : Area2D, IMoveable
{
    [Export]
    public int Speed { get; set; }

    private void OnVisibleOnScreenNotifier2DScreenExited(){
        QueueFree();
    }
    private void OnAreaEntered(Node2D Area){
        QueueFree();
    }
    public void Move(float delta){
        Position = new Vector2(Position.x, Position.y - (1 * Speed) * delta);
    }
    public override void _Process(float delta)
    {
        Move(delta);
    }
}
