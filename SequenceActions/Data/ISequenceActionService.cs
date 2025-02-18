namespace Game.Modules.leoecs.proto.features.SequenceActions.Data
{
    using Feature.SequenceActions.Data;
    using UniGame.GameFlow.Runtime.Interfaces;

    public interface ISequenceActionService : IGameService
    {
        SequenceActionItem GetAction(string actionName);
    }
}