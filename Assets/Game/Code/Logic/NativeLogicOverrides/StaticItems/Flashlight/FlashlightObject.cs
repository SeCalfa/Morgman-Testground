using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Flashlight
{
    public class FlashlightObject : MonoBehaviour
    {
        public void UseObject()
        {
            StaticInventoryManager.Instance.AddFlashlight();
        }
    }
}
