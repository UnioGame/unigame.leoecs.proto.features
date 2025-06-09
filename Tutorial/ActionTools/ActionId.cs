namespace UniGame.Ecs.Proto.Gameplay.Tutorial.ActionTools
{
	using System;
	using UnityEngine;

#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
	[Serializable]
#if ODIN_INSPECTOR
	[ValueDropdown("@unigame.ecs.proto.Gameplay.Tutorial.ActionTools.ActionTool.GetActionIds()",IsUniqueList = true,DropdownTitle = "Action Id")]
#endif
	public class ActionId
	{
		[SerializeField]
		public string value = string.Empty;

		public static implicit operator string(ActionId v)
		{
			return v.value;
		}

		public static explicit operator ActionId(string v)
		{
			return new ActionId { value = v };
		}
		
		public override string ToString() => value;
	}
}