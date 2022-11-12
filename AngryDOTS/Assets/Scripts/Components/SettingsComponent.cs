using Unity.Entities;

namespace Components
{
    public class SettingsComponent : IComponentData
    {
        public float PlayerCollisionRadius;
        public float EnemyCollisionRadius;
        public TransformAuthoring Player;
    }
}