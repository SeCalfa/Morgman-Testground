using System.Linq;
using HFPS.Systems;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.BindPanel
{
    public class BindPanelManager : MonoBehaviour
    {
        [SerializeField] private Transform dragParent;
        [SerializeField] private BindButton[] bindButtons;

        private Inventory.ShortcutModel _selectedShortcut;

        public static BindPanelManager Instance;
        
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }

            Inventory.Instance.OnAutoBindShortcut += UpdateBind;
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
            var shortcut1 = Inventory.Instance.Shortcuts.FirstOrDefault(shortcut => shortcut.shortcut == "UseItem1");
            var shortcut2 = Inventory.Instance.Shortcuts.FirstOrDefault(shortcut => shortcut.shortcut == "UseItem2");
            var shortcut3 = Inventory.Instance.Shortcuts.FirstOrDefault(shortcut => shortcut.shortcut == "UseItem3");
            var shortcut4 = Inventory.Instance.Shortcuts.FirstOrDefault(shortcut => shortcut.shortcut == "UseItem4");

            bindButtons[0].UpdateLogo(shortcut1);
            bindButtons[1].UpdateLogo(shortcut2);
            bindButtons[2].UpdateLogo(shortcut3);
            bindButtons[3].UpdateLogo(shortcut4);
        }
    }
}
