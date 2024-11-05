namespace Game.Ecs.ButtonAction
{
    using System;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using UnityEngine;

    [Serializable]
    public abstract class GameActionsSubFeature : ScriptableObject
    {
        public virtual UniTask<IProtoSystems> InitializeActions(IProtoSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }
    }
}