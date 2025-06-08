namespace Game.Ecs.GameActions.Converters
{
    using System;
    using ButtonAction.Components;
    using ButtonAction.Components.Requests;
    using Data;
    using Game.Ecs.ButtonAction.Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using R3;
    using Sirenix.OdinInspector;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
     
    using UnityEngine;
    using UnityEngine.Scripting.APIUpdating;
    using UnityEngine.UI;

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
    [MovedFrom(true,
        sourceClassName:"MainActionConverter",
        sourceAssembly:"game.ecs.buttonAction")]
    public class GameActionButtonConverter : GameObjectConverter,IConverterEntityDestroyHandler
    {
        public GameActionId gameActionId;
        
        [InlineProperty]
        [HideLabel]
        public ButtonStartValue buttonStartValue;
        
        public Button button;

        private IDisposable _buttonDisposable;
        
        protected override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            CleanUp();
            
            ref var buttonActionComponent = ref world
                .AddComponent<ButtonGameActionComponent>(entity);
            
            buttonActionComponent.Id = gameActionId;
            buttonActionComponent.Button = button;
            
            if (buttonStartValue.isSelected)
                world.AddComponent<SelectedButtonComponent>(entity);

            if (buttonStartValue.interactable)
                world.AddComponent<InteractableButtonComponent>(entity);

            if (button == null) return;

            var packedEntity = world.PackEntity(entity);
            
            _buttonDisposable = button
                .OnClickAsObservable()
                .Subscribe(x => OnClick(packedEntity,world))
                .AddTo(target.GetAssetLifeTime());
        }
        
        private void OnClick(ProtoPackedEntity entity,ProtoWorld world)
        {
            if(!entity.Unpack(world,out var protoEntity))return;
            
            ref var request = ref world
                .GetOrAddComponent<ButtonGameActionSelfRequest>(protoEntity);
        }

        public void OnEntityDestroy(ProtoWorld world, ProtoEntity entity)
        {
            CleanUp();
        }

        private void CleanUp()
        {
            _buttonDisposable?.Dispose();
            _buttonDisposable = null;
        }
    }
}