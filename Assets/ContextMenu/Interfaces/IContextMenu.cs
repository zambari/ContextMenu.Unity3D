using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Z.ContextMenu;
// {
    public interface IContextMenu
    {
        // void RequestMenu(out List<string> labels, out List<UnityAction> callbacks);
        void BuildContextMenu(PrefabProviderTool prefabs);
        GameObject gameObject { get; }
        string name { get; }
        Transform transform { get; }
    }
// }