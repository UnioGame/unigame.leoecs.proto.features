namespace UniGame.Ecs.Proto.Gameplay.CriticalAttackChance
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Critical Attack Chance Feature",fileName = "Critical Attack Chance Feature")]
    public class CriticalAttackChanceFeature  : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            
            //recalculate critical change after attack and mark with CriticalAttackMarkerComponent
            //if next attack should be critical
            ecsSystems.Add(new RecalculateCriticalChanceSystem());
            //on new circle recalculate critical chance after damage event ready to gone
            //detect attack and check can it be critical or not
            ecsSystems.Add(new DetectAttackDamageEventSystem());
            
            return UniTask.CompletedTask;
        }
    }
}
