using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Flashlight
{
    public class BatteryObject : MonoBehaviour
    {
        public void UseObject()
        {
            StaticInventoryManager.Instance.AddFlashlightBattery(1);
        }
    }
}
