using System.Collections.Generic;
using HFPS.Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Code.Logic.NativeLogicOverrides.BindPanel
{
    public class BindButton : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image logo;

        private RectTransform _logoRectTransform;
        private Canvas _canvas;
        private Inventory.ShortcutModel _shortcutModel;

        public Inventory.ShortcutModel ShortcutModel => _shortcutModel;

        public static bool IsDrag;

        private void Awake()
        {
            _logoRectTransform = logo.GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
        }

        public void UpdateLogo(Inventory.ShortcutModel shortcutModel)
        {
            _shortcutModel = shortcutModel;

            if (_shortcutModel != null)
            {
                print(shortcutModel.shortcut + " | not empty");
                logo.sprite = _shortcutModel.item.ItemSprite;
                logo.gameObject.SetActive(true);
            }
            else
            {
                print("empty");
                logo.gameObject.SetActive(false);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && _shortcutModel != null)
            {
                // BindPanelManager.Instance.SetSelectedShortcut(_shortcutModel, logo);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && _shortcutModel != null)
            {
                logo.transform.SetParent(BindPanelManager.Instance.transform);
                BindPanelManager.Instance.SetSelectedShortcut(_shortcutModel);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && _shortcutModel != null)
            {
                _logoRectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            
            foreach (var result in results)
            {
                if (eventData.button == PointerEventData.InputButton.Left && _shortcutModel != null)
                {
                    var button = result.gameObject.GetComponent<BindButton>();

                    if (button != null && button != this)
                    {
                        // Shortcut replaced
                        BindPanelManager.Instance.SetSelectedShortcut(button.ShortcutModel);
                    }
                    
                    logo.transform.SetParent(transform);
                    logo.transform.localPosition = Vector3.zero;
                }
            }
        }
    }
}
