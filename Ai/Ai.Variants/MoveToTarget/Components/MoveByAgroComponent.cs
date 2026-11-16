namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

    /// <summary>
    /// Настройка ИИ «иди за тем, кто тебя разозлил»: каких врагов и на каком расстоянии замечать.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MoveByAgroComponent : IApplyableComponent<MoveByAgroComponent>
    {
        [SerializeField]
        public MoveByAgroFilterData[] FilterData;

        public void Apply(ref MoveByAgroComponent component)
        {
            component.FilterData = FilterData;
        }
    }
}