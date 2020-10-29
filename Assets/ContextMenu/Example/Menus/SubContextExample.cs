using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z.ContextMenu;

public class SubContextExample : MonoBehaviour, IContextMenu
{
    public void BuildContextMenu(PrefabProviderTool prefabs)
    {
        prefabs.GetLabel("Hello","Header");
        prefabs.GetButton("Hello");
        prefabs.GetButton("Nested");
        prefabs.GetButton("Context");
        prefabs.GetButton("Menus");
        prefabs.GetSlider("Slider");
    }

}