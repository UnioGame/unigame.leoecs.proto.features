namespace UniGame.Ecs.Proto.Effects.Converters
{
    using System;
    using Components;
    using Data;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    /// <summary>
    /// mark gameobject as effect root target
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class EffectRootTargetConverter : GameObjectConverter
    {
        public EffectRootId effectRootId;
        public bool useRootTransform = true;
        
#if ODIN_INSPECTOR
        [HideIf(nameof(useRootTransform))]
#endif
        public Transform rootValue;
        
        protected override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var effectRootIdComponent = ref world.AddComponent<EffectRootIdComponent>(entity);
            ref var targetComponent = ref world.AddComponent<EffectRootTargetComponent>(entity);
            ref var parentComponent = ref world.AddComponent<EffectParentComponent>(entity);
            
            effectRootIdComponent.Value = effectRootId;
            parentComponent.Value = useRootTransform 
                ? target.transform 
                : rootValue;
        }
    }
}