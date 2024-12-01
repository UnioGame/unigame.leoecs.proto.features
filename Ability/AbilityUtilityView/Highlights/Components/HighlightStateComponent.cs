namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Highlights.Components
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
    public struct HighlightStateComponent : IProtoAutoReset<HighlightStateComponent>
    {
        public Dictionary<ProtoPackedEntity, GameObject> Highlights;
        
        public void AutoReset(ref HighlightStateComponent c)
        {
            c.Highlights ??= new Dictionary<ProtoPackedEntity, GameObject>();
            c.Highlights.Clear();
        }
    }
}