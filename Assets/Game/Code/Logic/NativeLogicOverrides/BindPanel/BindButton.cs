using System.Collections.Generic;
using HFPS.Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Code.Logic.NativeLogicOverrides.BindPanel
{
    public class BindButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private int slotId;
        [SerializeField] private string control;
        [Space]
        [SerializeField] private Image logo;

        private RectTransform _logoRectTransform;
        private Canvas _canvas;
        private Inventory.ShortcutModel _shortcutModel;

        private void Awake()
        {
            _logoRectTransform = logo.GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
        }

        public void UpdateLogo(Inventory.ShortcutModel shortcutModel)
        {
            _shortcutModel = shortcutModel;

            if (shortcutModel != null)
            {
                logo.gameObject.SetActive(true);
                logo.sprite = shortcutModel.item.ItemSprite;
            }
            else
            {
                logo.gameObject.SetActive(false);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && _shortcutModel != null)
            {
                logo.transform.SetParent(BindPanelManager.Instance.transform);
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

            if (eventData.button == PointerEventData.InputButton.Left && _shortcutModel != null)
            {
                var button = results[0].gameObject.GetComponent<BindButton>();

                if (button == null || button == this)
                {
                    // Target is on empty place or the same shortcut
                } 
                else if (button._shortcutModel == null)
                {
                    // Shortcut on empty slot
                    
                    var itemData = Inventory.Instance.ItemDataOfSlot(_shortcutModel.slot);
                    Inventory.Instance.ShortcutBind(itemData.itemID, itemData.slotID, button.control);
                }
                else if (button._shortcutModel != null)
                {
                    // Shortcut replaced

                    var targetShortcut = Inventory.Instance.Shortcuts.Find(s => s.shortcut == button.control);
                    
                    var currentItemData = Inventory.Instance.ItemDataOfSlot(_shortcutModel.slot);
                    var targetItemData = Inventory.Instance.ItemDataOfSlot(targetShortcut.slot);

                    var currentItemId = currentItemData.itemID;
                    var currentSlotId = currentItemData.slotID;
                    var targetItemId = targetItemData.itemID;
                    var targetSlotId = targetItemData.slotID;

                    Inventory.Instance.ShortcutBind(currentItemId, currentSlotId, button.control);
                    Inventory.Instance.ShortcutBind(targetItemId, targetSlotId, control);
                }

                logo.transform.SetParent(transform);
                logo.transform.localPosition = Vector3.zero;
                
                BindPanelManager.Instance.UpdateBind();
            }
        }
    }
}
