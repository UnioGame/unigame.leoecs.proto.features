namespace UniGame.Ecs.Proto.Gameplay.Dodge
{
    using System;
    using Cysharp.Threading.Tasks;
    using Damage;
    using Damage.Systems;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public class DamageShieldFeature : DamageSubFeature
    {
        public sealed override UniTask BeforeDamageSystem(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new CheckDamageShieldSystem());

            return UniTask.CompletedTask;
        }
    }
}