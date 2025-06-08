namespace UniGame.Ecs.Proto.Shaders.Systems
{
	using System;
	using Aspects;
	using Components;
	using Game.Ecs.Core.Components;
	using LeoEcs.Bootstrap.Runtime.Abstract;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Components;
	using UnityEngine;

	/// <summary>
	/// Take champion position and set it to global mask
	/// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[ECSDI]
	[Serializable]
	public class ChampionGlobalMaskSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private UnityAspect _unityAspect;
		private ShadersAspect _shadersAspect;

		private ProtoIt _championFilter = It
			.Chain<TransformPositionComponent>()
			.Inc<ChampionComponent>()
			.End();
		
		private ProtoIt _globalMaskFilter = It
			.Chain<ChampionGlobalMaskComponent>()
			.End();

		public void Run()
		{
			foreach (var championEntity in _championFilter)
			{
				ref var transformComponent = ref _unityAspect.Position.Get(championEntity);
				ref var position = ref transformComponent.Position;
				foreach (var entity in _globalMaskFilter)
				{
					ref var championGlobalMask = ref _shadersAspect.GlobalMask.Get(entity);
					foreach (var championVariable in championGlobalMask.Variables)
					{
						foreach (var material in championGlobalMask.Materials)
						{
							#if UNITY_EDITOR
							if (material == null)
							{
								Debug.LogError($"Material is null for {championGlobalMask}");
								continue;
							}
							#endif

							var targetPosition = (Vector3)position;
							material.SetVector(championVariable, targetPosition);
						}
					}
				}
			}
		}
	}
}