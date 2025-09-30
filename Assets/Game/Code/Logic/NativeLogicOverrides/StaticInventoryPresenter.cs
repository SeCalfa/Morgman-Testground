using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.Logic.NativeLogicOverrides
{
    public class StaticInventoryPresenter : MonoBehaviour
    {
        [Header("Flashlight")]
        [SerializeField] private GameObject flashlightFillFrame;
        [SerializeField] private GameObject flashlightEmptyFrame;
        [SerializeField] private GameObject flashlightLogo;
        [SerializeField] private Text flashlightBatteries;

        public void AddFlashlight(int batteries)
        {
            flashlightFillFrame.SetActive(true);
            flashlightEmptyFrame.SetActive(false);
            flashlightLogo.SetActive(true);

            flashlightBatteries.text = batteries.ToString();
        }

        public void UpdateBatteries(int batteries)
        {
            flashlightBatteries.text = batteries.ToString();
        }
        
        public void RemoveFlashlight()
        {
            flashlightFillFrame.SetActive(false);
            flashlightEmptyFrame.SetActive(true);
            flashlightLogo.SetActive(false);
        }
    }
}
