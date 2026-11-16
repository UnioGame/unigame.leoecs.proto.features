namespace UniGame.Ecs.Proto.GameLayers.Category.Components
{
    using System;
    using Game.Code.GameLayers.Category;

    /// <summary>
    /// Категория сущности: кто она такая (ближний бой, дальний бой, здание…). По категориям способности и
    /// ИИ выбирают, кого бить.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CategoryIdComponent
    {
        public CategoryId Value;
    }
}
