using System;
using System.Linq;
using HFPS.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.Logic.NativeLogicOverrides.BindPanel
{
    public class BindPanelManager : MonoBehaviour
    {
        [SerializeField] private Transform dragParent;
        [SerializeField] private BindButton[] bindButtons;

        private Inventory.ShortcutModel _selectedShortcut;

        public Transform DragParent => dragParent;
        
        public static BindPanelManager Instance;
        
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }

            Inventory.Instance.OnAutoBindShortcut += UpdateBind;

            // ToggleActive(false);
        }

        private void Update()
        {
            // Inventory.Instance.Shortcuts.ForEach(s => print($"{s.shortcut}"));
            print("Shortcuts: " + Inventory.Instance.Shortcuts.Count);
        }

        public void SetSelectedShortcut(Inventory.ShortcutModel selectedShortcut)
        {
            if (_selectedShortcut == null)
            {
                // New slot pressed
                _selectedShortcut = selectedShortcut;
            }
            else if (_selectedShortcut == selectedShortcut)
            {
                // The same slot pressed
                _selectedShortcut = null;
            }
            else
            {
                // New slot while active slot exist pressed
                var selectedSlotId = _selectedShortcut.slot;
                var selectedControl = _selectedShortcut.shortcut;
                
                var targetSlotId = selectedShortcut.slot;
                var targetControl = selectedShortcut.shortcut;
                
                Inventory.Instance.UpdateShortcut(selectedControl, targetSlotId);
                Inventory.Instance.UpdateShortcut(targetControl, selectedSlotId);
                
                _selectedShortcut = null;
                
                UpdateBind();
            }
        }

        public void RemoveShortcut(int slotId)
        {
            var shortcut = Inventory.Instance.Shortcuts.FirstOrDefault(s => s.slot == slotId);

            if (shortcut != null)
            {
                Inventory.Instance.Shortcuts.Remove(shortcut);
            }
        }
        
        public void UpdateBind()
        {
            // HERE
            print("Shortcuts: " + Inventory.Instance.Shortcuts.Count);
            var sortedShortcut = Inventory.Instance.Shortcuts.OrderBy(s => s.slot).ToList();
            for (var index = 0; index < bindButtons.Length; index++)
            {
                if (index >= sortedShortcut.Count)
                {
                    bindButtons[index].UpdateLogo(null);
                    continue;
                }
                // var shortcutName = $"UseItem{index + 1}";
                var shortcutName = sortedShortcut[index].shortcut;
                var shortcut = sortedShortcut.Find(s => s.shortcut == shortcutName);
                
                bindButtons[index].UpdateLogo(shortcut);
            }
            
            // var shortcuts = Inventory.Instance.Shortcuts.OrderBy(s => s.shortcut).ToList();
            // for (var index = 0; index < shortcuts.Count; index++)
            // {
            //     if (index == bindButtons.Length)
            //     {
            //         break;
            //     }
            //
            //     // Привязка к списку
            //     var shortcutModel = shortcuts[index];
            //     bindButtons[index].UpdateLogo(shortcutModel);
            // }
        }

        // public void ToggleActive(bool toggle)
        // {
        //     gameObject.SetActive(toggle);
        // }
    }
}
