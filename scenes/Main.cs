using Godot;
using System;

public class Main : Node
{
    [Export]
    public PackedScene MobScene { get; set; }

    private int _score;

    public override void _Ready(){
        NewGame();
    }

    public void GameOver(){
        GetNode<Timer>("SpawnTimer").Stop();
        GetNode<Timer>("PlayTimer").Stop();
    }

    public void NewGame(){
        _score = 0;
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Node2D>("StartPosition");
        player.Start(startPosition.Position);

        GetNode<Timer>("StartTimer").Start();
    }

    private void OnPlayTimerTimeout(){
        _score++;
    }

    private void OnStartTimerTimeout(){
        GetNode<Timer>("SpawnTimer").Start();
        GetNode<Timer>("PlayTimer").Start();
    }

    private void OnSpawnTimerTimeout(){
        Mob mob = MobScene.Instance<Mob>();

        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");

        mobSpawnLocation.UnitOffset = GD.Randf();

        mob.Position = mobSpawnLocation.Position;

        var velocity = new Vector2(0, (float)GD.RandRange(200.0, 250.0));
        mob.LinearVelocity = velocity;

        AddChild(mob);
    }
}