using Unity.Entities;
using Unity.Transforms;

partial class MoveForwardSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = SystemAPI.Time.DeltaTime;
        Entities
            .WithAll<MoveForward, MoveSpeed>()
            .ForEach((TransformAspect transform, MoveSpeed moveSpeed) =>
            {
                var pos = transform.Position;
                transform.Position = pos + (dt * moveSpeed.Value * transform.Forward);
            })
            .Schedule();
    }
}