namespace UniGame.Ecs.Proto.GameEffects.BlockAutoAttackEffect
{
	using Cysharp.Threading.Tasks;
	using Effects.Feature;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Block Auto Attack Effect Feature")]
	public class BlockAutoAttackEffectFeature : EffectFeature
	{
		public override UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			// Adds the effect of blocking auto attack on an ability.
			ecsSystems.Add(new ProcessBlockAutoAttackEffectSystem());
			// Removes the effect of blocking auto attack on an ability. Check PauseAbilityComponent.
			// If duration is over, then remove the effect.
			ecsSystems.Add(new RemoveBlockAutoAttackEffectSystem());
			return UniTask.CompletedTask;
		}
	}
}