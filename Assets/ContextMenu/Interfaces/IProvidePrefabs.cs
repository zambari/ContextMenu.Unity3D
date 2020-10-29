using UnityEngine;

namespace Z.ContextMenu
{
  public interface IProvidePrefabs
  {
    T InstantiatePrefab<T>(Transform target, string label, string style = null) where T : Component;
  }
}