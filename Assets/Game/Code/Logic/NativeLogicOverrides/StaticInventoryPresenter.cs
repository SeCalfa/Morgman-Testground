using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.Logic.NativeLogicOverrides
{
    public class StaticInventoryPresenter : MonoBehaviour
    {
        [Header("Flashlight")]
        [SerializeField] private GameObject flashlightItem;
        [SerializeField] private GameObject flashlightFillFrame;
        [SerializeField] private GameObject flashlightEmptyFrame;
        [SerializeField] private GameObject flashlightLogo;
        [SerializeField] private Text flashlightBatteries;
        [Header("Health")]
        [SerializeField] private GameObject healthItem;
        [SerializeField] private GameObject healthFillFrame;
        [SerializeField] private GameObject healthEmptyFrame;
        [SerializeField] private GameObject healthLogo;
        [SerializeField] private Text healthPoints;
        [Header("Stamina")]
        [SerializeField] private GameObject staminaItem;
        [SerializeField] private GameObject staminaFillFrame;
        [SerializeField] private GameObject staminaEmptyFrame;
        [SerializeField] private GameObject staminaLogo;
        [SerializeField] private Text staminaPoints;
        [Header("Damage")]
        [SerializeField] private GameObject damageItem;
        [SerializeField] private GameObject damageFillFrame;
        [SerializeField] private GameObject damageEmptyFrame;
        [SerializeField] private GameObject damageLogo;
        [SerializeField] private Text damagePoints;
        [Header("Backpack")]
        [SerializeField] private GameObject backpackItem;
        [SerializeField] private GameObject backpackFillFrame;
        [SerializeField] private GameObject backpackEmptyFrame;
        [SerializeField] private GameObject backpackLogo;
        [SerializeField] private Text backpackPoints;

        private void Start()
        {
            StaticInventoryManager.Instance.Construct(
                flashlightItem,
                healthItem,
                staminaItem,
                damageItem,
                backpackItem);
        }

        public void AddFlashlight(int batteries)
        {
            flashlightFillFrame.SetActive(true);
            flashlightEmptyFrame.SetActive(false);
            flashlightLogo.SetActive(true);

            flashlightBatteries.text = batteries.ToString();
        }
        
        public void AddHealth(int batteries)
        {
            healthFillFrame.SetActive(true);
            healthEmptyFrame.SetActive(false);
            healthLogo.SetActive(true);

            healthPoints.text = batteries.ToString();
        }
        
        public void AddStamina(int batteries)
        {
            staminaFillFrame.SetActive(true);
            staminaEmptyFrame.SetActive(false);
            staminaLogo.SetActive(true);

            staminaPoints.text = batteries.ToString();
        }
        
        public void AddDamage(int batteries)
        {
            damageFillFrame.SetActive(true);
            damageEmptyFrame.SetActive(false);
            damageLogo.SetActive(true);

            damagePoints.text = batteries.ToString();
        }
        
        public void AddBackpack(int batteries)
        {
            backpackFillFrame.SetActive(true);
            backpackEmptyFrame.SetActive(false);
            backpackLogo.SetActive(true);

            backpackPoints.text = batteries.ToString();
        }

        public void UpdateBatteries(int batteries)
        {
            flashlightBatteries.text = batteries.ToString();
        }
        
        public void UpdateHealth(int batteries)
        {
            healthPoints.text = batteries.ToString();
        }
        
        public void UpdateStamina(int batteries)
        {
            staminaPoints.text = batteries.ToString();
        }
        
        public void UpdateDamage(int batteries)
        {
            damagePoints.text = batteries.ToString();
        }
        
        public void UpdateBackpack(int batteries)
        {
            backpackPoints.text = batteries.ToString();
        }
    }
}
