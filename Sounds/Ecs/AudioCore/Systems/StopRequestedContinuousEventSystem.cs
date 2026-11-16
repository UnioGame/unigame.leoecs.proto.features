using FMOD.Studio;
using Game.Ecs.Audio.AudioCore.Aspects;
using Game.Ecs.Audio.AudioCore.Components;
using Game.Ecs.Audio.AudioCore.Components.Requests;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UniGame.LeoEcs.Shared.Extensions;

namespace Game.Ecs.Audio.AudioCore.Systems
{
    using System;
    using System.Linq;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
    [ECSDI]
    public class StopRequestedContinuousEventSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _requestedEventsFilter = It
            .Chain<StopFMODEventRequest>()
            .End();
        private EcsFilter _continuousAudioFilter = It
            .Chain<ContinuousAudioComponent>()
            .End();
        private AudioCoreAspect _audioCoreAspect;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

        }

        public void Run()
        {
            foreach (var entity in _requestedEventsFilter)
            {
                ref var request = ref _audioCoreAspect.StopFMODEventRequest.Get(entity);

                foreach (var continuousEntity in _continuousAudioFilter)
                {
                    ref var continuousAudio = ref _audioCoreAspect.ContinuousAudio.Get(continuousEntity);
                    
                    if (continuousAudio.Path == request.Path)
                    {
                        continuousAudio.Playback.Stop();
                    }
                }
            }
        }
    }
}