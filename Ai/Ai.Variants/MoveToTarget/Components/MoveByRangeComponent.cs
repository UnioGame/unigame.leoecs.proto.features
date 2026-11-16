namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
    using System.Collections.Generic;
    using Game.Code.Configuration.Runtime.Effects.Abstract;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

    /// <summary>
    /// Настройка ИИ «держись рядом с точкой»: если агент ушёл за радиус, его тянет обратно.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MoveByRangeComponent : IApplyableComponent<MoveByRangeComponent>
    {
        private static readonly List<IEffectConfiguration> EmptyEffects = new List<IEffectConfiguration>();
        
        public float Priority;
        public Vector3 Center;
        public float Radius;
        public float MinDistance;
        public List<IEffectConfiguration> Effects;

        public void Apply(ref MoveByRangeComponent component)
        {
            EmptyEffects.Clear();
            
            component.Effects = EmptyEffects;
            component.Center = Center;
            component.Radius = Radius;
            component.Priority = Priority;
            component.MinDistance = MinDistance;
        }
    }
}
