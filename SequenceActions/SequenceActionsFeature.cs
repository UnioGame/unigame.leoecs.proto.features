namespace Game.Ecs.State
{
    using Leopotam.EcsProto;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto.QoL;
    using Modules.leoecs.proto.features.SequenceActions.Data;
    using Modules.SequenceActions.Components.Requests;
    using Modules.SequenceActions.Systems;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEditor;
    using UnityEngine;
    
#if UNITY_EDITOR
    using UniModules.Editor;
#endif

    [CreateAssetMenu(menuName = "ECS Proto/Features/SequenceActions/SequenceActions Feature",fileName = "SequenceActions")]
    public class SequenceActionsFeature : BaseLeoEcsFeature
    {
        [Header("actions map")]
        [InlineEditor]
        [HideLabel]
        public SequenceActionsMapAsset sequenceActionsMap;

        public sealed override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.AddSystem(new StartSequenceActionByIdSystem());
            ecsSystems.AddSystem(new StartSequenceSystem());
            
            ecsSystems.AddSystem(new StartSequenceActionSystem());
            ecsSystems.AddSystem(new UpdateSequenceActionSystem());
            ecsSystems.AddSystem(new UpdateSequenceProgressSystem());
            ecsSystems.AddSystem(new UpdateSequenceSystem());

            
            ecsSystems.DelHere<StartSequenceByIdRequest>();
            ecsSystems.DelHere<StartSequenceRequest>();
            ecsSystems.DelHere<StartSequenceActionSelfRequest>();
            
            return UniTask.CompletedTask;
        }

        
#if UNITY_EDITOR
#if ODIN_INSPECTOR
        
        [OnInspectorInit]
        private void OnInspectorInit()
        {
            if (sequenceActionsMap != null) return;
            
            var statesAsset = this.CreateAsset<SequenceActionsMapAsset>("SequenceActionsMap");
            statesAsset.SaveAsset();
            sequenceActionsMap = statesAsset;
            this.MarkDirty();
            AssetDatabase.SaveAssets();

        }
        
#endif
#endif

    }
}