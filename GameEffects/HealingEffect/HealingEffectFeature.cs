namespace UniGame.Ecs.Proto.GameEffects.HealingEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Events.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Healing Effect Feature")]
    public sealed class HealingEffectFeature : EffectFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.DelHere<MadeHealEvent>();
            ecsSystems.Add(new ProcessHealingEffectSystem());
            
            return UniTask.CompletedTask;
        }
    }
}