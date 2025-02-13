namespace Game.Ecs.State.Components
{
    using System;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using Unity.Collections;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct StateHistoryComponent : IEcsAutoReset<StateHistoryComponent>
    {
        public NativeList<int> States;
        
        public void AutoReset(ref StateHistoryComponent c)
        {
            if (c.States.IsCreated)
                c.States.Dispose();
        }
    }
}