namespace UniGame.Ecs.Proto.GameEffects.ModificationEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Modification Effect Feature")]
    public sealed class ModificationEffectFeature : EffectFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessDestroyedModificationEffectSystem());
            ecsSystems.Add(new ProcessRemoveModificationEffectSystem());
            ecsSystems.Add(new ProcessSingleModificationEffectSystem());
            ecsSystems.Add(new ProcessModificationEffectSystem());

            return UniTask.CompletedTask;
        }
    }
}