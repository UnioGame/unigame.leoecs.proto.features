namespace UniGame.Ecs.Proto.Presets.DirectionalLight.Converters
{
    using Leopotam.EcsProto;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;
    public class MonoDirectionalLightSettingsPresetSourceConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        [InlineProperty]
        [HideLabel]
        public DirectionalLightSettingsSourceConverter directionalLightConverter = new();

        public sealed override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            directionalLightConverter.Apply(world, entity);
        }
    }
}