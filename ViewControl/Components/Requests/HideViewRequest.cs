namespace UniGame.Ecs.Proto.ViewControl.Components.Requests
{
    using System;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct HideViewRequest
    {
        public ProtoPackedEntity Destination;

        public GameObject View;
    }
}