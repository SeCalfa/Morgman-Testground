using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Stamina
{
    public class StaminaObject : MonoBehaviour
    {
        public void UseObject()
        {
            StaticInventoryManager.Instance.AddStamina();
        }
    }
}
