namespace UniGame.Ecs.Proto.GameLayers.Aspects
{
    using System;
    using Category.Components;
    using Layer.Components;
    using Leopotam.EcsProto;
    using Relationship.Components;
    using UniGame.LeoEcs.Bootstrap;

    /// <summary>
    /// Слой, категория и отношение сущностей: кто есть кто и кто кому враг.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class GameLayersAspect : EcsAspect
    {
        public ProtoPool<LayerIdComponent> Layer;
        public ProtoPool<CategoryIdComponent> Category;
        public ProtoPool<RelationshipIdComponent> Relationship;
    }
}