namespace UniGame.Ecs.Proto.Ability.SubFeatures.Projectiles.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class ProjectileAbilityAspect : EcsAspect
    {
        public ProtoPool<ProjectileAbilityComponent> ProjectileAbility;
        public ProtoPool<ProjectileComponent> Projectile;
        
        // To future trajectory module
        public ProtoPool<LinearMovementComponent> LinearMovement;
    }
}