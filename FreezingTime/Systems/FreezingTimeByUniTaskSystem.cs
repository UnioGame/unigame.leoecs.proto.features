namespace UniGame.Ecs.Proto.Gameplay.FreezingTime.Systems
{
    using System;
    using System.Threading;
    using UnityEngine;
    using Cysharp.Threading.Tasks;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniCore.Runtime.ProfilerTools;
    using Components;
    using Aspects;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    [ECSDI]
    public sealed class FreezingTimeByUniTaskSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private FreezingTimeAspect _aspect;
        private CancellationTokenSource _tokenSource;

        private ProtoIt _filter = It
            .Chain<FreezingTimeRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _aspect.freezingTimeRequest.Get(entity);
                var newScale = Mathf.Clamp(request.TimeScale, 0f, 1f);
                var duration = request.Duration;
                
                StopTask();

                _tokenSource = new CancellationTokenSource();
                AnimateTimeScaleAsync(Time.timeScale, newScale, duration, _tokenSource.Token).Forget();
                break;
            }
        }

        private async UniTaskVoid AnimateTimeScaleAsync(float from, float to, float duration, CancellationToken token)
        {
            try
            {
                var elapsed = 0f;

                while (elapsed < duration && !token.IsCancellationRequested)
                {
                    elapsed += Time.unscaledDeltaTime;
                    var t = Mathf.Clamp01(elapsed / duration);
                    Time.timeScale = Mathf.Lerp(from, to, t);
                    await UniTask.Yield(PlayerLoopTiming.Update);
                }
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation silently
            }
            catch (Exception ex)
            {
                GameLog.LogException(ex);
            }

            Time.timeScale = to;
            FinishProcess();
        }

        public void FinishProcess()
        {
            var completedEventEntity = _world.NewEntity();
            ref var completed = ref _aspect.freezingTimeCompletedEvent.Add(completedEventEntity);
            completed.TimeScale = Time.timeScale;
        }

        private void StopTask()
        {
            if (_tokenSource == null) return;

            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        }

        public void Destroy()
        {
            StopTask();
        }
    }
}