namespace UniGame.Ecs.Proto.AI.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AiAspect : EcsAspect
    {
        public ProtoPool<AiAgentComponent> AiAgent;
        public ProtoPool<AiAgentPlanningComponent> AiAgentPlanning;
        public ProtoPool<AiAgentSelfControlComponent> AiAgentSelfControl;
        public ProtoPool<AiDefaultActionComponent> AiDefaultAction;
        public ProtoPool<AiSensorComponent> AiSensor;
    }
}