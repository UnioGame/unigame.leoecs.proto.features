namespace UniGame.Ecs.Proto.AI.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;

    using UnityEngine;
    using UnityEngine.Serialization;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class AiAgentConfiguration
    {
        #region inspector

#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        [FormerlySerializedAs("_planners")]
        [SerializeReference]
        public List<PlannerConverter> planners = new List<PlannerConverter>();

        #endregion

        public IReadOnlyList<AiAgentActionId> Actions => planners.Select(x => x.Id).ToList();

        public IReadOnlyList<IPlannerConverter> Planners => planners;
    }
}