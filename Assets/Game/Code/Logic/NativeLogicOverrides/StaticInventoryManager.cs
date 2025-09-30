using System.Linq;
using System.Collections.Generic;
using Game.Code.Logic.NativeLogicOverrides.StaticItems;
using Game.Code.Logic.NativeLogicOverrides.StaticItems.Flashlight;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides
{
    public class StaticInventoryManager : MonoBehaviour
    {
        [SerializeField] private StaticInventoryPresenter staticInventoryPresenter;
        [Space]
        [Header("Flashlight")]
        [SerializeField] private GameObject flashlight;
        [SerializeField] private FlashlightPresenter flashlightPanel;
        [Tooltip("Amount of battery power used per second")]
        [SerializeField] private float powerPercentPerSecond;
        [SerializeField] private float batteryPower;
        
        public static StaticInventoryManager Instance;

        private readonly List<IInventoryItem> _inventoryItems = new();
        
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
        }

        private void Update()
        {
            _inventoryItems.ForEach(i => i.Update());
        }

        public void AddFlashlight()
        {
            var flashlightItem = new FlashlightItem();
            flashlightItem.Construct(staticInventoryPresenter, flashlight, flashlightPanel, powerPercentPerSecond);
            
            _inventoryItems.Add(flashlightItem);
            
            staticInventoryPresenter.AddFlashlight(flashlightItem.ActiveBatteries);
        }

        public void AddFlashlightBattery()
        {
            var flashlightItem = _inventoryItems.OfType<FlashlightItem>().FirstOrDefault();
            flashlightItem?.AddBattery();
            
            staticInventoryPresenter.UpdateBatteries(flashlightItem!.ActiveBatteries);
        }
    }
}
