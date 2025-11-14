using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Item
{
    public class StaticItemRenderer : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text countText;
        [Space]
        [SerializeField] private Sprite backpack;
        [SerializeField] private Sprite flashlight;
        [SerializeField] private Sprite damage;
        [SerializeField] private Sprite health;
        [SerializeField] private Sprite stamina;
        
        private StaticItemType _staticItemType;

        public void Construct(StaticItemType staticItemType, int count)
        {
            _staticItemType = staticItemType;
            
            RenderItem(count);
        }

        public void RenderItem(int count)
        {
            switch (_staticItemType)
            {
                case StaticItemType.Backpack:
                    image.sprite = backpack;
                    break;
                case StaticItemType.Flashlight:
                    image.sprite = flashlight;
                    break;
                case StaticItemType.Damage:
                    image.sprite = damage;
                    break;
                case StaticItemType.Health:
                    image.sprite = health;
                    break;
                case StaticItemType.Stamina:
                    image.sprite = stamina;
                    break;
            }
            
            countText.text = count.ToString();
        }
    }
}