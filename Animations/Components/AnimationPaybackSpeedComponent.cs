namespace UniGame.Ecs.Proto.Animations.Components
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;


    /// <summary>
    /// play speed value
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AnimationPaybackSpeedComponent : IProtoAutoReset<AnimationPaybackSpeedComponent>
    {
        public float Value;
        
        public void SetHandlers(IProtoPool<AnimationPaybackSpeedComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref AnimationPaybackSpeedComponent c)
        {
            c.Value = 1f;
        }
    }
}