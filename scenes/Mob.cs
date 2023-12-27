using Godot;
using System;

public class Mob : RigidBody2D
{
    private void OnVisibleOnScreenNotifier2DScreenExited(){
        QueueFree();
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
