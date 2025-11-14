using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Health
{
    public class HealthItem : IInventoryItem
    {
        public int Count { get; set; } = 1;

        public void Update()
        {
            
        }
    }
}
