namespace UniGame.Ecs.Proto.Ability.SubFeatures.Visuals
{
    using System;
    using Cysharp.Threading.Tasks;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Systems;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AbilityVisualsFeature : AbilitySubFeature
    {
        public override UniTask<IProtoSystems> OnActivateSystems(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new CreateAbilityVisualsSystem());
            ecsSystems.Add(new DelayVisualsSpawnSystem());

            return base.OnActivateSystems(ecsSystems);
        }
    }
}