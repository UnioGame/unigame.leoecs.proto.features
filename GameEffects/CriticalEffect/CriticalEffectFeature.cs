namespace UniGame.Ecs.Proto.GameEffects.CriticalEffect
{
	using Cysharp.Threading.Tasks;
	using Effects.Feature;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Bootstrap.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Critical Effect Feature", fileName = "Critical Effect Feature")]
	public class CriticalEffectFeature : EffectFeature
	{
		public override UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			ecsSystems.Add(new CriticalEffectSystem());
			return UniTask.CompletedTask;
		}
	}
}