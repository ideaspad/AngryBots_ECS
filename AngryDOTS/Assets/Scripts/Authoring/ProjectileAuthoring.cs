using Unity.Entities;

public class ProjectileAuthoring : UnityEngine.MonoBehaviour
{
    public float Speed;
    public float TimeToLive;
}

class ProjectileBaker : Baker<ProjectileAuthoring>
{
    public override void Bake(ProjectileAuthoring authoring)
    {
        AddComponent<MoveForward>();
        AddComponent(new MoveSpeed
        {
            Value = authoring.Speed
        });
        AddComponent(new TimeToLive
        {
            Value = authoring.TimeToLive
        });
    }
}