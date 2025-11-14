using System.Collections.Generic;
using Game.Code.Logic.NativeLogicOverrides.StaticItems.Item;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides
{
    public class StaticItem
    {
        public StaticItemRenderer StaticItemRenderer { get; set; }
        public StaticItemType ItemType { get; set; }
        public int ItemCount { get; set; }
    }
    
    public class StaticInventoryPresenter : MonoBehaviour
    {
        [Space]
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Transform itemContainer;

        private readonly List<StaticItem> _items = new();

        public int AddItem(StaticItemType itemType, int itemCount = 1)
        {
            var itemExists = ItemExists(itemType);

            if (itemExists)
            {
                var existingItem = _items.Find(i => i.ItemType == itemType);
                AddItemCount(existingItem);

                return existingItem.ItemCount;
            }

            var item = SpawnNewItem(itemType, itemCount);
            _items.Add(item);

            return item.ItemCount;
        }

        private bool ItemExists(StaticItemType itemType)
        {
            var existingItem = _items.Find(i => i.ItemType == itemType);
            
            return existingItem != null;
        }

        private void AddItemCount(StaticItem itemToAdd)
        {
            itemToAdd.StaticItemRenderer.RenderItem(++itemToAdd.ItemCount);
        }
        
        public void UseBattery()
        {
            var flashlightItem = _items.Find(i => i.ItemType == StaticItemType.Flashlight);
            
            flashlightItem.StaticItemRenderer.RenderItem(--flashlightItem.ItemCount);
        }

        private StaticItem SpawnNewItem(StaticItemType itemType, int itemCount)
        {
            var spawnedItem = Instantiate(itemPrefab, itemContainer);
            var staticItemRenderer = spawnedItem.GetComponent<StaticItemRenderer>();

            spawnedItem.name = itemType.ToString();
            staticItemRenderer.Construct(itemType, itemCount);

            var staticItem = new StaticItem
            {
                ItemType = itemType,
                ItemCount = itemCount,
                StaticItemRenderer = spawnedItem.GetComponent<StaticItemRenderer>()
            };
            
            return staticItem;
        }
    }
}
