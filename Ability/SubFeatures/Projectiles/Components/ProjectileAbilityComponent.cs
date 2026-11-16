namespace UniGame.Ecs.Proto.Ability.SubFeatures.Projectiles.Components
{
    using System;
    using UnityEngine.Serialization;

    /// <summary>
    /// Шаг способности «снаряд»: какой префаб, откуда вылетает, куда летит и сколько летит.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ProjectileAbilityComponent
    {
        public int spawnPositionType;
        public int targetPositionType;
        
        public string assetGuid;
        public float duration;
        
        public int trajectoryType;
    }
}