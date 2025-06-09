namespace UniGame.Ecs.Proto.Effects.Converters
{
    using System;
    using Components;
    using Game.Code.Configuration.Runtime.Effects;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
#endif
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class EffectViewInstanceRootConverter : GameObjectConverter
    {
#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [InlineButton(nameof(FillOptions))]
#endif
#endif
        public ViewInstanceTypeSlot[] viewInstances;
        
        protected override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var viewInstanceComponent = ref world.AddComponent<ViewInstanceTypeComponent>(entity);
            viewInstanceComponent.value = new Transform[viewInstances.Length];
            foreach (var viewInstance in viewInstances)
            {
                viewInstanceComponent.value[(int)viewInstance.type] = viewInstance.transform;
            }
        }
        
#if UNITY_EDITOR
        private void FillOptions()
        {
            var options = Enum.GetValues(typeof(ViewInstanceType));
            viewInstances = new ViewInstanceTypeSlot[options.Length];
            foreach (var option in options)
            {
                var viewInstanceType = (ViewInstanceType)option;
                var slot = new ViewInstanceTypeSlot
                {
                    type = viewInstanceType
                };
                
                viewInstances[(int)viewInstanceType] = slot;
            }
        }
#endif
    }

    [Serializable]
    public class ViewInstanceTypeSlot
    {
#if ODIN_INSPECTOR
        [ReadOnly]
#endif
        public ViewInstanceType type;
        public Transform transform;
    }
}