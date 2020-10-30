using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z.ContextMenu
{
    public class SimplePrefabProvider : PrefabProviderBase
    {

        public Button buttonPrefab;
        public Text headerPrefab;
        public Text smallLabel;
        public RectTransform panel;
        public Slider sliderPrefab;
        public Toggle togglePrefab;

        T GetTemplate<T>(string style = null) where T : Component
        {
            System.Type typeParameterType = typeof(T);
            // return default(T);
            if (typeParameterType.Equals(typeof(Button)))
                return buttonPrefab as T;
            if (typeParameterType.Equals(typeof(Toggle)))
                return togglePrefab as T;
            if (typeParameterType.Equals(typeof(Slider)))
                return sliderPrefab as T;
            if (typeParameterType.Equals(typeof(Text)))
            {
                if (style != null)
                {
                    if (style.Contains("header"))
                        return headerPrefab as T;
                }
                return smallLabel as T;
            }
            if (typeParameterType.Equals(typeof(RectTransform)))
                return panel as T;
            Debug.Log("no match");
            return default(T);
        }
        public override T InstantiatePrefab<T>(Transform target, string label, string style = null)
        {
            var temlpate = GetTemplate<T>(style);
            var go = GameObject.Instantiate(temlpate.gameObject,target);
            go.transform.SetLabel(label);
            return go.GetComponent<T>();
        }

    }
}