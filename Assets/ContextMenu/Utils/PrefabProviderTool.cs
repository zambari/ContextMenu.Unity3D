using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z.ContextMenu
{
    public class PrefabProviderTool : IPointToTargetRect, IProvidePrefabs
    {
        // Use this for initialization
        public IProvidePrefabs provider;
        public Transform target { get; set; }
        public MonoBehaviour source;

        public T InstantiatePrefab<T>(Transform target, string label, string style = null) where T : Component
        {
            return provider.InstantiatePrefab<T>(target, label, style);
        }

        public T InstantiatePrefab<T>(string label, string style = null) where T : Component
        {
            return provider.InstantiatePrefab<T>(target, label, style);
        }

        // public Button GetButton(string label, string style=null)
        // {
        //    return  Instantiate<Button>(provider.GetButtonPrefab(style),label);
        // }

        // public Slider GetSlider(string label, string style=null)
        // {
        //    return  Instantiate<Slider>(provider.GetSliderPrefab(style),label);
        // }

        // public Toggle GetToggle(string label, string style)
        // {
        //   return  Instantiate<Toggle>(provider.GetTogglePrefab(style),label);
        // }

        // public Text GetLabel(string label, string style)
        // {
        //    return  Instantiate<Text>(provider.GetLabelPrefab(style),label);
        // }

        // public SubPanel GetSubPanel(string label, string style)
        // {
        // 	  return  Instantiate<SubPanel>(provider.SubPanelPrefab(style),label);

        // }

        //   public SubPanel GetSubPanel(string label, string style)
        // {
        // 	  return  Instantiate<SubPanel>(provider.SubPanel(label,style));

        // }
    }
}
