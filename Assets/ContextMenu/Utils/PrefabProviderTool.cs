using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z.ContextMenu;

public class PrefabProxy
{
    // Use this for initialization
    public IProvidePrefabs provider;
    public Transform target { get; set; }
    public MonoBehaviour source;
}