namespace Game.Ecs.State
{
    using Leopotam.EcsProto;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto.QoL;
    using Modules.Feature.SequenceActions.Data;
    using Modules.SequenceActions.Data;
    using Modules.SequenceActions.Components.Events;
    using Modules.SequenceActions.Components.Requests;
    using Modules.SequenceActions.Systems;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UnityEditor;
    using UniModules.Editor;
#endif

    [CreateAssetMenu(menuName = "ECS Proto/Features/SequenceActions/SequenceActions Feature",fileName = "SequenceActions")]
    public class SequenceActionsFeature : BaseLeoEcsFeature
    {
#if ODIN_INSPECTOR
        [InlineEditor]
        [HideLabel]
#endif
        [Header("actions map")]
        public SequenceActionsMapAsset sequenceActionsMap;

        public sealed override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var lifeTime = world.GetWorldLifeTime();
            var context = ecsSystems.GetService<IContext>();
            var asset = Instantiate(sequenceActionsMap);
            var service = new SequenceActionService(asset).AddTo(lifeTime);
            
            world.SetGlobal<ISequenceActionService>(service);
            world.SetGlobal(asset);
            context.Publish<ISequenceActionService>(service);

            ecsSystems.AddSystem(new HandleCompletedSequenceSystem());
            
            ecsSystems.DelHere<SequenceCompleteSelfEvent>();
            
            ecsSystems.AddSystem(new StartSequenceByIdSystem());
            ecsSystems.AddSystem(new StartSequenceSystem());
            
            ecsSystems.AddSystem(new UpdateSequenceActionSystem());
            ecsSystems.AddSystem(new UpdateSequenceSystem());
            
            ecsSystems.DelHere<StartSequenceByIdRequest>();
            ecsSystems.DelHere<StartSequenceRequest>();
            
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