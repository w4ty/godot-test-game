using System.Numerics;

public interface IProjectile
{
    int Speed { get; set; }
    
    void Move(float delta);
}
