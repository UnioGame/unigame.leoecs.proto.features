namespace Game.Editor.Runtime.CharacteristicsViewer
{
    using System;


#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if ODIN_INSPECTOR
    [InlineProperty]
    [HideLabel]
#endif

    [Serializable]
    public class EcsCharacteristicDebugView
    {
#if ODIN_INSPECTOR
        [FoldoutGroup("$Name")]
        [InlineProperty]
        [HideLabel]
        [ShowIf(nameof(IsActive))]
#endif
        public CharacteristicValue Value;

        public virtual bool IsActive { get; set; }
        
        public string Name { get; set; }
        
        public void UpdateView()
        {
            Value = CreateView();
        }

#if ODIN_INSPECTOR
        [FoldoutGroup("$Name")]
        [ButtonGroup("$Name/Commands", Stretch = true)]
        [Button]
#endif
        public virtual void Recalculate()
        {
            
        }
        
        public virtual CharacteristicValue CreateView()
        {
            return new CharacteristicValue();
        }
    }
}