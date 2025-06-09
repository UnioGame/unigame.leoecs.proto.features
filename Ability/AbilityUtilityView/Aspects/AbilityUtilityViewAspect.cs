namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Aspects
{
    using System;
    using Area.Components;
    using Components;
    using Highlights.Components;
    using Leopotam.EcsProto;
    using Radius.AggressiveRadius.Components;
    using Radius.Component;
    using LeoEcs.Bootstrap;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AbilityUtilityViewAspect : EcsAspect
    {
        public ProtoPool<AreaInstanceComponent> AreaInstance;
        public ProtoPool<VisibleUtilityViewComponent> VisibleUtilityView;
        public ProtoPool<HighlightComponent> Highlight;
        public ProtoPool<HighlightStateComponent> HighlightState;
        public ProtoPool<AggressiveRadiusViewDataComponent> AggressiveRadiusViewData;
        public ProtoPool<AggressiveRadiusViewStateComponent> AggressiveRadiusViewStat;
        public ProtoPool<HideRadiusRequest> HideRadius;
        public ProtoPool<RadiusViewComponent> RadiusView;
        public ProtoPool<RadiusViewDataComponent> RadiusViewData;
        public ProtoPool<RadiusViewStateComponent> RadiusViewState;
        public ProtoPool<ShowRadiusRequest> ShowRadius;
        
        public ProtoPool<HideHighlightRequest> HideHighlight;
        public ProtoPool<ShowHighlightRequest> ShowHighlight;
    }
}