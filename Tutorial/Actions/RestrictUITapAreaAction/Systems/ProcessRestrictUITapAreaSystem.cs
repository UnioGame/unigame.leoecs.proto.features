namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Systems
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine.EventSystems;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ProcessRestrictUITapAreaSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private RestrictUITapAreaActionAspect _aspect;

        private List<RaycastResult> _raycastResults = new();
        private EventSystem _eventSystem = EventSystem.current;

        private ProtoIt _filter = It
            .Chain<ActivateRestrictUITapAreaComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                if (!Input.GetMouseButtonDown(0)) continue;
                
                ref var restrictUITapArea = ref _aspect.RestrictUITapArea.Get(entity);
                var rect = restrictUITapArea.Value.Rect;
                var pass = restrictUITapArea.Value.Passages;
                var mouseScreenPosition = Input.mousePosition;
                var isPointInside = rect.Contains(mouseScreenPosition);
                
                if (!isPointInside) continue;

                _raycastResults.Clear();
                var pointerEventData = new PointerEventData(_eventSystem)
                {
                    position = Input.mousePosition
                };

                _eventSystem.RaycastAll(pointerEventData, _raycastResults);
                foreach (var result in _raycastResults)
                {
                    if (pass == 0) continue;
                    if (!result.gameObject) continue;
                    PassTapToUI(pointerEventData, result.gameObject);
                    pass--;
                }

                _aspect.CompletedRestrictUITapArea.Add(entity);
            }
        }

        private void PassTapToUI(BaseEventData eventData, GameObject targetGameObject)
        {
            ExecuteEvents.Execute(targetGameObject, eventData, ExecuteEvents.pointerClickHandler);
        }
    }
}