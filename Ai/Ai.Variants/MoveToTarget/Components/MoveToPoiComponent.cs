namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
    using Game.Code.GameLayers.Category;
    using Unity.Mathematics;

    /// <summary>
    /// Точка интереса: куда могут идти агенты нужной категории и насколько это важно.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MoveToPoiComponent
    {
        public float Priority;
        public float3 Position;
        public CategoryId CategoryId;
    }
}
