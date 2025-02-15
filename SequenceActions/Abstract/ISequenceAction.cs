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
        
        UniTask<SequenceActionResultComponent> Execute(ProtoPackedEntity sequenceEntity,
            ProtoWorld world,CancellationToken cancellationToken = default);
    }
    
}