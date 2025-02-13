namespace Game.Ecs.State.Editor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public static class StateCodeGenerator
    {
        public static void GenerateClass(string filePath, string className, string scriptContent, bool overwrite = true)
        {
            var path = FixClassPath(filePath, className);

            if (File.Exists(path) && !overwrite)
            {
                Debug.Log($"{className} class already exists. Skipping generation.");
                return;
            }

            File.WriteAllText(path, scriptContent);

            Debug.Log($"{className} class generated successfully.");
        }

        public static void DeletedFiles(string path)
        {
            if (!Directory.Exists(path)) return;

            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        public static string CreateNameSpace(string path)
        {
            if (path.StartsWith("Assets/"))
                path = path["Assets/".Length..];

            var result = path.Replace("/", ".");
            result = result.Replace(" ", string.Empty);

            return result;
        }

        public static string FormatName(string originalName)
        {
            var nameWithoutSpaces = originalName.Replace(" ", "");

            if (!string.IsNullOrEmpty(nameWithoutSpaces))
            {
                return char.ToUpper(nameWithoutSpaces[0]) + nameWithoutSpaces[1..];
            }

            return originalName;
        }

        public static string ComponentBase(string nameSpace, string name)
        {
            return $@"namespace {nameSpace}
{{
    using System;

    /// <summary>
    ///   
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct {name}Component
    {{
        
    }}
 }}";
        }

        public static string GenerateAspectClass(string aspectNamespace, List<string> properties)
        {
            var propertiesCode = string.Join(Environment.NewLine + "        ", properties);

            return $@"namespace {aspectNamespace}
{{
    using Components.Generated;
    using Leopotam.EcsProto;

    public partial class StateGeneratedAspect
    {{
        {propertiesCode}
    }}
}}";
        }

        public static string BehaviourBase(string nameSpace, string name)
        {
            return $@"namespace {nameSpace}
{{
    using System;
    using Components;
    using Components.Generated;
    using Leopotam.EcsProto;
    using Data;
    using Shared.Generated;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public class {name}Behaviour : IStateBehaviour
    {{
        public void AddState(ProtoWorld world, ProtoEntity stateEntity, StateId stateId)
        {{
            world.GetOrAddComponent<{name}Component>(stateEntity);
            ref var stateIdComponent = ref world.GetOrAddComponent<StateComponent>(stateEntity);
            stateIdComponent.StateId = stateId;
        }}

        public void ChangeState(ProtoWorld world, ProtoEntity stateEntity, StateId stateId)
        {{
            world.GetOrAddComponent<{name}Component>(stateEntity);
            ref var stateIdComponent = ref world.GetComponent<StateComponent>(stateEntity);
            stateIdComponent.StateId = stateId;
        }}

        public void RemoveState(ProtoWorld world, ProtoEntity stateEntity)
        {{
            world.TryRemoveComponent<{name}Component>(stateEntity);
        }}

        public void RemoveStateAndId(ProtoWorld world, ProtoEntity stateEntity)
        {{
            world.TryRemoveComponent<{name}Component>(stateEntity);
            world.TryRemoveComponent<StateComponent>(stateEntity);
        }}
    }}
 }}";
        }

        private static string FixClassPath(string path, string name) =>
            string.IsNullOrEmpty(path) ? path : Path.Combine(path, $"{name}.cs");
    }
}