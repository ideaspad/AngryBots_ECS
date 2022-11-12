using Unity.Entities;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class RemoveDeadSystem : ISystem
{
	protected override void OnUpdate()
	{
		Entities
			.ForEach((Entity entity, ref Health health, ref TransformAspect transform) =>
		{
			if (health.Value <= 0)
			{
				if (EntityManager.HasComponent(entity, typeof(PlayerTag)))
				{
					Settings.PlayerDied();
				}
				else if (EntityManager.HasComponent(entity, typeof(EnemyTag)))
				{
					PostUpdateCommands.DestroyEntity(entity);
					BulletImpactPool.PlayBulletImpact(transform.Position);
				}
			}
		});
	}

	public void OnCreate(ref SystemState state)
	{
		
	}

	public void OnDestroy(ref SystemState state)
	{
	}

	public void OnUpdate(ref SystemState state)
	{
		
		var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
		var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
		foreach (var transform in SystemAPI.Query<TransformAspect>().WithAll<Health>())
		{
			transform.RotateWorld(rotation);
		}
		
		if (health.Value <= 0)
		{
			if (EntityManager.HasComponent(entity, typeof(PlayerTag)))
			{
				Settings.PlayerDied();
			}
			else if (EntityManager.HasComponent(entity, typeof(EnemyTag)))
			{
				PostUpdateCommands.DestroyEntity(entity);
				BulletImpactPool.PlayBulletImpact(transform.Position);
			}
		}
	}
}