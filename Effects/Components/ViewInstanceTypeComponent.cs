namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using Game.Code.Configuration.Runtime.Effects;
    using Leopotam.EcsLite;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ViewInstanceTypeComponent : IEcsAutoReset<ViewInstanceTypeComponent>
    {
        public Transform[] value;
        
        public void AutoReset(ref ViewInstanceTypeComponent c)
        {
            if (c.value == null)
            {
                var length = Enum.GetValues(typeof(ViewInstanceType)).Length;
                c.value = new Transform[length];
            }
        }
    }
}