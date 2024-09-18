namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Highlights.Systems
{
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;


    public sealed class ProcessHighlightWhenDeadSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<OwnerDestroyedEvent>()
                .Inc<HighlightStateComponent>()
                .End();
        }
        
        public void Run()
        {
            var highlightedStatePool = _world.GetPool<HighlightStateComponent>();
            var hideHighlightPool = _world.GetPool<HideHighlightRequest>();

            foreach (var entity in _filter)
            {
                ref var highlightedState = ref highlightedStatePool.Get(entity);
                foreach (var packedEntity in highlightedState.Highlights)
                {
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref hideHighlightPool.Add(hideRequestEntity);
                    
                    hideRequest.Source = entity.PackEntity(_world);
                    hideRequest.Destination = packedEntity.Key;
                }
            }
        }
    }
}