using Unity.Entities;
using Unity.Transforms;


[UpdateAfter(typeof(MoveForwardSystem))]
partial struct TimedDestroySystem : ISystem
{
	public void OnCreate(ref SystemState state)
	{
		
	}

	public void OnDestroy(ref SystemState state)
	{
	}

	public void OnUpdate(ref SystemState state)
	{
	}
}

