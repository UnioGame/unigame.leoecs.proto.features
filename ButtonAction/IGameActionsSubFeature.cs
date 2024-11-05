namespace Game.Ecs.ButtonAction
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;

    public interface IGameActionsSubFeature
    {
        UniTask<IProtoSystems> InitializeActions(IProtoSystems ecsSystems);
    }
}