namespace UniGame.Ecs.Proto.TargetSelection.Systems
{
    using DataStructures.ViliWonka.KDTree;
    using Components;
    using Unity.Mathematics;
    using System;
    using Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;


#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class InitKdTreeTargetsSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private TargetAspect _targetAspect;
        
        private ProtoIt _kdDataFilter = It
            .Chain<KDTreeDataComponent>()
            .Inc<KDTreeComponent>()
            .Inc<KDTreeQueryComponent>()
            .End();

        public void Run()
        {
            if (_kdDataFilter.IsEmpty())
                InitKDTreeTargets();
        }

        private void InitKDTreeTargets()
        {
            var treeEntity = _world.NewEntity();

            _targetAspect.Data.Add(treeEntity);
            ref var treeComponent = ref _targetAspect.Tree.Add(treeEntity);
            ref var radiusQueryComponent = ref _targetAspect.Query.Add(treeEntity);

            var treeData = new float3[TargetSelectionData.MaxAgents];
            var tree = new KDTree(treeData);
            tree.Build(treeData, TargetSelectionData.MaxAgents, TargetSelectionData.MaxTargets);

            treeComponent.Value = tree;

            var radiusQuery = new KDQuery();
            radiusQueryComponent.Value = radiusQuery;
        }
    }
}