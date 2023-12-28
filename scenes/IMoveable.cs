using System.Numerics;

public interface IMoveable
{
    int Speed { get; set; }
    
    void Move(float delta);
}
