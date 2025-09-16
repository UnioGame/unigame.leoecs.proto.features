namespace UniGame.Ecs.Proto.GameEffects.ImmobilityEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Immobility Effect Feature")]
    public sealed class ImmobilityEffectFeature : EffectFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessImmobilityEffectSystem());
            ecsSystems.Add(new ProcessDestroyedBlockMovementEffectSystem());
            
            return UniTask.CompletedTask;
        }
    }
}