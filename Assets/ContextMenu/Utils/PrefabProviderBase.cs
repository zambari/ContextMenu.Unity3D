using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z.ContextMenu
{
	public abstract class PrefabProviderBase : MonoBehaviour, IProvidePrefabs
	{
		public abstract T InstantiatePrefab<T>(Transform target, string label, string style = null) where T : Component;
		public PrefabProxy GetPrefabTool(MonoBehaviour src, Transform content = null)
		{
			if (content == null)
			{
				content = InstantiatePrefab<RectTransform>(src.transform, "spawned").transform;
			}
			var newHelper = new PrefabProxy() { source = src, target = content, provider = this };
			return newHelper;
		}
	
	}

}