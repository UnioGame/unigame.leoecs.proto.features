namespace UniGame.Ecs.Proto.Effects.Converters
{
    using System;
    using Components;
    using Data;
    using Game.Modules.UnioModules.UniGame.CoreModules.UniGame.Core.Runtime.Extension;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
#endif
    
    /// <summary>
    /// apply effect on convert
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class EffectRootConverter : GameObjectConverter
    {
        public EffectRootValue[] effectRoots = Array.Empty<EffectRootValue>();

        [Tooltip("bake effect roots on convert. if false - convert only in editor mode")]
        public bool bakeOnConvert = false;
        
#if ODIN_INSPECTOR
        [TitleGroup("Backed Effect Roots")]
#endif

        public Transform[] backedRoots = Array.Empty<Transform>();

#if ODIN_INSPECTOR
        [OnInspectorGUI]
        [InlineButton(nameof(Bake),icon:SdfIconType.Activity)]
        [TitleGroup("Backed Effect Roots")]
#endif
        [NonSerialized]
        private GameObject _target;
        
        protected override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var effectRootsComponent = ref world.AddComponent<EffectRootTransformsComponent>(entity);
            ref var effectRootComponent = ref world.AddComponent<EffectRootComponent>(entity);
            
            effectRootsComponent.Value = backedRoots;
            effectRootComponent.Value = effectRoots;
        }

        public void Bake(GameObject gameObject)
        {
#if UNITY_EDITOR
            backedRoots = Array.Empty<Transform>();
            
            var rootConfiguration = EffectRootId.EquipSlotsAsset;
            if (rootConfiguration == null) return;

            var rootsData = rootConfiguration.data;
            var roots = rootsData.roots;

            backedRoots = new Transform[roots.Length];

            for (var i = 0; i < roots.Length; i++)
            {
                var key = roots[i];
                if (!key.isChild) continue;
                var root = gameObject.FindChildGameObject(key.objectName);
                backedRoots[i] = root == null ? null : root.transform;
            }

            foreach (var key in effectRoots)
            {
                if(key.objectValue == null && string.IsNullOrEmpty(key.objectName))
                    continue;
                
                var root = key.objectValue == null 
                    ? gameObject.FindChildGameObject(key.objectName) 
                    : key.objectValue;
                
                backedRoots[key.id] = root == null 
                    ? backedRoots[key.id] 
                    : root.transform;
            }   
#endif
        }
    }
}