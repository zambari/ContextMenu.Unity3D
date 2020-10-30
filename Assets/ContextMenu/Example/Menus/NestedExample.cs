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
			// prefabs.GetNestedButton("Rotation", NestedMenu);
			prefabs.GetNestedButton("Rotation", (nested) =>
			{
				nested.GetLabel("Rotation", "header");
				var x = nested.GetSlider("X");
				var y = nested.GetSlider("Y");
				var z = nested.GetSlider("Z");
				x.maxValue = 360;
				y.maxValue = 360;
				z.maxValue = 360;
				x.value = transform.localEulerAngles.x;
				y.value = transform.localEulerAngles.y;
				z.value = transform.localEulerAngles.z;
				x.onValueChanged.AddListener((v) => { var rot = transform.localEulerAngles; rot.x = v; transform.localEulerAngles = rot; });
				z.onValueChanged.AddListener((v) => { var rot = transform.localEulerAngles; rot.z = v; transform.localEulerAngles = rot; });
				y.onValueChanged.AddListener((v) => { var rot = transform.localEulerAngles; rot.y = v; transform.localEulerAngles = rot; });

			});

			prefabs.GetNestedButton("SubMenu with right", (submenu) =>
			{
				submenu.GetButton("ButtonA");
				submenu.GetButton("ButtonB");
				submenu.GetButton("ButtonC");
			}).gameObject.AddComponent<NestedExample>();
		}

		void NestedMenu(PrefabProviderTool nested)
		{
			nested.GetLabel("Rotation", "header");
			var x = nested.GetSlider("X");
			var y = nested.GetSlider("Y");
			var z = nested.GetSlider("Z");
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