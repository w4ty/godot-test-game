public interface IFireable
{
    float FireRate { get; set; }
    float ShotDelay { get; set; }
    
    void Fire();
}
