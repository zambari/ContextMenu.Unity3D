using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z.ContextMenu
{
    public class ContextMenuExampleBuilder : MonoBehaviour, IContextMenu
    {
        public bool startWithObjectName;
        public void BuildContextMenu(PrefabProxy prefabs)
        {
            if (startWithObjectName) prefabs.GetLabel(name);
            var scaleSlider = prefabs.GetSlider("Scale");
            scaleSlider.maxValue = 3;
            scaleSlider.value = Mathf.Sqrt(transform.localScale.x);
            scaleSlider.onValueChanged.AddListener((x) => { x = x * x; transform.localScale = new Vector3(x, x, x); });
            if (gameObject.GetComponent<MeshRenderer>() != null)
                prefabs.GetToggle("MeshRenderer On").onValueChanged.AddListener((x) => { var renderer = GetComponent<Renderer>(); if (renderer != null) renderer.enabled = x; });
        }
    }
}