namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.AnalyticsAction.Systems
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
    using Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SendAnalyticsActionSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private Dictionary<string, string> _emptyDictionary = new(0);
        private AnalyticsActionAspect _aspect;
        private ITutorialAnalytics _analyticsService;
        private int _stepCounter;

        private ProtoItExc _filter = It
            .Chain<AnalyticsActionComponent>()
            .Exc<CompletedAnalyticsActionComponent>()
            .End();

        public SendAnalyticsActionSystem(ITutorialAnalytics analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public void Run()
        {
            foreach (var entity in _filter)
            {
                _stepCounter++;

                ref var request = ref _aspect.AnalyticsAction.Get(entity);
                _aspect.CompletedAnalyticsAction.Add(entity);

                _analyticsService?.Send(new TutorialMessage()
                {
                    id = _stepCounter.ToString(),
                    message = request.stepName,
                    data = _emptyDictionary,
                });
            }
        }
    }
}