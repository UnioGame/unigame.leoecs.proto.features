namespace Game.ECS.UI.GameActionsViews.Data
{
    using System;
    using System.Collections.Generic;
    using global::Game.Ecs.GameActions.Data;

    using UniGame.UiSystem.Runtime.Settings;
    using UniModules.UniGame.UiSystem.Runtime;
    using UnityEngine.Serialization;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class GameActionViewsData
    {
        public List<GameActionViewData> gameActionsViews = new();
    }
    
    [Serializable]
    public class GameActionViewData
    {
        public GameActionId gameActionId;

        public ViewId view;

        public ViewType viewType = ViewType.Screen;

        [FormerlySerializedAs("spawnWithContainer")]
        public bool spawnInContainer = false;

#if ODIN_INSPECTOR
        [ShowIf(nameof(spawnInContainer))]
#endif
        public bool useBusyContainer = false;
    }
}