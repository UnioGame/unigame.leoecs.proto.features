namespace UniGame.Ecs.Proto.Gameplay.Dodge
{
    using Cysharp.Threading.Tasks;
    using Damage;
    using Damage.Systems;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Damage/Damage Shield Feature", fileName = "Damage Shield Feature")]
    public class DamageShieldFeature : DamageSubFeature
    {
        public sealed override UniTask BeforeDamageSystem(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new CheckDamageShieldSystem());

            return UniTask.CompletedTask;
        }
    }
}