using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(MoveForwardSystem))]
[UpdateBefore(typeof(TimedDestroySystem))]
partial struct CollisionSystem : ISystem
{
    EntityQuery enemyGroup;
    EntityQuery bulletGroup;
    EntityQuery playerGroup;

    public void OnCreate(ref SystemState state)
    {
        playerGroup = state.GetEntityQuery(
            typeof(Health),
            ComponentType.ReadOnly<TransformAspect>(),
            ComponentType.ReadOnly<PlayerTag>()
        );
        enemyGroup = state.GetEntityQuery(
            typeof(Health),
            ComponentType.ReadOnly<TransformAspect>(),
            ComponentType.ReadOnly<EnemyTag>()
        );
        bulletGroup = state.GetEntityQuery(
            typeof(TimeToLive),
            ComponentType.ReadOnly<TransformAspect>()
        );
    }

    public void OnDestroy(ref SystemState state)
    {
    }

    public void OnUpdate(ref SystemState state)
    {
    }
}

[BurstCompile]
partial struct CollisionJob : IJobEntity
{
    bool CheckCollision(float3 posA, float3 posB, float radiusSqr)
    {
        float3 delta = posA - posB;
        float distanceSquare = delta.x * delta.x + delta.z * delta.z;
        return distanceSquare <= radiusSqr;
    }
}