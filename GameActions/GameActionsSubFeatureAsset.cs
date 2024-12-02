namespace Game.Ecs.ButtonAction
{
    using System;
    using UniGame.LeoEcs.Bootstrap.Runtime;

    [Serializable]
    public abstract class GameActionsSubFeatureAsset : BaseLeoEcsFeature,IGameActionsSubFeature
    {

        public virtual void EditorInitialize(string path)
        {
            
        }
        
    }
    
    [Serializable]
    public abstract class GameActionsSubFeature : EcsFeature,IGameActionsSubFeature
    {
        public virtual void EditorInitialize(string path) { }
    }
}