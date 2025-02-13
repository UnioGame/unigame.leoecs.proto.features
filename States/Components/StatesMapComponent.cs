namespace Game.Ecs.State.Components
{
    using System;
    using Leopotam.EcsLite;
    using Unity.Collections;

    /// <summary>
    /// possible states of the entity
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct StatesMapComponent : IEcsAutoReset<StatesMapComponent>
    {
        public NativeHashSet<int> States;
        
        public void AutoReset(ref StatesMapComponent c)
        {
            if (c.States.IsCreated)
                c.States.Dispose();
        }
    }
}