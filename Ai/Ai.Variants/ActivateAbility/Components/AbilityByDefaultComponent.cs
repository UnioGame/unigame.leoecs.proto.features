namespace UniGame.Ecs.Proto.GameAi.ActivateAbility.Components
{
    using System;
    using Game.Code.Ai.ActivateAbility;
    using UniGame.LeoEcs.Shared.Abstract;

    /// <summary>
    /// Настройка ИИ «бей при первой возможности»: способность применяется, как только есть подходящая цель
    /// в её радиусе.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityByDefaultComponent : IApplyableComponent<AbilityByDefaultComponent>
    {
        public float Priority;
        public AbilityFilter[] FilterData;

        public void Apply(ref AbilityByDefaultComponent component)
        {
            component.Priority = Priority;
            component.FilterData = FilterData;
        }
    }
}
