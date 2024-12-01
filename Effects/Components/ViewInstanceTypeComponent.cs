namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using Game.Code.Configuration.Runtime.Effects;
    using Leopotam.EcsProto;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ViewInstanceTypeComponent : IProtoAutoReset<ViewInstanceTypeComponent>
    {
        public Transform[] value;
        
        public void AutoReset(ref ViewInstanceTypeComponent c)
        {
            if (c.value != null) return;
            var length = Enum.GetValues(typeof(ViewInstanceType)).Length;
            c.value = new Transform[length];
        }
    }
}