namespace UniGame.Ecs.Proto.Gameplay.Dodge
{
    using Cysharp.Threading.Tasks;
    using Damage;
    using Events;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Damage/Damage Dodge Feature", fileName = "Damage Dodge Feature")]
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