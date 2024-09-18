namespace UniGame.Ecs.Proto.GameResources.Systems
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Aspects;
    using Components;
    using Cysharp.Threading.Tasks;
    using Game.Code.DataBase.Runtime;
    using Game.Code.DataBase.Runtime.Abstract;
    using Game.Ecs.Time.Service;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;
    using Object = UnityEngine.Object;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ProceedGameResourceRequestSystem : IProtoRunSystem, IProtoDestroySystem
    {
        private readonly IGameDatabase _gameDatabase;
        private readonly ILifeTime _defaultLifeTime;
        private readonly CancellationTokenSource _tokenSource = new();
        
        private ProtoWorld _world;
        private GameResourceTaskAspect _taskAspect;
        private OwnershipAspect _ownershipAspect;
        
        private ProtoItExc _filter = It
            .Chain<GameResourceHandleComponent>()
            .Exc<GameResourceResultComponent>()
            .Exc<GameResourceTaskComponent>()
            .End();

        public ProceedGameResourceRequestSystem(IGameDatabase gameDatabase,ILifeTime defaultLifeTime)
        {
            _gameDatabase = gameDatabase;
            _defaultLifeTime = defaultLifeTime;
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _taskAspect.Handle.Get(entity);
                var resourceId = request.Resource;
                
                ref var taskComponent = ref _taskAspect.Task.Add(entity);
                taskComponent.Resource = request.Resource;
                taskComponent.LoadingStartTime = GameTime.Time;
                taskComponent.RequestOwner = request.Source;

                var lifeTime = request.LifeTime == default
                    ? _defaultLifeTime
                    : request.LifeTime;
                
                LoadGameResource(entity, resourceId, lifeTime).Forget();
            }
        }

        public void Destroy()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }
        
        private async UniTask LoadGameResource(ProtoEntity entity, string resourceId, ILifeTime lifeTime)
        {
            var result = await _gameDatabase.LoadAsync<Object>(resourceId, lifeTime);
            CreateResourceResult(entity, ref result, resourceId, _world, lifeTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CreateResourceResult(ProtoEntity entity, ref GameResourceResult result, string resourceId, 
            ProtoWorld world, ILifeTime lifeTime)
        {
            //is entity still alive?
            var packedEntity = world.PackEntity(entity);
            if (!packedEntity.Unpack(world, out var entityData)) return;

            //add loaded game resource data to entity
            if (result.Result is GameObject gameObject)
            {
                ref var gameObjectComponent = ref _taskAspect.GameObject.Add(entity);
                gameObjectComponent.Value = gameObject;
            }
            
            ref var component = ref _taskAspect.Result.Add(entity);
            component.Resource = result.Result;
            component.ResourceId = resourceId;
            component.LifeTime = lifeTime;
            
#if UNITY_EDITOR
            if(result.Result == null)
                Debug.LogError($"ECS: {nameof(ProceedGameResourceRequestSystem)} not found spawn resource with ID : {resourceId}");
#endif
        }
    }
}
