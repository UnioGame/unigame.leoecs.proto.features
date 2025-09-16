namespace UniGame.Ecs.Proto.Gameplay.Damage
{
    using System;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime;

    [Serializable]
    public abstract class DamageSubFeature : EcsFeature
    {
        public virtual UniTask BeforeDamageSystem(IProtoSystems ecsSystems)
        {
            return UniTask.CompletedTask;
        }
        
        public virtual UniTask AfterDamageSystem(IProtoSystems ecsSystems)
        {
            return UniTask.CompletedTask;
        }
        
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            return UniTask.CompletedTask;
        }
        
    }
}