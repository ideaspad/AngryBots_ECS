using Unity.Entities;
using UnityEngine;

public class MoveSpeedAuthoring : MonoBehaviour
{
	public float speed = 50f;
}

class MoveForwardBaker : Baker<MoveSpeedAuthoring>
{
	public override void Bake(MoveSpeedAuthoring authoring)
	{
		AddComponent<MoveForward>();
		AddComponent(new MoveSpeed
		{
			Value =  authoring.speed
		});
	}
}