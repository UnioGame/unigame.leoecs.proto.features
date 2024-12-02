namespace Game.Ecs.GameActions.Data
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    [Serializable]
    public class GameActionsData
    {
        [InlineProperty]
        [ListDrawerSettings(ListElementLabelName = "@name")]
        public List<GameActionData> collection = new();
    }
}