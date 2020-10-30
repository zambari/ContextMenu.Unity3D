using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z.ContextMenu
{
	public class ContextMenuTransform : MonoBehaviour, IContextMenu
	{
		public bool showRotation = true;
		public bool showPosition = true;
		public void BuildContextMenu(PrefabProxy prefabs)
		{
			if (!showRotation && !showPosition) return;
			prefabs.GetNestedButton("Transform", (nested) =>
			{
				if (showRotation)
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
				if (showPosition)
				{
					nested.GetLabel("Position", "header");
					Vector3 localPos = transform.localPosition;
					var px = nested.GetSlider("X");
					var py = nested.GetSlider("Y");
					var pz = nested.GetSlider("Z");

					float posRange = 10;
					if (localPos.x > 100 || localPos.y > 100) posRange = Screen.width; // on canvas
					px.maxValue = posRange;
					py.maxValue = posRange;
					pz.maxValue = posRange;
					px.minValue = -posRange;
					py.minValue = -posRange;
					pz.minValue = -posRange;
					px.value = localPos.x;
					py.value = localPos.y;
					pz.value = localPos.z;
					px.onValueChanged.AddListener((v) => { var pos = transform.localPosition; pos.x = v; transform.localPosition = pos; });
					pz.onValueChanged.AddListener((v) => { var pos = transform.localPosition; pos.z = v; transform.localPosition = pos; });
					py.onValueChanged.AddListener((v) => { var pos = transform.localPosition; pos.y = v; transform.localPosition = pos; });
				}
			});

		}
	}

}