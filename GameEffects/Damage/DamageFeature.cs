namespace UniGame.Ecs.Proto.Gameplay.Damage
{
    using System.Collections.Generic;
    using Components.Events;
    using Components.Request;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Sirenix.OdinInspector;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
    [CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Damage Feature",fileName = "Damage Feature")]
    public class DamageFeature  : BaseLeoEcsFeature
    {
        public List<DamageSubFeature> damageFeatures = new List<DamageSubFeature>();
        
        public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {

            //if unit ready to death then create KillRequest
            ecsSystems.DelHere<MadeDamageEvent>();
            ecsSystems.DelHere<BlockedDamageEvent>();
            ecsSystems.DelHere<CriticalDamageEvent>();
            
            //recalculate critical damage and increase value by critical multiplyer
            ecsSystems.Add(new CalculateCriticalDamageSystem());

            foreach (var feature in damageFeatures)
                await feature.BeforeDamageSystem(ecsSystems);
            
            //apply damage to health
            ecsSystems.Add(new ApplyDamageSystem());

            foreach (var feature in damageFeatures)
                await feature.AfterDamageSystem(ecsSystems);
            
            ecsSystems.DelHere<ApplyDamageRequest>();
        }
        
        [Button]
        public void FillFeatures()
        {
#if UNITY_EDITOR
            var features = AssetEditorTools.GetAssets<DamageSubFeature>();
            damageFeatures.Clear();
            damageFeatures.AddRange(features);
            this.MarkDirty();
#endif
        }
    }
}
