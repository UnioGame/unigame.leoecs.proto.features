namespace Game.Ecs.ButtonAction.SubFeatures.MainAction.Converters
{
    using System;
    using Components;
    using Data;
    using Game.Ecs.ButtonAction.Data;
    using Leopotam.EcsProto;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.Serialization;

    /// <summary>
    /// Converter main menu action to entity.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class MainActionConverter : GameObjectConverter
    {
        [FormerlySerializedAs("mainActionId")]
        [SerializeField]
        private GameActionId gameActionId;
        [SerializeField, HideLabel]
        private ButtonStartValue buttonStartValue;
        
        protected override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var buttonActionComponent = ref world.AddComponent<ButtonActionComponent<GameActionId>>(entity);
            buttonActionComponent.Id = gameActionId;
            
            if (buttonStartValue.isSelected)
                world.AddComponent<SelectedButtonComponent>(entity);

            if (buttonStartValue.interactable)
                world.AddComponent<InteractableButtonComponent>(entity);
        }
    }
}