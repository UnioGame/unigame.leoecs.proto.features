namespace UniGame.Ecs.Proto.Characteristics.Stun.Aspects
{
    using System;
    using Base.Aspects;
    using Components;
    using Leopotam.EcsProto;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class StunAspect : GameCharacteristicAspect<StunComponent>
    {
        public ProtoPool<StunComponent> Stun;
        public ProtoPool<StunSourcesCounterComponent> SourceCounter;
    }
}