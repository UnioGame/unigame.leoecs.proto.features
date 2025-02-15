namespace Game.Ecs.State.Data
{
    using Leopotam.EcsProto;

    public interface IStateBehaviour
    {
        void Initialize(ProtoWorld world);
        void Enter(ProtoEntity entity,ProtoWorld world);
        int Update(ProtoEntity entity,ProtoWorld world);
        void Exit(ProtoEntity entity,ProtoWorld world);       
    }
}