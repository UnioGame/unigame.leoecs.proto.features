using System.Collections.Generic;
using UniGame.Ecs.Proto.GameEffects.ModificationEffect.Components;

using UniGame.LeoEcs.Shared.Extensions;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace UniGame.Ecs.Proto.Characteristics.Converters
{
    using System;
    using Characteristics;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;
    
    /// <summary>
    /// create ModificationsComponents and add it to entity
    /// </summary>
    [Serializable]
    public class ModificationsConverter : EcsComponentConverter
    {
#if ODIN_INSPECTOR
        [InlineProperty] 
#endif
        [SerializeReference]
        public List<ModificationHandler> modifications = new List<ModificationHandler>();

        public override void Apply(ProtoWorld world, ProtoEntity entity)
        {
            ref var modificationsComponent = ref world.GetOrAddComponent<ModificationEffectComponent>(entity);
            modificationsComponent.ModificationHandlers.AddRange(modifications);
        }

    }
}
