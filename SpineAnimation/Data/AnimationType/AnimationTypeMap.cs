namespace Game.Ecs.SpineAnimation.Data.AnimationType
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Sirenix.OdinInspector;
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CreateAssetMenu(menuName = "Game/Maps/Animation Type Map")]
    public class AnimationTypeMap : ScriptableObject
    {
        [InlineProperty]
        [HideLabel]
        public AnimationTypeData value = new();

        #region IdGenerator

#if UNITY_EDITOR
        [Button("Generate Static Properties")]
        public void GenerateProperties()
        {
            GenerateStaticProperties(this);
        }

        public static void GenerateStaticProperties(AnimationTypeMap map)
        {
            var idType = typeof(AnimationTypeId);
            var idTypeName = nameof(AnimationTypeId);
            var scriptPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(map));
            var directoryPath = Path.GetDirectoryName(scriptPath);
            var outputPath = Path.Combine(directoryPath, "Generated");
            var outputFileName = $"{idTypeName}.Generated.cs";

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
                writer.WriteLine($"    public partial struct {idTypeName}");
                writer.WriteLine("    {");

                var typesField = typeof(AnimationTypeData)
                    .GetField(nameof(AnimationTypeData.collection),
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (typesField != null)
                {
                    var types = (List<AnimationType>)typesField.GetValue(map.value);
                    foreach (var type in types)
                    {
                        var propertyName = type.name.Replace(" ", "");
                        writer.WriteLine(
                            $"        public static {idTypeName} {propertyName} = new {idTypeName} {{ value = {type.id} }};");
                    }
                }

                writer.WriteLine("    }");
                writer.WriteLine("}");
            }

            AssetDatabase.Refresh();
            Debug.Log("Partial class with static properties generated successfully.");
        }

#endif

        #endregion

    }
}