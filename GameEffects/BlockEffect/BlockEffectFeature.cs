namespace UniGame.Ecs.Proto.GameEffects.DamageEffect
{
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsProto;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Block Effect Feature")]
    public sealed class BlockEffectFeature : EffectFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            return UniTask.CompletedTask;
        }
    }
}