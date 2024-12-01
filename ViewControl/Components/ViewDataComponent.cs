namespace UniGame.Ecs.Proto.ViewControl.Components
{
    using System;
    using System.Collections.Generic;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ViewDataComponent : IProtoAutoReset<ViewDataComponent>
    {
        public Dictionary<GameObject, ProtoPackedEntity> Views;

        public void AutoReset(ref ViewDataComponent c)
        {
            c.Views ??= new Dictionary<GameObject, ProtoPackedEntity>();
            c.Views.Clear();
        }
    }
}