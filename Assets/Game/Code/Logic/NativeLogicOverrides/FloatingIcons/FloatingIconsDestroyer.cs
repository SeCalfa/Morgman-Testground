using System.Collections;
using HFPS.Systems;
using ThunderWire.Utility;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.FloatingIcons
{
    public class FloatingIconsDestroyer : MonoBehaviour
    {
        public void UseObject()
        {
            StartCoroutine(DestroyFloatingIcon());
        }

        private IEnumerator DestroyFloatingIcon()
        {
            yield return 0;
            
            FloatingIconManager.Instance.DestroySafely(gameObject);
        }

        private void AddFloatingIcon()
        {
            var uIFloatingItem = FindObjectOfType<FloatingIconManager>();

            if (uIFloatingItem)
            {
                if (gameObject.HasComponent(out InteractiveItem interactiveItem))
                {
                    interactiveItem.floatingIcon = true;
                }

                if (!uIFloatingItem.FloatingIcons.Contains(gameObject))
                {
                    uIFloatingItem.FloatingIcons.Add(gameObject);
                }
            }
        }
    }
}
