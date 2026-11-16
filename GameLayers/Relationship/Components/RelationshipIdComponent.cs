namespace UniGame.Ecs.Proto.GameLayers.Relationship.Components
{
    using System;
    using Game.Code.GameLayers.Relationship;

    /// <summary>
    /// Отношение, по которому способность выбирает цели: союзники, враги или сам.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct RelationshipIdComponent
    {
        public RelationshipId Value;
    }
}