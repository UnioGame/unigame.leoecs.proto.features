namespace Game.Ecs.Scenes.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UniGame.MultiScene.Runtime;
    using UnityEditor;
    using UniModules.Editor;
#endif

    [CreateAssetMenu(menuName = "ECS Proto/Scenes/Scene Map",fileName = "Scene Map")]
    public class SceneMap : ScriptableObject
    {
#if ODIN_INSPECTOR
        [ListDrawerSettings(ListElementLabelName = "@name")]
#endif
        public List<SceneInfo> collection = new();
        
        #region IdGenerator

#if UNITY_EDITOR

#if ODIN_INSPECTOR
        [Button("Collect Scenes")]
#endif
        public void CollectScenes()
        {
            var sceneAssets = AssetEditorTools.GetAssets<MultiSceneAsset>();
            collection.Clear();
            foreach (var sceneAsset in sceneAssets)
            {
                var sceneInfo = new SceneInfo
                {
                    name = sceneAsset.name,
                    sceneData = sceneAsset,
                    id = sceneAsset.name.GetHashCode()
                };
                
                collection.Add(sceneInfo);
            }

            this.MarkDirty();
        }
        
#if ODIN_INSPECTOR
        [Button("Generate Static Properties")]
#endif
        public void GenerateProperties()
        {
            GenerateStaticProperties(this);
        }
        
        public static void GenerateStaticProperties(SceneMap dataAsset)
        {
            var idType = typeof(SceneId);
            var typeName = nameof(SceneId);
            var outputPath = Path.Combine("Assets/UniGame.Generated",$"Ecs/{typeName}");
            var outputFileName = $"{typeName}.Generated.cs";
            var outputTypeName = $"SceneIds";

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
                writer.WriteLine($"    public class {outputTypeName}");
                writer.WriteLine("    {");

                foreach (var type in dataAsset.collection)
                {
                    var propertyName = type.Name.Replace(" ", "");
                    writer.WriteLine(
                        $"        public static {typeName} {propertyName} = new {typeName} {{ value = {type.Value} }};");
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