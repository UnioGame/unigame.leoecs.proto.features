namespace UniGame.Ecs.Proto.Gameplay.Damage
{
    using System.Collections.Generic;
    using System.Linq;
    using Components.Events;
    using Components.Request;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Runtime.Utils;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UniModules.Editor;
    using UnityEditor;
#endif
    
    [CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Damage Feature",fileName = "Damage Feature")]
    public class DamageFeature  : BaseLeoEcsFeature
    {
        [SerializeReference]
        public List<DamageSubFeature> damageFeatures = new();
        
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
        
#if ODIN_INSPECTOR || TRI_INSPECTOR
        [Button]
#endif
        public void FillFeatures()
        {
#if UNITY_EDITOR
            damageFeatures.RemoveAll(x => x == null);
            var types = TypeCache.GetTypesDerivedFrom(typeof(DamageSubFeature));
            foreach (var type in  types)
            {
                if(type.IsInterface || type.IsAbstract)continue;
                if(damageFeatures.FirstOrDefault(x => x.GetType() == type) != null) continue;

                var instance = type.CreateWithDefaultConstructor();
                damageFeatures.Add((DamageSubFeature)instance);
            }
            this.MarkDirty();
#endif
        }
    }
}
