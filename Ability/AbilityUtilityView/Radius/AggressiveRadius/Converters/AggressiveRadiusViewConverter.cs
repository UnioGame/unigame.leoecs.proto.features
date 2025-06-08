namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.AggressiveRadius.Converters
{
    using System;
    using Components;
    using Core.Runtime;
    using Cysharp.Threading.Tasks;
    using Game.Code.GameLayers.Category;
    using Game.Code.GameLayers.Layer;
    using Leopotam.EcsProto;
    using UniGame.AddressableTools.Runtime;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public sealed class AggressiveRadiusViewConverter : LeoEcsConverter
    {
        public AssetReferenceGameObject noTargetView;
        public AssetReferenceGameObject targetCloseView;
        public AssetReferenceGameObject hasTargetView;
        
        [CategoryIdMask]
        [SerializeField]
        public CategoryId categoryId;
        
        [LayerIdMask]
        [SerializeField]
        public LayerId layerMask;
        
        private ProtoWorld _world;
        private ProtoEntity _entity;
        private GameObject _noTargetRadiusView;
        private GameObject _targetCloseRadiusView;
        private GameObject _hasTargetRadiusView;
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
#if !UNITY_EDITOR
            return;
#endif
            _world = world;
            _entity = entity;
            
            var lifeTime = target.GetAssetLifeTime();
            LoadViewAsync(lifeTime).Forget();
        }
        
        private async UniTask LoadViewAsync(ILifeTime lifeTime)
        {
            _noTargetRadiusView  = await noTargetView.LoadAssetInstanceTaskAsync(lifeTime,true);
            _targetCloseRadiusView = await targetCloseView.LoadAssetInstanceTaskAsync(lifeTime,true);
            _hasTargetRadiusView = await hasTargetView.LoadAssetInstanceTaskAsync(lifeTime,true);
            OnApply();
        }

        private void OnApply()
        {
            var radiusViewPool = _world.GetPool<AggressiveRadiusViewDataComponent>();
            ref var radiusView = ref radiusViewPool.Add(_entity);
            
            radiusView.NoTargetRadiusView = _noTargetRadiusView;
            radiusView.TargetCloseRadiusView = _targetCloseRadiusView;
            radiusView.HasTargetRadiusView = _hasTargetRadiusView;

            radiusView.CategoryId = categoryId;
            radiusView.LayerMask = layerMask;

            var viewStatePool = _world.GetPool<AggressiveRadiusViewStateComponent>();
            viewStatePool.Add(_entity);
        }
    }
}