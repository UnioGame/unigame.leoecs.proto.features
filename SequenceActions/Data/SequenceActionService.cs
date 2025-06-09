namespace Game.Modules.SequenceActions.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Feature.SequenceActions.Data;
    using UniGame.UniNodes.GameFlow.Runtime;

    [Serializable]
    public class SequenceActionService : GameService, ISequenceActionService
    {
        private SequenceActionsMapAsset _map;
        private Dictionary<string,SequenceActionItem> _actions;
        
        public SequenceActionService(SequenceActionsMapAsset map)
        {
            _map = map;
            _actions = map.actions.ToDictionary(x => x.ActionName);
        }

        public SequenceActionItem GetAction(string actionName)
        {
            return _actions.GetValueOrDefault(actionName);
        }
    }
}