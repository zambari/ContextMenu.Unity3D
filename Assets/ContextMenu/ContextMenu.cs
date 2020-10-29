using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Z;
// v.0.03 compat
// v.0.04 interfaces for prefabs
namespace Z.ContextMenu
{
    public class ContextMenu : MonoBehaviour
    {

        public PrefabProviderBase provider;
        public Color blockerColor = new Color(0, 0, 0, 0.4f);
        RectTransform lastFilledMenu;
        int menucount = 0;
        Stack<GameObject> menus = new Stack<GameObject>();
        Stack<CanvasGroup> canvasGroups = new Stack<CanvasGroup>();
        CanvasGroup previousGroup;
        List<RaycastResult> raycastList;

        [Range(0.1f, 1)]
        public float inactiveAlpha = 0.3f;

        void Reset()
        {
            provider = gameObject.GetComponent<PrefabProviderBase>();
            if (provider == null)
                provider = gameObject.AddComponent<SimplePrefabProvider>();
            var rect = GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;
            name = "ContextMenu";
            transform.SetAsLastSibling();
        }
        void PrepareBlocker(GameObject blocker)
        {
            var rect = blocker.GetComponent<RectTransform>();
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = new Vector2(0, 1);
            rect.anchoredPosition = Vector2.zero;

        }
        public void MovePanel(RectTransform panelRect)
        {
            var pivot = panelRect.pivot;
            var blockerRect = panelRect.transform.parent.GetComponent<RectTransform>().rect;
            float normalizedX = Input.mousePosition.x / (Screen.width); //' + rect.rect.width);
            float normalizedY = Input.mousePosition.y / (Screen.height); // + rect.rect.height);
            panelRect.position = new Vector3(normalizedX * blockerRect.width, normalizedY * blockerRect.height);
            pivot.y = normalizedY;
            float normalizedX2 = Input.mousePosition.x / (Screen.width - panelRect.rect.width);
            if (normalizedX2 >.7f)
            {
                pivot.x = 1;
            }
            panelRect.pivot = pivot;
        }

        public PrefabProviderTool GetMenu()
        {
            var contextBlocker = new GameObject("blocker " + (menucount++));
            contextBlocker.transform.SetParent(transform);
            contextBlocker.AddComponent<Image>().color = blockerColor;
            Push(contextBlocker);
            contextBlocker.AddComponent<Button>().onClick.AddListener(Pop); //() => Destroy(contextBlocker)
            PrepareBlocker(contextBlocker);
            var thispanel = provider.GetPanel(contextBlocker.transform, "ContextMenu");
            lastFilledMenu = thispanel.GetComponent<RectTransform>();
            thispanel.name = "ContextMenu";
            return provider.GetPrefabTool(this, thispanel.transform);
        }

        public List<RaycastResult> RaycastMouse()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1,
            };
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            return results;
        }
        void RestoreGroup(CanvasGroup g)
        {
            g.alpha = 1;
            g.interactable = true;
        }

        void Push(GameObject g)
        {
            if (previousGroup != null)
            {
                previousGroup.alpha = inactiveAlpha;
                previousGroup.interactable = false;
            }
            CanvasGroup cg = g.AddComponent<CanvasGroup>();
            canvasGroups.Push(cg);
            previousGroup = cg;
            menus.Push(g);
        }
        void Pop()
        {
            if (canvasGroups.Count > 0)
            {
                previousGroup = canvasGroups.Pop();
                RestoreGroup(previousGroup);
            }
            if (menus.Count > 0)
            {
                Destroy(menus.Pop());
            }
            if (canvasGroups.Count > 0)
            {
                previousGroup = canvasGroups.Pop();
                RestoreGroup(previousGroup);
            }
        }
        List<IContextMenu> GetContextRequiring(List<RaycastResult> raycastList)
        {
            var list = new List<IContextMenu>();
            foreach (var i in raycastList)
            {
                list.AddRange(i.gameObject.GetComponents<IContextMenu>());

            }
            return list;
        }
        bool CheckIfImplementsContextMenu(List<IContextMenu> menus)
        {
            if (menus.Count == 0) return false;
            return true;
        }
        void OnMouseReleased()
        {
            List<IContextMenu> builders = GetContextRequiring(raycastList);
            if (!CheckIfImplementsContextMenu(builders)) return;
            var thisMenu = GetMenu();
            //   thisMenu.GetButton("[X] Close").AddCallback(Pop);
            foreach (var b in builders)
            {
                //   thisMenu.GetLabel($"{b.name} ( {b.GetType().ToString()}) ");
                b.BuildContextMenu(thisMenu);
            }
            MovePanel(lastFilledMenu);
        }

        bool CheckIfObjectIsContexAndWantsContext()
        {
            if (raycastList.Count == 0) return false;
            var firstobject = raycastList[0].gameObject;
            if (firstobject.GetComponentInParent<ContextMenu>() == this)
            {
                if (firstobject.GetComponentInParent<IContextMenu>() != null)
                    return true;
            }
            return false;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                raycastList = RaycastMouse();
                if (menus.Count > 0 && !CheckIfObjectIsContexAndWantsContext())
                    Pop();
                else
                    OnMouseReleased();
            }
        }

    }
}