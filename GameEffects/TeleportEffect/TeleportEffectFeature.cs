namespace UniGame.Ecs.Proto.GameEffects.TeleportEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Teleport Effect Feature")]
    public sealed class TeleportEffectFeature : EffectFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessTeleportEffectSystem());

            return UniTask.CompletedTask;
        }
    }
}