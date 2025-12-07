using System.Linq;
using System.Collections.Generic;
using Game.Code.Logic.NativeLogicOverrides.StaticItems;
using Game.Code.Logic.NativeLogicOverrides.StaticItems.Damage;
using Game.Code.Logic.NativeLogicOverrides.StaticItems.Flashlight;
using HFPS.Player;
using HFPS.Systems;
using Newtonsoft.Json.Linq;
using UnityEngine;
using FlashlightItem = Game.Code.Logic.NativeLogicOverrides.StaticItems.Flashlight.FlashlightItem;

namespace Game.Code.Logic.NativeLogicOverrides
{
    public class StaticInventoryManager : MonoBehaviour, ISaveable
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

        private float _currentHealth = 100f;
        private int _healthCount, _backpackCount, _staminaCount, _damageCount, _batteryCount;
        private bool _flashlightExist;
        
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

        public void OnLoad(JToken token)
        {
            _currentHealth = token["currentHealth"].ToObject<float>();
            _healthCount = token["staticInventoryHealth"].ToObject<int>();
            _backpackCount = token["staticInventoryBackpack"].ToObject<int>();
            _staminaCount = token["staticInventoryStamina"].ToObject<int>();
            _damageCount = token["staticInventoryDamage"].ToObject<int>();
            _flashlightExist = token["staticInventoryFlashlight"].ToObject<bool>();
            _batteryCount = token["staticInventoryFlashlightBattery"].ToObject<int>();

            LoadBackpack();
            LoadFlashlight();
            LoadHealth();
            LoadStamina();
            LoadDamage();
        }

        public Dictionary<string, object> OnSave()
        {
            _currentHealth = GetComponent<HealthManager>().Health;
            
            return new Dictionary<string, object>
            {
                { "currentHealth", _currentHealth },
                { "staticInventoryHealth", _healthCount },
                { "staticInventoryBackpack", _backpackCount },
                { "staticInventoryStamina", _staminaCount },
                { "staticInventoryDamage", _damageCount },
                { "staticInventoryFlashlight", _flashlightExist },
                { "staticInventoryFlashlightBattery", _batteryCount },
            };
        }

        private void LoadHealth()
        {
            var currentHealth = _currentHealth;
            var healthCount = _healthCount;

            for (var i = 0; i < healthCount; i++)
            {
                AddHealth(0);
            }

            GetComponent<HealthManager>().Health = currentHealth;
        }

        private void LoadBackpack()
        {
            var backpackCount = _backpackCount;
            
            for (var i = 0; i < backpackCount; i++)
            {
                AddBackpack(0);
            }
        }

        private void LoadStamina()
        {
            var staminaCount = _staminaCount;
            
            for (var i = 0; i < staminaCount; i++)
            {
                AddStamina(0);
            }
        }

        private void LoadDamage()
        {
            var damageCount = _damageCount;
            
            for (var i = 0; i < damageCount; i++)
            {
                AddDamage(0);
            }
        }

        private void LoadFlashlight()
        {
            var flashlightExist = _flashlightExist;
            var batteryCount = _batteryCount;

            if (flashlightExist)
            {
                AddFlashlight();
            }
            
            for (var i = 0; i < batteryCount; i++)
            {
                AddFlashlightBattery(0);
            }
        }

        public void AddFlashlight()
        {
            flashlight.Construct(flashlightPercent);
            
            var flashlightItem = new FlashlightItem();
            flashlightItem.Construct(staticInventoryPresenter, flashlight, flashlightPanel, powerPercentPerSecond);
            
            _inventoryItems.Add(flashlightItem);
            
            staticInventoryPresenter.AddItem(StaticItemType.Flashlight);

            _flashlightExist = true;
        }

        public void AddFlashlightBattery(int counter)
        {
            var flashlightItem = _inventoryItems.OfType<FlashlightItem>().FirstOrDefault();
            flashlightItem?.AddBattery();
            
            staticInventoryPresenter.AddItem(StaticItemType.Flashlight);

            _batteryCount += counter;
        }
        
        public void AddHealth(int counter)
        {
            staticInventoryPresenter.AddItem(StaticItemType.Health);
            GetComponent<HealthManager>().maximumHealth += 25f;

            _healthCount += counter;
        }
        
        public void AddBackpack(int counter)
        {
            staticInventoryPresenter.AddItem(StaticItemType.Backpack);
            Inventory.Instance.ExpandSlots(2);

            _backpackCount += counter;
        }
        
        public void AddStamina(int counter)
        {
            staticInventoryPresenter.AddItem(StaticItemType.Stamina);
            PlayerController.Instance.staminaSettings.staminaRegenSpeed += 0.01f;

            _staminaCount += counter;
        }
        
        public void AddDamage(int counter)
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

            _damageCount += counter;
        }
    }
}
