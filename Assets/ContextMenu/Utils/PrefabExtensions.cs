using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z.ContextMenu
{
	public static class PrefabExtensions
	{
		static int offset {get { return 15;}} // for submenu placement
		public static T InstantiatePrefab<T>(this PrefabProxy prefabs, Transform target, string label, string style = null) where T : Component
		{
			return prefabs.provider.InstantiatePrefab<T>(target, label, style);
		}

		public static Button GetButton(this PrefabProxy prefabs, string label, string style = null)
		{
			return prefabs.InstantiatePrefab<Button>(prefabs.target, label, style);

		}

		public static Toggle GetToggle(this PrefabProxy prefabs, string label, string style = null)
		{
			return prefabs.InstantiatePrefab<Toggle>(prefabs.target, label, style);

		}

		public static Slider GetSlider(this PrefabProxy prefabs, string label, string style = null)
		{
			return prefabs.InstantiatePrefab<Slider>(prefabs.target, label, style);
		}

		public static Text GetLabel(this PrefabProxy prefabs, string label, string style = null)
		{
			return prefabs.InstantiatePrefab<Text>(prefabs.target, label, style);

		}

		public static Button GetNestedButton(this PrefabProxy prefabs, string label, System.Action<PrefabProxy> callback, string style = null)
		{
			Button button = prefabs.GetButton(label, style);
			button.onClick.AddListener(() =>
			{
				var context = prefabs.source as ContextMenu;
				var newMenu = context.GetMenu(label);
				RectTransform newTarget= newMenu.target.GetComponent<RectTransform>();
				
				if (callback != null)
					callback.Invoke(newMenu); // build the sumbenu
				context.MovePanel(newTarget);

				// ofset the sumbenu
				RectTransform buttonRect=button.GetComponent<RectTransform>();
				Vector3[] corners =new Vector3[4];
				buttonRect.GetWorldCorners(corners);
				float xstart=corners[0].x;
				float xend=corners[2].x;
				Vector3 currentpos=newTarget.position;
				currentpos.x=xend+offset;
				newTarget.position=currentpos;

			});
			return button;
		}

		public static RectTransform GetPanel(this PrefabProviderBase prefabs, Transform target, string label, string style = null)
		{
			var instanced = prefabs.InstantiatePrefab<RectTransform>(target, label, style);
			return instanced;
		}

		public static void SetLabel(this Component component, string label, bool destroyIfNoLabel = true)
		{
			component.name = label;
			var text = component.GetComponentInChildren<Text>();
			if (destroyIfNoLabel && string.IsNullOrEmpty(label))
			{
				GameObject.Destroy(text.gameObject);
				return;
			}
			if (text != null) text.text = label;

		}
	}
}