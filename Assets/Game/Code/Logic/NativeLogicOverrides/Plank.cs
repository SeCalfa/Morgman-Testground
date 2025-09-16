using HFPS.Systems;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides
{
    public class Plank : MonoBehaviour
    {
        private DynamicObjectPlank _dynamicObjectPlank;
        
        private void Awake()
        {
            _dynamicObjectPlank = GetComponent<DynamicObjectPlank>();
        }

        public void ApplyDamage(int amount)
        {
            _dynamicObjectPlank.UseObject();
        }
    }
}
