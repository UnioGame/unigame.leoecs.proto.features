namespace UniGame.Ecs.Proto.Ability.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AbilityInventory.Components;
    using Common.Components;
    using Components;
    using Game.Code.Configuration.Runtime.Ability;
    using Game.Code.Services.AbilityLoadout.Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Collections;
    using UnityEngine;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif

    [Serializable]
    public class AbilityConverter : LeoEcsConverter, ILeoEcsGizmosDrawer
    {
        #region inspector

        [SerializeField] 
        [OnValueChanged(nameof(UpdateAbilityConfiguration))]
        [OnValueChanged(nameof(ResetDefault))]
        public List<AbilityCell> abilityCells = new List<AbilityCell>();

        public bool userInput;

        public bool equipAbilityOnConvert = true;
        
        public bool drawGizmos = false;

        #endregion

        private AbilityConfiguration _defaultAbility;
        
#if UNITY_EDITOR
        [NonSerialized]
        private List<AbilityConfiguration> _abilityConfigurations = new List<AbilityConfiguration>();
#endif
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var mapComponent = ref world.GetOrAddComponent<AbilityMapComponent>(entity);
            ref var inHandLink = ref world.GetOrAddComponent<AbilityInHandLinkComponent>(entity);
            ref var defaultSlotComponent = ref world.GetOrAddComponent<DefaultAbilityTargetSlotComponent>(entity);

            if (userInput)
            {
                ref var inputComponent = ref world.GetOrAddComponent<AbilityInputComponent>(entity);
                inputComponent.IsUserInput = userInput;
            }

            if (equipAbilityOnConvert == false) return;
            
            ref var abilitySlots = ref mapComponent.AbilitySlots;
            ref var abilities = ref mapComponent.Abilities;
            abilitySlots = new NativeHashMap<int, ProtoPackedEntity>(10, Allocator.Persistent);
            abilities = new NativeHashSet<ProtoPackedEntity>(10, Allocator.Persistent);

            var packedEntity = entity.PackEntity(world);

            var defaultCell = abilityCells.FirstOrDefault(x => x.IsDefault);
            var defaultCellIType = defaultCell.SlotId;

            //EquipAbilityByIdRequest
            foreach (var cell in abilityCells)
            {
                var abilityRequestEntity = world.NewEntity();
                ref var abilityRequest = ref world.AddComponent<EquipAbilityIdSelfRequest>(abilityRequestEntity);

                abilityRequest.AbilityId = cell.AbilityId;
                abilityRequest.AbilitySlot = cell.SlotId;
                abilityRequest.Owner = packedEntity;
                abilityRequest.IsDefault = cell.IsDefault || cell.SlotId == defaultCellIType;
                
                if (cell.IsDefault)
                    defaultSlotComponent.Value = cell.SlotId;
            }
        }

        public AbilityConfiguration GetDefaultAbility()
        {
#if UNITY_EDITOR
            if (_defaultAbility != null) return _defaultAbility;
            
            var defaultAbility = abilityCells.FirstOrDefault(x => x.IsDefault);
            var data = AssetEditorTools.GetAssets<AbilityItemAsset>();
            var abilityRecord = data.FirstOrDefault(x => defaultAbility.AbilityId == x.Id);
            if (abilityRecord == null) return null;

            var configuration = abilityRecord.data.configurationReference.editorAsset;
            _defaultAbility = configuration == null
                ? null
                : configuration;
#endif
            return _defaultAbility;
        }

        [ButtonGroup("UpdateGizmos")]
        public void UpdateAbilityConfiguration()
        {
#if UNITY_EDITOR
            _abilityConfigurations.Clear();
            foreach (var abilityCell in abilityCells)
            {
                var data = AssetEditorTools.GetAssets<AbilityItemAsset>();
                var abilityRecord = data.FirstOrDefault(x => abilityCell.AbilityId == x.Id);
                if (abilityRecord == null) return;

                var configuration = abilityRecord.data.configurationReference.editorAsset;
                _abilityConfigurations.Add(configuration);
            }
#endif
        }
        

        public void DrawGizmos(GameObject target)
        {
#if UNITY_EDITOR
            if (!drawGizmos)
                return;
            if (_abilityConfigurations.Count == 0) 
                UpdateAbilityConfiguration();
            
            foreach (var abilityConfiguration in _abilityConfigurations)
            {
                foreach (var abilityAbilityBehaviour in abilityConfiguration.abilityBehaviours)
                {
                    if (abilityAbilityBehaviour is ILeoEcsGizmosDrawer drawer)
                    {
                        drawer.DrawGizmos(target);
                    }
                }
            }
#endif
        }

        private void ResetDefault()
        {
            
        }
    }
}