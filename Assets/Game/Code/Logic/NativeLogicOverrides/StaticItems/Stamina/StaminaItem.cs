using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Stamina
{
    public class StaminaItem : IInventoryItem
    {
        public int Count { get; set; } = 1;
        
        public StaminaItem(GameObject item)
        {
            item.SetActive(true);
        }
        
        public void Update()
        {
            
        }
    }
}