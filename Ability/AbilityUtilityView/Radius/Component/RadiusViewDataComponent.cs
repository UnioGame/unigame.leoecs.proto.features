namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Component
{
    using System;
    using UnityEngine;

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
    public struct RadiusViewDataComponent
    {
        public GameObject InvalidRadiusView;
        public GameObject ValidRadiusView;
    }
}