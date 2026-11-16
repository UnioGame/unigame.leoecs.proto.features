namespace Game.Code.Configuration.Runtime.Ability.Description
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct AbilityId
    {
        public static readonly AbilityId None = (AbilityId)(-1);
        
        [SerializeField]
        private int _id;

        public int Id => _id;

        public static implicit operator int(AbilityId abilityId)
        {
            return abilityId.Id;
        }

        public static explicit operator AbilityId(int abilityId)
        {
            return new AbilityId
            {
                _id = abilityId
            };
        }

        public override string ToString()
        {
            return _id.ToString();
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is AbilityId abilityId)
                return abilityId.Id == Id;

            return false;
        }
    }
}