namespace UniGame.Ecs.Proto.GameResources.Converters
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AddressablesGuidConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        private AssetReference reference;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var addressablesGuidComponent = ref world.AddComponent<AddressablesGuidComponent>(entity);
            addressablesGuidComponent.Guid = reference.AssetGUID;
        }

#if UNITY_EDITOR
        [Button]
        private void SetReferenceSelf()
        {
            var targetPath = AssetDatabase.GetAssetPath(gameObject);
            
            if (string.IsNullOrEmpty(targetPath))
            {
                Debug.LogError($"Путь для {gameObject.name} не найден в AssetDatabase.");
                return;
            }
            
            var guid = AssetDatabase.AssetPathToGUID(targetPath);
            if (string.IsNullOrEmpty(guid))
            {
                Debug.LogError($"GUID для {targetPath} не найден.");
                return;
            }
            
            reference = new AssetReference(guid);

            Debug.Log($"Успешно установлен AssetReference для {gameObject.name} с GUID {guid}");
        }
#endif
    }
}