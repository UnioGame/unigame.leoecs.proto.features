namespace UniGame.Ecs.Proto.Camera.Converters
{
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    /// <summary>
    /// Конвертер игровой камеры на управляемом персонаже.
    /// </summary>
    public sealed class PlayerCameraConverter : MonoLeoEcsConverter
    {
        private const string MainCameraTag = "MainCamera";

        #region inspector

        public Camera targetCamera;
        
        [SerializeField] 
        private float _speed = 1.0f;
        [SerializeField]
        private Vector3 _offset;

#if ODIN_INSPECTOR
        [BoxGroup("runtime")]
        [ShowIf(nameof(IsPlaying))]
        [OnValueChanged(nameof(OnComponentChanged))]
#endif
        [SerializeField]
        private CameraLookTargetComponent _cameraComponent;
        
        #endregion

        private ProtoWorld _world;
        private ProtoIt _filter;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            _world = world;
            
            var cameraPool = world.GetPool<CameraComponent>();
            ref var cameraComponent = ref cameraPool.Add(entity);
            targetCamera ??= target.GetComponent<Camera>();
                
            if (targetCamera == null)
            {
                Debug.LogError($"Entity must contains {nameof(Camera)} component!");
                return;
            }

            cameraComponent.Camera = targetCamera;
            cameraComponent.IsMain = targetCamera.tag.Equals(MainCameraTag);

            var cameraLookTargetPool = world.GetPool<CameraLookTargetComponent>();
            ref var lookTargetComponent = ref cameraLookTargetPool.Add(entity);
            
            lookTargetComponent.Speed = _speed;
            lookTargetComponent.Offset = _offset;

            _cameraComponent = lookTargetComponent;
        }

        private void OnComponentChanged(CameraLookTargetComponent cameraComponent)
        {
            if (_world == null) return;

            _filter ??= _world.Filter<CameraLookTargetComponent>().Inc<CameraComponent>().End();
            foreach (var entity in _filter)
            {
                ref var lookTarget = ref _world.GetComponent<CameraLookTargetComponent>(entity);
                lookTarget.Offset = cameraComponent.Offset;
                lookTarget.Speed = cameraComponent.Speed;
            }
        }
    }
}