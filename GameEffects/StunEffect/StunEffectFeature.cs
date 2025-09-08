namespace UniGame.Ecs.Proto.GameEffects.StunEffect
{
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Stun Effect Feature")]
    public sealed class StunEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessStunEffectSystem());
            ecsSystems.Add(new ProcessDestroyedStunEffectSystem());
            
            return UniTask.CompletedTask;
        }
    }
}