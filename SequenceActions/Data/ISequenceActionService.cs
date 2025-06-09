namespace Game.Modules.SequenceActions.Data
{
    using Feature.SequenceActions.Data;
    using UniGame.GameFlow.Runtime;

    public interface ISequenceActionService : IGameService
    {
        SequenceActionItem GetAction(string actionName);
    }
}