namespace UniGame.Ecs.Proto.GameEffects.FreezeEffect
{
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Freeze Effect Feature")]
    public class FreezeEffectFeature : EffectFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new CreateFreezeEffectSystem());
            ecsSystems.Add(new ApplyFreezeEffectSystem());
            ecsSystems.DelHere<ApplyFreezeTargetEffectRequest>();
            ecsSystems.Add(new ProcessFreezeEffectSystem());
            ecsSystems.Add(new RemoveFreezeEffectSystem());
            ecsSystems.DelHere<RemoveFreezeTargetEffectRequest>();
            
            return UniTask.CompletedTask;
        }
    }
}