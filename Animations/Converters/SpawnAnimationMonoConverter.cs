namespace UniGame.Ecs.Proto.Animations.Converters
{
    using System;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Runtime.Timeline;

    using UniGame.AddressableTools.Runtime;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Core.Runtime;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    public sealed class SpawnAnimationMonoConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        public PlayableAsset spawnAnimation;

        [SerializeField]
        public PlayableDirector playableDirector;
        
        [SerializeField]
        public bool disableParts = true;
        
#if ODIN_INSPECTOR
        [Required]
#endif
        [SerializeField]
        public GameObject[] disabledParts;
     
        [SerializeField]
        public bool playOnConvert = true;
        
        private void Awake()
        {
            if(!disableParts || IsEnabled == false) return;
            
            foreach (var disabledPart in disabledParts)
            {
#if UNITY_EDITOR
                if (disabledPart == null)
                {
                    Debug.LogError($"PREFAB: {name} DISABLED PART IN NULL");
                    continue;
                }
#endif
                disabledPart.SetActive(false);
            }
        }

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            if (!playOnConvert) return;
 
            var animationCreateRequest = world.NewEntity();
            ref var createAnimationRequest = ref world.AddComponent<CreateAnimationPlayableSelfRequest>(animationCreateRequest);
            ref var playAnimationSelfRequest = ref world.AddComponent<PlayAnimationSelfRequest>(animationCreateRequest);
            
            createAnimationRequest.Animation = spawnAnimation;
            createAnimationRequest.Owner = world.PackEntity(entity);
            createAnimationRequest.Target = world.PackEntity(entity);
            createAnimationRequest.WrapMode = DirectorWrapMode.None;
            createAnimationRequest.Duration = 0;
            createAnimationRequest.BindingData = null;

            ActivateAfterSpawn((float)spawnAnimation.duration).Forget();
        }
        
        private async UniTask ActivateAfterSpawn(float delay)
        {
            return;
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            foreach (var disabledPart in disabledParts)
            {
                if (disabledPart == null)
                    continue;
                disabledPart.SetActive(true);
            }
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Test()
        {
            // Get the PlayableGraph from the PlayableDirector
            playableDirector.playableAsset = spawnAnimation;
            playableDirector.RebuildGraph();
            
            var binding = playableDirector.GetGenericBinding(spawnAnimation);
            var graph = playableDirector.playableGraph;
            var animator = gameObject.GetComponent<Animator>();

            var track = spawnAnimation.GetTrack<AnimationTrack>();
            playableDirector.SetGenericBinding(track,animator);
        }
    }
    
    [Serializable]
    public sealed class SpawnAnimationConverter : LeoEcsConverter
    {
        [SerializeField]
        public AssetReferenceT<PlayableAsset> spawnAnimation;

#if ODIN_INSPECTOR
        [Required]
#endif
        [SerializeField]
        private GameObject[] disabledParts;
        
        [SerializeField]
        private bool playOnConvert = true;
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            foreach (var disabledPart in disabledParts)
            {
#if UNITY_EDITOR
                if (disabledPart == null)
                {
                    Debug.LogError($"PREFAB: {target.name} DISABLED PART IN NULL",target);
                    continue;
                }
#endif
                disabledPart.SetActive(false);
            }
            
            if (!playOnConvert) return;
            
            ActivateAnimationAsync(target, world, entity).Forget();
        }

        private async UniTask ActivateAnimationAsync(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            var lifeTime = target.GetAssetLifeTime();
            var animation = await spawnAnimation
                .LoadAssetTaskAsync(lifeTime);
            
            CreateAnimationEntity(animation,target,world,entity);

            await ActivateAfterSpawn((float)animation.duration)
                .AttachExternalCancellation(lifeTime.Token);
        }

        private void CreateAnimationEntity(PlayableAsset animation,GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            var animationCreateRequest = world.NewEntity();
            ref var createAnimationRequest = ref world.AddComponent<CreateAnimationPlayableSelfRequest>(animationCreateRequest);
            ref var playAnimationSelfRequest = ref world.AddComponent<PlayAnimationSelfRequest>(animationCreateRequest);
            
            createAnimationRequest.Animation = animation;
            createAnimationRequest.Owner = world.PackEntity(entity);
            createAnimationRequest.Target = world.PackEntity(entity);
            createAnimationRequest.WrapMode = DirectorWrapMode.None;
            createAnimationRequest.Duration = 0;
            createAnimationRequest.BindingData = null;
        }
        
        
        private async UniTask ActivateAfterSpawn(float delay)
        {
            return;
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            foreach (var disabledPart in disabledParts)
            {
                if (disabledPart == null)
                    continue;
                disabledPart.SetActive(true);
            }
        }
    }
}