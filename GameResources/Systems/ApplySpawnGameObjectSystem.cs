namespace UniGame.Ecs.Proto.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using UnityEngine;
    using Component = UnityEngine.Component;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ApplySpawnGameObjectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private GameResourceAspect _resourceAspect;
        
        private ProtoItExc _filter = It
            .Chain<GameSpawnedResourceComponent>()
            .Inc<UnityObjectComponent>()
            .Inc<GameObjectComponent>()
            .Exc<GameSpawnCompleteComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var objectComponent = ref _resourceAspect.Object.Get(entity);

                var asset = objectComponent.Value;
                var gameObject = asset is Component component
                    ? component.gameObject
                    : asset as GameObject;
                
                if(!gameObject) continue;

                var isSelfTarget = _resourceAspect.Target.Has(entity);
                var converter = gameObject.GetComponent<ILeoEcsMonoConverter>();
                if (!isSelfTarget && converter is { AutoCreate: true })
                {
                    gameObject.SetActive(true); 
                    continue;
                }

                gameObject.ConvertGameObjectToEntity(_world, entity);
                gameObject.SetActive(true);
            }
        }
    }
}