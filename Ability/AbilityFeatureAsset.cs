namespace UniGame.Ecs.Proto.Ability
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    [CreateAssetMenu(menuName = "ECS Proto/Features/Ability Feature",fileName = "Ability Feature")]
    public class AbilityFeatureAsset : BaseLeoEcsFeature
    {
#if ODIN_INSPECTOR
        [HideLabel]
        [InlineProperty]
#endif 
        public AbilityFeature abilityFeature = new();

        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            return abilityFeature.InitializeAsync(ecsSystems);
        }
    }
}
