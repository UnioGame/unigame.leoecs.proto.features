namespace UniGame.Ecs.Proto.TargetSelection
{
    using Leopotam.EcsProto;

    public class TargetSelectionData
    {
        public static readonly ProtoEntity EmptyResult = (ProtoEntity)(-1);
        // Set from TargetSelectionFeature (asset fields) before its systems are created.
        // MaxAgents: how many entities with Health the kd-tree tracks; extra ones are silently not selectable.
        // MaxTargets: how many targets one area query returns.
        public static int MaxTargets = 64;
        public static int MaxAgents = 512;
    }
}