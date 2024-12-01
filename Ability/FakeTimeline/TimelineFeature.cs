namespace Game.Modules.leoecs.proto.features.Ability.FakeTimeline
{
    using System;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Components.Requests;
    using UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [CreateAssetMenu(menuName = "ECS Proto/Features/Ability/Timeline Feature", 
        fileName = "Timeline Feature")]
    public class TimelineFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            base.InitializeAsync(ecsSystems);
            
            ecsSystems.Add(new SortTimelinePlayablesSystem());

            ecsSystems.DelHere<ExecuteTimelinePlayableRequest>();
            ecsSystems.Add(new TimelineSystem());
            
            return UniTask.CompletedTask;
        }
    }
}