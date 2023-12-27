using Godot;
using System;

public class Mob : RigidBody2D
{
    private void OnVisibleOnScreenNotifier2DScreenExited(){
        QueueFree();
    }
}
