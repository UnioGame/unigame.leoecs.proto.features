namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Component
{
    using System.Collections.Generic;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;

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