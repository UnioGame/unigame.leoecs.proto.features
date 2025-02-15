namespace Game.Ecs.State.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Sirenix.OdinInspector;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/States/States Map",fileName = "StatesMap")]
    public class StateDataAsset : ScriptableObject
    {
        #region inspector
        
        [ListDrawerSettings(ListElementLabelName = "@Name")]
        public List<State> Types = new List<State>();
        
        #endregion
        
        private GameStatesMap _map;

        public GameStatesMap Map
        {
            get
            {
                if (_map != null) return _map;
                
                _map = new GameStatesMap();
                foreach (var type in Types)
                {
                    _map.States[type.Id] = type;
                    _map.StatesNames[type.Name] = type;
                }

                return _map;
            }
        }
        
        #region IdGenerator

#if UNITY_EDITOR
        [Button("Generate Static Properties")]
        public void GenerateProperties()
        {
            GenerateStaticProperties(this);
        }

        public static void GenerateStaticProperties(StateDataAsset dataAsset)
        {
            var idType = typeof(StateId);
            var typeName = nameof(StateId);
            var outputPath = Path.Combine("Assets/UniGame.Generated",$"Ecs/{typeName}");
            var outputFileName = $"{typeName}.Generated.cs";

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
                writer.WriteLine("    public class StateIds");
                writer.WriteLine("    {");

                var typesField = typeof(StateDataAsset).GetField("Types",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (typesField != null)
                {
                    var types = (List<State>)typesField.GetValue(dataAsset);
                    foreach (var type in types)
                    {
                        var propertyName = type.Name.Replace(" ", "");
                        writer.WriteLine(
                            $"        public static StateId {propertyName} = new StateId {{ value = {type.Id} }};");
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