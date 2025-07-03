namespace Game.Ecs.State.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Modules.States.Data;
    using Sirenix.OdinInspector;
    using Systems;
    using UniModules.Editor;
    using UniModules.UniCore.Runtime.Utils;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/States/States Map",fileName = "StatesMap")]
    public class StateDataAsset : ScriptableObject
    {
        #region inspector
        
        [ListDrawerSettings(ListElementLabelName = "@Name")]
        public List<State> Types = new List<State>();
        
        [SerializeReference]
        [ListDrawerSettings(ListElementLabelName = "@Name")]
        public List<StateSystem> stateSystems = new();
        
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

        public const string TemplateComponentKey = "COMPONENT_NAME";
        public const string StateComponentTemplate =
            "   public struct COMPONENT_NAME_StateComponent { }";

        public const string ComponentNameTemplate = "{0}_StateComponent";
        public const string AspectNameTemplate = "StatesIdsAspect_Template";
        public const string AspectNameValue = "StatesIdsAspect";
        public const string PoolTemplate = "public ProtoPool<{0}_StateComponent> {1};\n";
        public const string BeginPoolsKey = "//BEGIN_POOLS";
        public const string BeginMapKey = "//BEGIN_MAP";
        
        public const string SystemNameTemplate = "StatesIds_System :";
        public const string SystemNameValue = "StatesIds_System : StateSystem,";
        
        [Button("Generate Static Properties")]
        public void GenerateProperties()
        {
            GenerateStaticProperties(this);
            UpdateStateSystems();
        }
        
        [Button]
        public void UpdateStateSystems()
        {
            var systemsMap = stateSystems.ToDictionary(x => x.GetType());
            var typeSystems = TypeCache.GetTypesDerivedFrom(typeof(StateSystem));
            foreach (var type in typeSystems)
            {
                if(type.IsAbstract || type.IsInterface) continue;
                
                if (systemsMap.ContainsKey(type)) continue;
                var instance = type.CreateWithDefaultConstructor() as StateSystem;
                if(instance == null) continue;
                
                systemsMap[type] = instance;
            }
            
            stateSystems = systemsMap.Values.ToList();
            this.MarkDirty();
        }

        public static void GenerateStaticProperties(StateDataAsset dataAsset)
        {
            var idType = typeof(StateId);
            var typeName = nameof(StateId);
            var outputPath = Path.Combine("Assets/UniGame.Generated",$"Ecs/{typeName}");
            var outputFileName = $"{typeName}.Generated.cs";
            var outputAspectFileName = $"{typeName}.Aspect.Generated.cs";

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            var namespaceName = idType.Namespace;

            var filePath = Path.Combine(outputPath, outputFileName);
            var aspectFilePath = Path.Combine(outputPath, outputAspectFileName);
            var types = dataAsset.Types;
            
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine($"namespace {namespaceName}");
                writer.WriteLine("{");
                writer.WriteLine("    public class StateIds");
                writer.WriteLine("    {");
            
                foreach (var type in types)
                {
                    var propertyName = type.Name.Replace(" ", "");
                    writer.WriteLine(
                        $"        public static StateId {propertyName} = new StateId {{ value = {type.Id} }};");
                }
            
                writer.WriteLine("    }");
            
                foreach (var type in types)
                {
                    var keyName = type.Name.Replace(" ", "");
                    var template = StateComponentTemplate
                        .Replace(TemplateComponentKey,keyName);
                    writer.WriteLine(template);
                }
                
                writer.WriteLine("}");
            }

            var templateType = typeof(StatesIdsAspect_Template);
            var scriptAsset = templateType.GetScriptAsset();
            var scriptText = scriptAsset.text;
            var mapValue = string.Empty;
            var poolsValue = string.Empty;
            
            using (var writer = new StreamWriter(aspectFilePath, false, Encoding.UTF8))
            {
                scriptText = scriptText.Replace(AspectNameTemplate, AspectNameValue);
                foreach (var state in types)
                {
                    var stateName = state.Name.Replace(" ", "");
                    var componentName = string.Format(ComponentNameTemplate, stateName);
                    mapValue += $"{{ {state.Id} , typeof({componentName}) }},\n";
                    poolsValue += string.Format(PoolTemplate, stateName, stateName);
                }
                
                scriptText = scriptText.Replace(BeginMapKey, mapValue);
                scriptText = scriptText.Replace(BeginPoolsKey, poolsValue);
                scriptText = scriptText.Replace(SystemNameTemplate, SystemNameValue);
                
                writer.Write(scriptText);
            }

            AssetDatabase.Refresh();
            Debug.Log("Partial class with static properties generated successfully.");
        }

#endif

        #endregion

    }
}