namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
    using System.Collections.Generic;
    using Game.Code.Configuration.Runtime.Effects.Abstract;
    using Unity.Mathematics;

    /// <summary>
    /// Действие ИИ «иди туда» на этот кадр: куда идти и какие эффекты повесить на время движения.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MoveToTargetActionComponent
    {
        public float3 Position;
        public List<IEffectConfiguration> Effects;
    }
}
