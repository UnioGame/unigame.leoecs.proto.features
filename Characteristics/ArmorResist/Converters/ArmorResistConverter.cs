namespace UniGame.Ecs.Proto.Characteristics.ArmorResist.Converters
{
	using System;
	using Base.Components.Requests;
	using Components;
	using Leopotam.EcsProto;

	using UniGame.LeoEcs.Converter.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
	[Serializable]
	public class ArmorResistConverter : LeoEcsConverter
	{
		#region Inspector
		
		public float physicalResist;
		
#if ODIN_INSPECTOR
		[MaxValue(100)]
#endif
		public float maxValue = 100f;
		
#if ODIN_INSPECTOR
		[MinValue(0)]
#endif
		public float minValue = 0f;

		#endregion
		
		public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
		{
			ref var createCharacteristicRequest = ref world.AddComponent<CreateCharacteristicRequest<ArmorResistComponent>>(entity);
			createCharacteristicRequest.Value = physicalResist;
			createCharacteristicRequest.MaxValue = maxValue;
			createCharacteristicRequest.MinValue = minValue;
			createCharacteristicRequest.Owner = entity.PackEntity(world);
            
			ref var valueComponent = ref world.AddComponent<ArmorResistComponent>(entity);
			valueComponent.Value = physicalResist;
		}
	}
}