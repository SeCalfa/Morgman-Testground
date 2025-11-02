using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Damage
{
    public class DamageObject : MonoBehaviour
    {
        public void UseObject()
        {
            StaticInventoryManager.Instance.AddDamage();
        }
    }
}
