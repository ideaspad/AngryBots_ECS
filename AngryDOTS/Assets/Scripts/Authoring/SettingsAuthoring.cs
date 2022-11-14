using Unity.Entities;

public class SettingsAuthoring : UnityEngine.MonoBehaviour
{
    public UnityEngine.Transform Player;
    public float PlayerCollisionRadius;
    public float EnemyCollisionRadius;
}

class SettingsBaker : Baker<SettingsAuthoring>
{
    public override void Bake(SettingsAuthoring authoring)
    {
        AddComponent(new SettingsComponent
        {
            EnemyCollisionRadius = authoring.EnemyCollisionRadius,
            PlayerCollisionRadius = authoring.PlayerCollisionRadius,
            Player = authoring.Player.position
        });
    }
}