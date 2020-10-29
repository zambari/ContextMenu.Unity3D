using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z.ContextMenu;
namespace Z.ContextMenu
{
	public class NestedExample : MonoBehaviour, IContextMenu
	{
		public void BuildContextMenu(PrefabProviderTool prefabs)
		{
			prefabs.GetLabel("Rotation submenu");
			prefabs.GetNestedButton("Rotation", NestedMenu);
		}

		void NestedMenu(PrefabProviderTool prefabs)
		{
			prefabs.GetLabel("Nested", "header");
			var x = prefabs.GetSlider("X");
			var y = prefabs.GetSlider("Y");
			var z = prefabs.GetSlider("Z");
			x.maxValue = 360;
			y.maxValue = 360;
			z.maxValue = 360;
			x.value = transform.localEulerAngles.x;
			y.value = transform.localEulerAngles.y;
			z.value = transform.localEulerAngles.z;
			x.onValueChanged.AddListener((v) => { var rot = transform.localEulerAngles; rot.x = v; transform.localEulerAngles = rot; });
			z.onValueChanged.AddListener((v) => { var rot = transform.localEulerAngles; rot.z = v; transform.localEulerAngles = rot; });
			y.onValueChanged.AddListener((v) => { var rot = transform.localEulerAngles; rot.y = v; transform.localEulerAngles = rot; });
		}
	}

}