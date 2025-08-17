namespace Game.Ecs.State.Aspects
{
    using System;
    using Data;
    using UniGame.LeoEcs.Bootstrap;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class StatesAspectT<TStateComponent> : EcsAspect
        where TStateComponent : struct, IStateComponent
    {
        public static int StateId = typeof(TStateComponent).GetHashCode();
    }
}