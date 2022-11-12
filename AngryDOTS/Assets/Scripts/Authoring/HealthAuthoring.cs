using Unity.Entities;
using UnityEngine;

public class HealthAuthoring : MonoBehaviour
{
    public float healthValue = 1f;
}

class PlayerBaker : Baker<HealthAuthoring>
{
    public override void Bake(HealthAuthoring authoring)
    {
        AddComponent<PlayerTag>();
        AddComponent(new Health
        {
            Value = authoring.healthValue
        });
    }
}