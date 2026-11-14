namespace UniGame.Ecs.Proto.Gameplay.Dodge
{
    using System;
    using Cysharp.Threading.Tasks;
    using Damage;
    using Events;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public class DamageDodgeFeature : DamageSubFeature
    {
        public sealed override UniTask BeforeDamageSystem(IProtoSystems ecsSystems)
        {
            ecsSystems.DelHere<MissedEvent>();
            //if unit ready to death then create KillRequest
            ecsSystems.Add(new CheckDamageDodgeSystem());

            return UniTask.CompletedTask;
        }
    }
}