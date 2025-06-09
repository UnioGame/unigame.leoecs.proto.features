namespace UniGame.Ecs.Proto.Characteristics.CharacteristicsTools.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using Runtime.ReflectionUtils;
    using Runtime.Utils;
    using UnityEngine;
    using Object = UnityEngine.Object;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
#if ODIN_INSPECTOR
    using Sirenix.Utilities.Editor;
#endif
    using UnityEditor;
#endif
    
    [Serializable]
    public sealed class CharacteristicsConverter : LeoEcsConverter, ILeoEcsGizmosDrawer, IEcsConverterProvider
    {
#if ODIN_INSPECTOR
        [Required]
        [ListDrawerSettings(ShowFoldout = true)]
#endif
        [SerializeReference]
        [ItemNotNull]
        public List<ICharacteristicConverter> characteristics = new List<ICharacteristicConverter>();
        
        public IEnumerable<IEcsComponentConverter> Converters => characteristics;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Reset()
        {
#if UNITY_EDITOR
            var unityObjectType = typeof(Object);
            var types = TypeCache.GetTypesDerivedFrom<ICharacteristicConverter>();
            
            characteristics.RemoveAll(x => x == null);
            
            foreach (var type in types)
            {
                if(type.IsAbstract || type.IsInterface || unityObjectType.IsAssignableFrom(type))
                    continue;

                var existsItem = characteristics
                    .FirstOrDefault(x => x.GetType() == type);
                if(existsItem!=null) continue;

                var item = type.CreateWithDefaultConstructor() as ICharacteristicConverter;
   
                characteristics.Add(item);
            }
#endif
        }
        
        public T GetConverter<T>() where T : class
        {
            foreach (var converter in characteristics)
                if(converter is T result) return result;

            return null;
        }

        public void RemoveConverter<T>() where T : IEcsComponentConverter
        {
            characteristics.RemoveAll(x => x is T);
        }

        public IEcsComponentConverter GetConverter(Type target)
        {
            foreach (var converter in characteristics)
                if(converter.GetType() == target) return converter;
            return null;
        }

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            foreach (var characteristic in characteristics)
            {
                characteristic.Apply(world, entity);
            }
        }

        public override bool IsMatch(string searchString)
        {
            var result = base.IsMatch(searchString);
            if (result) return true;
            foreach (var converter in characteristics)
                if (converter.IsMatch(searchString)) return true;

            return false;
        }

        public void DrawGizmos(GameObject target)
        {
            foreach (var converter in characteristics)
            {
                if(converter is not ILeoEcsGizmosDrawer drawer) continue;
                drawer.DrawGizmos(target);
            }
        }
        
        private void BeginDrawListElement(int index)
        {
#if UNITY_EDITOR && ODIN_INSPECTOR
            var item =  characteristics[index];
            var label = item.GetType().PrettyName();
            SirenixEditorGUI.BeginBox(label);
#endif
        }

    }
}