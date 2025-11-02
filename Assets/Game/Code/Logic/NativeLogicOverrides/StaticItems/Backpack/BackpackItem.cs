using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Backpack
{
    public class BackpackItem : IInventoryItem
    {
        public int Count { get; set; } = 1;

        public BackpackItem(GameObject item)
        {
            item.SetActive(true);
        }

        public void Update()
        {
            
        }
    }
}
