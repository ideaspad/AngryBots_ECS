using Unity.Entities;
using Unity.Mathematics;

public class SettingsAuthoring : UnityEngine.MonoBehaviour
{
    public UnityEngine.Transform Player;
    public float PlayerCollisionRadius = .5f;
    public float EnemyCollisionRadius = .3f;
}

class SettingsBaker : Baker<SettingsAuthoring>
{
    public override void Bake(SettingsAuthoring authoring)
    {
        AddComponent(new );
    }
}