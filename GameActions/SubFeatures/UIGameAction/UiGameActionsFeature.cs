namespace ECS.GameActions.UIGameAction
{
    using System;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.ButtonAction;
    using Game.Ecs.ButtonAction.Components.Requests;
    using Game.Ecs.GameActions.Systems;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public class UiGameActionsFeature : GameActionsSubFeature
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            // System for handling main button actions.
            ecsSystems.Add(new UiButtonActionSystem());
            ecsSystems.DelHere<ButtonGameActionSelfRequest>();
            
            return UniTask.CompletedTask;
        }
    }
}