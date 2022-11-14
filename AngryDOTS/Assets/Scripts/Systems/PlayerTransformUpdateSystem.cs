using Unity.Entities;
using Unity.Transforms;

[UpdateBefore(typeof(CollisionSystem))]
partial class PlayerTransformUpdateSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .WithAll<PlayerTag>()
            .ForEach((TransformAspect transform) =>
            {
                transform.Position = SystemAPI.GetSingleton<SettingsComponent>().Player;
            }).Schedule();
    }
}