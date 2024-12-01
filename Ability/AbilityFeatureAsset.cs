namespace UniGame.Ecs.Proto.Ability
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    
    [CreateAssetMenu(menuName = "ECS Proto/Features/Ability Feature",fileName = "Ability Feature")]
    public class AbilityFeatureAsset : BaseLeoEcsFeature
    {
        [HideLabel]
        [InlineProperty]
        public AbilityFeature abilityFeature = new();

        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            return abilityFeature.InitializeAsync(ecsSystems);
        }
    }
}
