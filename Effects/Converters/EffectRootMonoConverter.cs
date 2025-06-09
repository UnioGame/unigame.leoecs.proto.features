namespace UniGame.Ecs.Proto.Effects.Converters
{

    using UniGame.LeoEcs.Converter.Runtime.Converters;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    public class EffectRootMonoConverter : MonoLeoEcsConverter<EffectRootConverter>
    {
#if ODIN_INSPECTOR
        [Button]
#endif
        public void BakeActive()
        {
#if UNITY_EDITOR
            converter ??= new EffectRootConverter();
            converter.Bake(gameObject);
            
            this.MarkDirty();
            gameObject.MarkDirty();
#endif
        }
        
    }
}