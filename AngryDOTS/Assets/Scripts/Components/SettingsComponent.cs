using Unity.Entities;
using Unity.Mathematics;

public struct SettingsComponent : IComponentData
{
    public float PlayerCollisionRadius;
    public float EnemyCollisionRadius;
    public float3 Player;
}