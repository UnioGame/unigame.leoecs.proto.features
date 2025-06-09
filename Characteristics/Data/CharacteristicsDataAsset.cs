using System;

using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections.Generic;

using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

#if UNITY_EDITOR
using UniModules.Editor;
#endif

using System.IO;
using System.Reflection;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UniGame.Proto.Features.Characteristics
{
    using Core.Runtime;
    using UniModules;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Characteristics Data Asset", fileName = "Characteristics Data Asset")]
    public class CharacteristicsDataAsset : ScriptableObject
    {
        private const string GenerationPath = "Assets/UniGame.Generated/Characteristics";
        private const string GenerationFeaturePath = "Features";
        private const string ReplaceKey = "CHARACTERISTIC_FEATURE";
        private const string FeatureFileName = "CHARACTERISTIC_FEATUREFeature.cs";
        
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public List<Characteristic> Types = new List<Characteristic>();

        #region IdGenerator

#if UNITY_EDITOR
#if ODIN_INSPECTOR || TRI_INSPECTOR
        [Button("Generate Characteristics")]
#endif
        public void GenerateProperties()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
                
                GenerateStaticProperties(this);
                GenerateCharacteristicsFeatures(this);
                
                AssetDatabase.StopAssetEditing();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        public static void GenerateStaticProperties(CharacteristicsDataAsset dataAsset)
        {
            var idType = typeof(CharacteristicId);
            var typeName = nameof(CharacteristicId);
            var outputPath = GenerationPath.FixDirectoryPath();
            var outputFileName = "CharacteristicsId.Generated.cs";

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            var namespaceName = idType.Namespace;

            var filePath = Path.Combine(outputPath, outputFileName);
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine($"namespace {namespaceName}");
                writer.WriteLine("{");
                writer.WriteLine($"    public partial struct {typeName}s");
                writer.WriteLine("    {");

                var typesField = typeof(CharacteristicsDataAsset).GetField("Types",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (typesField != null)
                {
                    var types = (List<Characteristic>)typesField.GetValue(dataAsset);
                    foreach (var type in types)
                    {
                        var propertyName = type.Name.Replace(" ", "");
                        writer.WriteLine(
                            $"        public static {typeName} {propertyName} = new {typeName} {{ value = {type.Id} }};");
                    }
                }

                writer.WriteLine("    }");
                writer.WriteLine("}");
            }

            AssetDatabase.Refresh();
            Debug.Log("Partial class with static properties generated successfully.");
        }

        public static void GenerateCharacteristicsFeatures(CharacteristicsDataAsset dataAsset)
        {
            var monoType = typeof(CHARACTERISTIC_FEATUREFeature);
            var monoScript = monoType.GetScriptAsset();
            var outputPath = GenerationPath.CombinePath(GenerationFeaturePath).FixDirectoryPath();
            var text = monoScript.text;

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
            RemoveDirectoryContent(outputPath);
            
            foreach (var item in dataAsset.Types)
            {
                var result = text.Replace(ReplaceKey, item.Name);
                var fileName = FeatureFileName.Replace(ReplaceKey, item.Name);
                var filePath = outputPath.CombinePath(fileName);
                using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
                writer.WriteLine(result);
            }
        }
        
        public static void RemoveDirectoryContent(string folder)
        {
            if (!Directory.Exists(folder)) return;
            
            var di = new DirectoryInfo(folder);
            
            foreach (var file in di.GetFiles())
            {
                file.Delete(); 
            }
            
            foreach (var dir in di.GetDirectories())
            {
                dir.Delete(true); 
            }
        }
        
#endif

        #endregion

    }

    [Serializable]
    public struct Characteristic
    {
        public string Name;
        public int Id;
    }

#if ODIN_INSPECTOR
    [ValueDropdown("@UniGame.Proto.Features.Characteristics.CharacteristicId.GetCharacteristics()", 
        IsUniqueList = true,
        DropdownTitle = "Characteristics")]
#endif
    [Serializable]

    public partial struct CharacteristicId
    {
        [SerializeField]
        public int value;

        #region static editor data

        private static CharacteristicsDataAsset _dataAsset;

        public static IEnumerable<ValueDropdownItem<CharacteristicId>> GetCharacteristics()
        {
#if UNITY_EDITOR
            _dataAsset ??= AssetEditorTools.GetAsset<CharacteristicsDataAsset>();
            var types = _dataAsset;
            if (types == null)
            {
                yield return new ValueDropdownItem<CharacteristicId>()
                {
                    Text = "EMPTY",
                    Value = (CharacteristicId)0,
                };
                yield break;
            }

            foreach (var type in types.Types)
            {
                yield return new ValueDropdownItem<CharacteristicId>()
                {
                    Text = type.Name,
                    Value = (CharacteristicId)type.Id,
                };
            }
#endif
            yield break;
        }

        public static string GetCharacteristicsName(CharacteristicId slotId)
        {
#if UNITY_EDITOR
            var types = GetCharacteristics();
            var filteredTypes = types
                .FirstOrDefault(x => x.Value == slotId);
            var slotName = filteredTypes.Text;
            return string.IsNullOrEmpty(slotName) ? string.Empty : slotName;
#endif
            return string.Empty;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()
        {
            _dataAsset = null;
        }

        #endregion

        public static implicit operator int(CharacteristicId v)
        {
            return v.value;
        }

        public static explicit operator CharacteristicId(int v)
        {
            return new CharacteristicId { value = v };
        }

        public override string ToString() => value.ToString();

        public override int GetHashCode() => value;

        public CharacteristicId FromInt(int data)
        {
            value = data;

            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj is CharacteristicId mask)
                return mask.value == value;

            return false;
        }
    }
}