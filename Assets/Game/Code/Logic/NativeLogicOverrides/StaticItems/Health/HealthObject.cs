using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Health
{
    public class HealthObject : MonoBehaviour
    {
        public void UseObject()
        {
            StaticInventoryManager.Instance.AddHealth();
        }
    }
}
