namespace UniGame.Ecs.Proto.Gameplay.LevelProgress.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using Runtime.DataFlow;
    using Core.Runtime;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class GameViewContainerConverter : LeoEcsConverter
    {
        #region inspector
        
#if ODIN_INSPECTOR
        [TitleGroup("settings")]
#endif
        public Transform parent;
#if ODIN_INSPECTOR
        [TitleGroup("settings")]
#endif
        public Vector3 position;
#if ODIN_INSPECTOR
        [TitleGroup("settings")]
#endif
        public Vector3 rotation = Quaternion.identity.eulerAngles;
#if ODIN_INSPECTOR
        [TitleGroup("settings")]
#endif
        public Vector3 scale = Vector3.one;
#if ODIN_INSPECTOR
        [TitleGroup("settings")]
#endif
        public List<AssetReferenceGameObject> views = new List<AssetReferenceGameObject>();

#if ODIN_INSPECTOR
        [TitleGroup("default view")]
#endif
        public bool activateOnConvert = false;
        
#if ODIN_INSPECTOR
        [TitleGroup("default view")]
        [ShowIf(nameof(activateOnConvert))]
#endif
        public bool enableDefaultView = false;
        
#if ODIN_INSPECTOR
        [TitleGroup("default view")]
        [ShowIf(nameof(enableDefaultView))]
        [ShowIf(nameof(activateOnConvert))]
#endif
        public GameObject view;
        
#if ODIN_INSPECTOR
        [ValueDropdown(nameof(GetViews))]
#endif
        [Space]
        public AssetReferenceGameObject selection;

        #endregion
        
        private LifeTime _lifeTime;
        private ProtoWorld _world;
        private ProtoEntity _entity;

        public bool IsActive => Application.isPlaying && _world != null && _world.IsAlive();

        public sealed override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            _world = world;
            _entity = entity;
            
            _lifeTime ??= new LifeTime()
                .AddTo(target.GetAssetLifeTime());
            _lifeTime.Restart();

            ref var viewParentComponent = ref world.GetOrAddComponent<GameViewParentComponent>(entity);
            viewParentComponent.Parent = parent == null ? target.transform : parent;
            viewParentComponent.Position = position;
            viewParentComponent.Rotation = Quaternion.Euler(rotation);
            viewParentComponent.Scale = scale;

            if (!activateOnConvert) return;
            
            if (enableDefaultView && view != null)
            {
                //create default view by reference
                var viewEntity = world.NewEntity();
                ref var parentComponent = ref world.GetOrAddComponent<ParentEntityComponent>(viewEntity);
                ref var requestComponent = ref world.GetOrAddComponent<ActiveGameViewComponent>(entity);

                var viewPackedEntity = viewEntity.PackEntity(world);
                requestComponent.Value = viewPackedEntity;
                parentComponent.Value = entity.PackEntity(world);
                
                view.ConvertGameObjectToEntity(world, viewEntity);
                view.SetActive(true);
                return;
            }
            
            Activate();
        }

#if ODIN_INSPECTOR
        [EnableIf(nameof(IsActive))]
        [Button]
#endif
        public void Activate()
        {
            if (selection == null || selection.RuntimeKeyIsValid() == false) return;

            var requestEntity = _world.NewEntity();
            ref var requestComponent = ref _world.GetOrAddComponent<ActivateGameViewRequest>(requestEntity);
            requestComponent.Source = _entity.PackEntity(_world);
            requestComponent.View = selection.AssetGUID;
        }
    
        private IEnumerable<ValueDropdownItem<AssetReferenceGameObject>> GetViews()
        {
#if UNITY_EDITOR
            if (view == null) selection = views.FirstOrDefault();
            
            foreach (var item in views)
            {
                yield return new ValueDropdownItem<AssetReferenceGameObject>()
                {
                    Text = item.editorAsset.name,
                    Value = item,
                };
            }
#endif
            yield break;
        }
        
    }
}