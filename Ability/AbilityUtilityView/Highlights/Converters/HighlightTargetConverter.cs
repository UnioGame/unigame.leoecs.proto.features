namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Highlights.Converters
{
    using System;
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public sealed class HighlightTargetConverter : LeoEcsConverter
    {
        [SerializeField]
        public AssetReferenceGameObject highlight;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            LoadHighlightAsync(world,entity,target.GetAssetLifeTime()).Forget();
        }

        private async UniTask LoadHighlightAsync(ProtoWorld world,ProtoEntity entity,ILifeTime lifeTime)
        {
            var asset = await highlight.LoadAssetInstanceTaskAsync<GameObject>(lifeTime,true);
            ApplyComponent(world, entity, asset);
        }

        private void ApplyComponent(ProtoWorld world, ProtoEntity entity,GameObject highlightAsset)
        {
            ref var highlightComponent = ref world.GetOrAddComponent<HighlightComponent>(entity);
            highlightComponent.Highlight = highlightAsset;
        }
    }
}