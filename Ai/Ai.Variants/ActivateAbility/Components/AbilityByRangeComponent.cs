namespace UniGame.Ecs.Proto.GameAi.ActivateAbility.Components
{
    using System;
    using Game.Code.Ai.ActivateAbility;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

    /// <summary>
    /// Настройка ИИ «бей по дистанции»: способность применяется по целям в заданном радиусе вокруг точки,
    /// ближе минимальной дистанции не бьём.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityByRangeComponent : IApplyableComponent<AbilityByRangeComponent>
    {
        public float Priority;
        public Vector3 Center;
        public float Radius;
        public float MinDistance;
        public AbilityFilter[] FilterData;

        public void Apply(ref AbilityByRangeComponent component)
        {
            component.Center = Center;
            component.Radius = Radius;
            component.Priority = Priority;
            component.MinDistance = MinDistance;
            component.FilterData = FilterData;
        }
    }
}
