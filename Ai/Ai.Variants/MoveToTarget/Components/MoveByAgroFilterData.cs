namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
    using Game.Code.GameLayers.Category;
    using Game.Code.GameLayers.Relationship;

    [Serializable]
    public struct MoveByAgroFilterData
    {
        [RelationshipIdMask]
        public RelationshipId RelationshipId;
        public CategoryId CategoryId;
        public float SensorDistance;
        public float AgroEffectMultiplier;
    }
}