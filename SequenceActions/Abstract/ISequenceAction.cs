namespace Game.Modules.SequenceActions.Abstract
{
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    public interface ISequenceAction
    {
        string ActionName { get; }
        
        SequenceActionResult Status { get; }

        UniTask ExecuteAsync(ProtoPackedEntity entity, ProtoWorld world,
            CancellationToken cancellationToken = default);
    }
}