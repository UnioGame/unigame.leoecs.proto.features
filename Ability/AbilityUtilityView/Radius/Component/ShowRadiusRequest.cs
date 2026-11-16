namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Component
{
    using System;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;

    /// <summary>
    ///
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ShowRadiusRequest
    {
        public ProtoPackedEntity Source;
        public ProtoPackedEntity Destination;

        public Transform Root;
        public GameObject Radius;
        
        public Vector3 Size;
    }
}