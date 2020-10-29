using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z.ContextMenu
{
	public static class PrefabExtensions
	{
		public static Button GetButton(this PrefabProviderTool prefabs, string label, string style = null)
		{
			return prefabs.InstantiatePrefab<Button>(prefabs.target, label, style);

		}

		public static Toggle GetToggle(this PrefabProviderTool prefabs, string label, string style = null)
		{
			return prefabs.InstantiatePrefab<Toggle>(prefabs.target, label, style);

		}

		public static Slider GetSlider(this PrefabProviderTool prefabs, string label, string style = null)
		{
			return prefabs.InstantiatePrefab<Slider>(prefabs.target, label, style);
		}

		public static Text GetLabel(this PrefabProviderTool prefabs, string label, string style = null)
		{
			return prefabs.InstantiatePrefab<Text>(prefabs.target, label, style);

		}

		public static Button GetNestedButton(this PrefabProviderTool prefabs, string label, System.Action<PrefabProviderTool> callback, string style = null)
		{
			Button button = prefabs.GetButton(label, style);
			button.onClick.AddListener(() =>
			{
				var context = prefabs.source as ContextMenu;
				var newMenu = context.GetMenu();
				if (callback != null)
					callback.Invoke(newMenu);
				context.MovePanel(newMenu.target.GetComponent<RectTransform>());
			});
			return button;
		}
	}
}