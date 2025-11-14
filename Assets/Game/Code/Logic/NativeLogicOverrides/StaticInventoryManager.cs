using System.Linq;
using System.Collections.Generic;
using Game.Code.Logic.NativeLogicOverrides.StaticItems;
using Game.Code.Logic.NativeLogicOverrides.StaticItems.Backpack;
using Game.Code.Logic.NativeLogicOverrides.StaticItems.Damage;
using Game.Code.Logic.NativeLogicOverrides.StaticItems.Flashlight;
using Game.Code.Logic.NativeLogicOverrides.StaticItems.Health;
using Game.Code.Logic.NativeLogicOverrides.StaticItems.Stamina;
using HFPS.Player;
using HFPS.Systems;
using UnityEngine;
using FlashlightItem = Game.Code.Logic.NativeLogicOverrides.StaticItems.Flashlight.FlashlightItem;

namespace Game.Code.Logic.NativeLogicOverrides
{
    public class StaticInventoryManager : MonoBehaviour
    {
        [SerializeField] private StaticInventoryPresenter staticInventoryPresenter;
        [Space]
        [Header("Flashlight")]
        [SerializeField] private NewFlashlight flashlight;
        [SerializeField] private FlashlightPresenter flashlightPanel;
        [Tooltip("Amount of battery power used per second")]
        [SerializeField] private float powerPercentPerSecond;
        [SerializeField] private float batteryPower;
        [Range(0, 100)]
        [SerializeField] private float flashlightPercent;

        [Header("Damage")]
        [SerializeField] private MeleeController axe;
        [SerializeField] private WeaponController pistol;
        [SerializeField] private WeaponController shotgun;
        
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
            flashlight.Construct(flashlightPercent);
            
            var flashlightItem = new FlashlightItem();
            flashlightItem.Construct(staticInventoryPresenter, flashlight, flashlightPanel, powerPercentPerSecond);
            
            _inventoryItems.Add(flashlightItem);
            
            staticInventoryPresenter.AddItem(StaticItemType.Flashlight);
        }

        public void AddFlashlightBattery()
        {
            var flashlightItem = _inventoryItems.OfType<FlashlightItem>().FirstOrDefault();
            flashlightItem?.AddBattery();
            
            staticInventoryPresenter.AddItem(StaticItemType.Flashlight);
        }
        
        public void AddHealth()
        {
            var healthItem = _inventoryItems.OfType<HealthItem>().FirstOrDefault();
            if (healthItem == null)
            {
                // First health
                // var health = new HealthItem(_healthItem);
                // _inventoryItems.Add(health);
            }
            
            staticInventoryPresenter.AddItem(StaticItemType.Health);
            GetComponent<HealthManager>().maximumHealth += 25f;
        }
        
        public void AddBackpack()
        {
            var backpackItem = _inventoryItems.OfType<BackpackItem>().FirstOrDefault();
            if (backpackItem == null)
            {
                // First backpack
                // var backpack = new BackpackItem(_backpackItem);
                // _inventoryItems.Add(backpack);
            }
            
            staticInventoryPresenter.AddItem(StaticItemType.Backpack);
            Inventory.Instance.ExpandSlots(2);
        }
        
        public void AddStamina()
        {
            var staminaItem = _inventoryItems.OfType<StaminaItem>().FirstOrDefault();
            if (staminaItem == null)
            {
                // First stamina
                // var stamina = new StaminaItem(_staminaItem);
                // _inventoryItems.Add(stamina);
            }

            staticInventoryPresenter.AddItem(StaticItemType.Stamina);
            PlayerController.Instance.staminaSettings.staminaRegenSpeed += 0.01f;
        }
        
        public void AddDamage()
        {
            var damageItem = _inventoryItems.OfType<DamageItem>().FirstOrDefault();
            if (damageItem == null)
            {
                // First damage
                var damage = new DamageItem();
                damage.Construct(axe, pistol, shotgun);
                damageItem = damage;
                
                _inventoryItems.Add(damage);
            }

            var itemCount = staticInventoryPresenter.AddItem(StaticItemType.Damage);
            damageItem.UpdateDamage(itemCount);
        }
    }
}
