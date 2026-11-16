namespace UniGame.Ecs.Proto.GameLayers.Layer.Components
{
    using System;
    using Game.Code.GameLayers.Layer;

    /// <summary>
    /// Слой сущности: на чьей она стороне (войска, Беды, святилище…). По слоям и матрице отношений понятно,
    /// кто кому враг.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct LayerIdComponent
    {
        public LayerId Value;
    }
}
