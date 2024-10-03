namespace UniGame.Ecs.Proto.Ability.SubFeatures.Cooldown.Systems
{
    using System;
    using Ability.Aspects;
    using Common.Components;
    using LeoEcs.Timer.Components.Events;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AbilityCompleteCooldownSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;

        private AbilityAspect _abilityAspect;

        private ProtoItExc _abilityFilter = It
            .Chain<AbilityIdComponent>()
            .Inc<CooldownFinishedSelfEvent>()
            .Exc<ActiveAbilityComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var abilityEntity in _abilityFilter)
            {
                _abilityAspect.Active.Add(abilityEntity);
                GameLog.Log("Ability cooldown complete.");
            }
        }
    }
}