namespace Game.Ecs.State.Components
{
    using System;
    using System.Collections.Generic;
    using Data;
    using Leopotam.EcsLite;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct StateBehavioursMapComponent : IEcsAutoReset<StateBehavioursMapComponent>
    {
        public Dictionary<int,IStateBehaviour> Behaviours;
        
        public void AutoReset(ref StateBehavioursMapComponent c)
        {
            c.Behaviours ??= new();
            c.Behaviours.Clear();
        }
    }
}