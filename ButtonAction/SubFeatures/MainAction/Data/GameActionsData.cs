namespace Game.Ecs.ButtonAction.SubFeatures.MainAction.Data
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    [Serializable]
    public class GameActionsData
    {
        [InlineProperty]
        public List<MainAction> collection = new();
    }
}