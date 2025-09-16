namespace UniGame.Ecs.Proto.GameEffects.DamageEffect
{
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsProto;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Ability Radius Effect Feature")]
    public sealed class AbilityRadiusEffectFeature : EffectFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {

            return UniTask.CompletedTask;
        }
    }
}