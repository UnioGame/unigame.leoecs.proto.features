namespace Game.Code.Services.Ability.Editor
{
    using System.IO;
    using AbilityLoadout.Data;
    using Configuration.Runtime.Ability;
    using UniModules;
    using UniModules.Editor;
    using UniGame.AddressableTools.Editor;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Timeline;

    public static class AbilityCommands
    {
        public static readonly string DefaultAbilityName = "New Ability";
        public static readonly string DefaultAbilityNameTemplate = "{0} Ability";
        public static readonly string MetaAssetName = "Meta";
        public static readonly string ConfigurationAssetName = "Configuration";
        public static readonly string AnimationLinkAssetName = "AnimationLink";
        public static readonly string AnimationAssetName = "Animation";
        
        public static readonly string AbilityNameTemplate = "{0} {1}";
        
        public static AbilityEditorData CreateAbility(string abilityName)
        {
            var selection = Selection.activeObject;
            var path = AssetDatabase.GetAssetPath(selection);
            var directory = path.GetDirectoryPath();

            return CreateAbility(abilityName, directory);
        }
        
        public static AbilityEditorData CreateAbility(string abilityName,string abilityFolder)
        {
            var abilityMeta = ScriptableObject.CreateInstance<AbilityItemAsset>();
            var abilityConfiguration = ScriptableObject.CreateInstance<AbilityConfiguration>();
            var animation = ScriptableObject.CreateInstance<TimelineAsset>();
            
            abilityMeta.name = string.Format(AbilityNameTemplate,abilityName,MetaAssetName);
            abilityConfiguration.name = string.Format(AbilityNameTemplate,abilityName,ConfigurationAssetName);
            animation.name = string.Format(AbilityNameTemplate,abilityName,AnimationAssetName);

            abilityMeta = abilityMeta.SaveAsset(abilityFolder, false, false);
            abilityConfiguration = abilityConfiguration.SaveAsset(abilityFolder, false, false);
            animation = animation.SaveAsset(abilityFolder, false, false);
            
            AssetDatabase.SaveAssets();
            
            abilityMeta.AddToDefaultAddressableGroup();
            abilityConfiguration.AddToDefaultAddressableGroup();
            animation.AddToDefaultAddressableGroup();
            
            abilityMeta.data.configurationReference =
                new AssetReferenceT<AbilityConfiguration>(abilityConfiguration.GetGUID());


            abilityMeta.MarkDirty();
            abilityConfiguration.MarkDirty();
            animation.MarkDirty();
            
            UpdateAbilitiesCatalog();
            
            return new AbilityEditorData()
            {
                meta = abilityMeta,
                configuration = abilityConfiguration,
                animation = animation
            };
        }

        [MenuItem("Game/Ability/Update Abilities Catalog")]
        public static void UpdateAbilitiesCatalog()
        {
            var catalogs = AssetEditorTools.GetAssets<AbilityDataBase>();
            foreach (var catalog in catalogs)
            {
                catalog.FillCategory();
            }
        }
        
        [MenuItem("Game/Ability/Create New Ability")]
        [MenuItem("Assets/Create/Game/Ability/New Ability")]
        private static void CreateAbility()
        {
            var selection = Selection.activeObject;
            var path = AssetDatabase.GetAssetPath(selection);
            
            var name = string.IsNullOrEmpty(path)
                ? DefaultAbilityName
                : string.Format(DefaultAbilityNameTemplate,Path.GetFileNameWithoutExtension(path));
                
            CreateAbility(name);
        }
    }
}