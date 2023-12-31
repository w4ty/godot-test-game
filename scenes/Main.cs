using Godot;
using System;

public class Main : Node
{
    [Export]
    public PackedScene MobScene { get; set; }

    private int _score;

    public void GameOver(){
        GetNode<Timer>("SpawnTimer").Stop();
        GetNode<Timer>("PlayTimer").Stop();
        GetNode<HUD>("HUD").ShowGameOver();
    }

    public void NewGame(){
        _score = 0;
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Node2D>("StartPosition");
        player.Start(startPosition.Position);

        GetNode<Timer>("StartTimer").Start();
        var hud = GetNode<HUD>("HUD");
        hud.UpdateScore(_score);
        hud.ShowMessage("Get Ready!");

        foreach(Node mobs in GetTree().GetNodesInGroup("mobs")){
            mobs.QueueFree();
        }
    }

    private void OnPlayTimerTimeout(){
        _score++;
        GetNode<HUD>("HUD").UpdateScore(_score);
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

        AddChild(mob);
    }
}
