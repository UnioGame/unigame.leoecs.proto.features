namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Component
{
    using System;
    using System.Collections.Generic;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
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
    public struct RadiusViewStateComponent : IProtoAutoReset<RadiusViewStateComponent>
    {
        public Dictionary<ProtoPackedEntity, GameObject> RadiusViews;
        
        public void SetHandlers(IProtoPool<RadiusViewStateComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref RadiusViewStateComponent c)
        {
            c.RadiusViews ??= new Dictionary<ProtoPackedEntity, GameObject>();
            c.RadiusViews.Clear();
        }
    }
}