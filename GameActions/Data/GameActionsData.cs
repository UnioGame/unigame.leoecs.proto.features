namespace Game.Ecs.GameActions.Data
{
    using System;
    using System.Collections.Generic;
  

#if ODIN_INSPECTOR
  using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class GameActionsData
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [ListDrawerSettings(ListElementLabelName = "@name")]
#endif
        public List<GameActionData> collection = new();
    }
}