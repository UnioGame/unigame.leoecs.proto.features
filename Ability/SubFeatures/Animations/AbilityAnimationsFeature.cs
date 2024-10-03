namespace UniGame.Ecs.Proto.Ability.SubFeatures.Animations
{
    using System;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Systems;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AbilityAnimationsFeature : AbilitySubFeature
    {
        public override UniTask<IProtoSystems> OnActivateSystems(IProtoSystems ecsSystems)
        {
            ecsSystems.AddSystem(new AbilityAnimationSystem());
            
            return UniTask.FromResult(ecsSystems);
        }
    }
}