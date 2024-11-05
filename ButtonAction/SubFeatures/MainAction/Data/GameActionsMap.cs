namespace Game.Ecs.ButtonAction.SubFeatures.MainAction.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Sirenix.OdinInspector;
    using UniModules;
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
    using UniModules.Editor;
#endif

    [CreateAssetMenu(menuName = "Proto Features/Game Actions/Actions Map", fileName = "Actions Map")]
    public class GameActionsMap : ScriptableObject
    {
        [InlineProperty]
        [HideLabel]
        public GameActionsData value = new();

        #region IdGenerator

#if UNITY_EDITOR
        [Button("Generate Static Properties")]
        public void GenerateProperties()
        {
            GenerateStaticProperties(this);
        }

        public static void GenerateStaticProperties(GameActionsMap map)
        {
            var idType = typeof(GameActionId);
            var idTypeName = nameof(GameActionId);
            var idsTypeName = $"{nameof(GameActionId)}s";
            var generatedName = $"{idsTypeName}.Generated.cs";
            var monoScript = MonoScript.FromScriptableObject(map);
            var scriptPath = EditorPathConstants.GeneratedContentPath
                .CombinePath("GameActions")
                .CombinePath(generatedName);
            
            var directoryPath = Path.GetDirectoryName(scriptPath);
            var outputPath = directoryPath;
            var outputFileName = generatedName;

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
                writer.WriteLine($"    public partial struct {idsTypeName}");
                writer.WriteLine("    {");

                var typesField = typeof(GameActionsData)
                    .GetField(nameof(GameActionsData.collection),
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (typesField != null)
                {
                    var types = (List<MainAction>)typesField.GetValue(map.value);
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