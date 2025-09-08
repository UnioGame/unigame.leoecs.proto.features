namespace Characteristics.Stun
{
    using System;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using UniGame.Ecs.Proto.Characteristics.Feature;
    using UniGame.Ecs.Proto.Characteristics.Stun.Components;
    using UniGame.Ecs.Proto.Characteristics.Stun.Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    
    public class StunEcsFeature : CharacteristicEcsFeature<StunComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessStunSystem());
            return UniTask.CompletedTask;
        }
    }
    
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Stun Feature", fileName = "Stun Feature")]
    public sealed class StunFeature : CharacteristicFeature<StunEcsFeature, StunComponent>
    {
    }
}