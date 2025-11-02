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

        private GameObject _flashlightItem;
        private GameObject _healthItem;
        private GameObject _staminaItem;
        private GameObject _damageItem;
        private GameObject _backpackItem;
        
        public static StaticInventoryManager Instance;

        private readonly List<IInventoryItem> _inventoryItems = new();

        public void Construct(GameObject flashlightItem, GameObject healthItem, GameObject staminaItem, GameObject damageItem, GameObject backpackItem)
        {
            _flashlightItem = flashlightItem;
            _healthItem = healthItem;
            _staminaItem = staminaItem;
            _damageItem = damageItem;
            _backpackItem = backpackItem;
        }
        
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
            
            var flashlightItem = new FlashlightItem(_flashlightItem);
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
        
        public void AddHealth()
        {
            var healthItem = _inventoryItems.OfType<HealthItem>().FirstOrDefault();
            if (healthItem == null)
            {
                // First health
                var health = new HealthItem(_healthItem);
                _inventoryItems.Add(health);
                
                staticInventoryPresenter.AddHealth(1);
            }
            else
            {
                staticInventoryPresenter.UpdateHealth(++healthItem.Count);
            }
            
            GetComponent<HealthManager>().maximumHealth += 25f;
        }
        
        public void AddBackpack()
        {
            var backpackItem = _inventoryItems.OfType<BackpackItem>().FirstOrDefault();
            if (backpackItem == null)
            {
                // First backpack
                var backpack = new BackpackItem(_backpackItem);
                _inventoryItems.Add(backpack);
                
                staticInventoryPresenter.AddBackpack(1);
            }
            else
            {
                staticInventoryPresenter.UpdateBackpack(++backpackItem.Count);
            }
            
            Inventory.Instance.ExpandSlots(2);
        }
        
        public void AddStamina()
        {
            var staminaItem = _inventoryItems.OfType<StaminaItem>().FirstOrDefault();
            if (staminaItem == null)
            {
                // First stamina
                var stamina = new StaminaItem(_staminaItem);
                _inventoryItems.Add(stamina);
                
                staticInventoryPresenter.AddStamina(1);
            }
            else
            {
                staticInventoryPresenter.UpdateStamina(++staminaItem.Count);
            }

            PlayerController.Instance.staminaSettings.staminaRegenSpeed += 0.01f;
        }
        
        public void AddDamage()
        {
            var damageItem = _inventoryItems.OfType<DamageItem>().FirstOrDefault();
            if (damageItem == null)
            {
                // First stamina
                var damage = new DamageItem(_damageItem);
                damage.Construct(axe, pistol, shotgun);
                damageItem = damage;
                
                _inventoryItems.Add(damage);
                
                staticInventoryPresenter.AddDamage(1);
            }
            else
            {
                staticInventoryPresenter.UpdateDamage(++damageItem.Count);
            }

            damageItem.UpdateDamage();
        }
    }
}
