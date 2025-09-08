namespace Game.Ecs.State.Systems
{
    using System;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public abstract class StateComponentSystem<TComponent> : IProtoRunSystem
        where TComponent : struct, IStateComponent
    {
        protected  ProtoIt _stateFilter = It
            .Chain<TComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in StateFilter)
            {
                OnStateRun();
                break;
            }
        }

        protected abstract void OnStateRun();
    }
}