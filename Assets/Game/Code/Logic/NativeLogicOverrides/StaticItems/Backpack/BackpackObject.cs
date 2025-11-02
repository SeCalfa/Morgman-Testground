using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Backpack
{
    public class BackpackObject : MonoBehaviour
    {
        public void UseObject()
        {
            StaticInventoryManager.Instance.AddBackpack();
        }
    }
}
