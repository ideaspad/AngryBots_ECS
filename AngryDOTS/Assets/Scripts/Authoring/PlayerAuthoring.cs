using Unity.Entities;
using UnityEngine.Serialization;

public class PlayerAuthoring : UnityEngine.MonoBehaviour
{
    public float Speed;
    public float HealthValue = 1f;
}

class PlayerBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        AddComponent<MoveForward>();
        AddComponent(new MoveSpeed
        {
            Value = authoring.Speed
        });
        AddComponent(new Health
        {
            Value = authoring.HealthValue
        });
    }
}