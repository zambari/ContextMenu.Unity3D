using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z.ContextMenu
{
    public class PrefabProviderTool 
    {
        // Use this for initialization
        public IProvidePrefabs provider;
        public Transform target { get; set; }
        public MonoBehaviour source;
    }
}
