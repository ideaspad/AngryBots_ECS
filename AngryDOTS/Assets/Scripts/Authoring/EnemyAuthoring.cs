using Unity.Entities;
using UnityEngine.Serialization;

public class EnemyAuthoring : UnityEngine.MonoBehaviour
{
    public float Speed;
    public float EnemyHealth;
}

class EnemyBaker : Baker<EnemyAuthoring>
{
    public override void Bake(EnemyAuthoring authoring)
    {
        AddComponent<EnemyTag>();
        AddComponent<MoveForward>();
        AddComponent(new MoveSpeed
        {
            Value = authoring.Speed
        });
        AddComponent(new Health
        {
            Value = authoring.EnemyHealth
        });
    }
}