using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Backpack
{
    public class BackpackItem : IInventoryItem
    {
        public int Count { get; set; } = 1;

        public void Update()
        {
            
        }
    }
}
