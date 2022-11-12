using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(MoveForwardSystem))]
[UpdateBefore(typeof(TimedDestroySystem))]
public class CollisionSystem : ISystem
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
        var healthType = GetArchetypeChunkComponentType<Health>(false);
        var translationType = GetArchetypeChunkComponentType<Translation>(true);

        float enemyRadius = Settings.EnemyCollisionRadius;
        float playerRadius = Settings.PlayerCollisionRadius;

        var jobEvB = new CollisionJob()
        {
            radius = enemyRadius * enemyRadius,
            healthType = healthType,
            translationType = translationType,
            transToTestAgainst = bulletGroup.ToComponentDataArray<Translation>(Allocator.TempJob)
        };

        JobHandle jobHandle = jobEvB.Schedule(enemyGroup, inputDependencies);

        if (Settings.IsPlayerDead())
            return;

        var jobPvE = new CollisionJob()
        {
            radius = playerRadius * playerRadius,
            healthType = healthType,
            translationType = translationType,
            transToTestAgainst = enemyGroup.ToComponentDataArray<Translation>(Allocator.TempJob)
        };

        jobPvE.Schedule(playerGroup, jobHandle);
    }
}

[BurstCompile]
partial struct CollisionJob : IJobEntity
{
    public float radius;

    public ComponentLookup<Health> healthType;
    [ReadOnly] public ComponentLookup<TransformAspect> translationType;

    [DeallocateOnJobCompletion] [ReadOnly] public NativeArray<TransformAspect> transToTestAgainst;

    void Execute(Entity entity, int index, [ReadOnly] ref TransformAspect translation)
    {
        for (int i = 0; i < transToTestAgainst.Length; i++)
        {
            var other = transToTestAgainst[i];
            var dist = math.distance(translation.Position, other.Position);
            if (dist < radius)
            {
                healthType[entity] = new Health() { Value = 0 };
                break;
            }
        }

        var chunkHealths = chunk.GetNativeArray(healthType);
        var chunkTranslations = chunk.GetNativeArray(translationType);

        for (int i = 0; i < chunk.Count; i++)
        {
            float damage = 0f;
            Health health = chunkHealths[i];
            Translation pos = chunkTranslations[i];

            for (int j = 0; j < transToTestAgainst.Length; j++)
            {
                Translation pos2 = transToTestAgainst[j];

                if (CheckCollision(pos.Value, pos2.Value, radius))
                {
                    damage += 1;
                }
            }

            if (damage > 0)
            {
                health.Value -= damage;
                chunkHealths[i] = health;
            }
        }
    }

    bool CheckCollision(float3 posA, float3 posB, float radiusSqr)
    {
        float3 delta = posA - posB;
        float distanceSquare = delta.x * delta.x + delta.z * delta.z;
        return distanceSquare <= radiusSqr;
    }
}