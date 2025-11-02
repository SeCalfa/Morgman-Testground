using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Health
{
    public class HealthItem : IInventoryItem
    {
        public int Count { get; set; } = 1;
        
        public HealthItem(GameObject item)
        {
            item.SetActive(true);
        }

        public void Update()
        {
            
        }
    }
}
