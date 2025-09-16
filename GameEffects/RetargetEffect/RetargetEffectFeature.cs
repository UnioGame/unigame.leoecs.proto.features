﻿namespace UniGame.Ecs.Proto.GameEffects.RetargetEffect
{
	using Cysharp.Threading.Tasks;
	using Effects.Feature;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Retarget Effect Feature", fileName = "Retarget Effect Feature")]
	public class RetargetEffectFeature : EffectFeature
	{
		public override UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			ecsSystems.Add(new RetargetEffectSystem());
			return UniTask.CompletedTask;
		}
	}
}