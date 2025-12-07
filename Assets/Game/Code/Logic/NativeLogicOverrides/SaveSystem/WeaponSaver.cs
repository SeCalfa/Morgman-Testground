using System.Collections;
using System.Collections.Generic;
using Game.Code.Logic.NativeLogicOverrides.BindPanel;
using HFPS.Systems;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.SaveSystem
{
    public class WeaponSaver : MonoBehaviour, ISaveable
    {
        [SerializeField] private GameObject weapon1;
        [SerializeField] private GameObject weapon2;
        [SerializeField] private GameObject weapon3;
        
        private bool _weapon1Active, _weapon2Active, _weapon3Active;

        public void OnLoad(JToken token)
        {
            _weapon1Active = token["weapon1"].ToObject<bool>();
            _weapon2Active = token["weapon2"].ToObject<bool>();
            _weapon3Active = token["weapon3"].ToObject<bool>();

            if (!_weapon1Active)
            {
                Destroy(weapon1);
            }
            
            if (!_weapon2Active)
            {
                Destroy(weapon2);
            }
            
            if (!_weapon3Active)
            {
                Destroy(weapon3);
            }

            StartCoroutine(UpdateShortcutsOnStart());
        }
        
        private void Test() => BindPanelManager.Instance.UpdateBind();

        public Dictionary<string, object> OnSave()
        {
            return new Dictionary<string, object>
            {
                { "weapon1", weapon1 != null },
                { "weapon2", weapon2 != null },
                { "weapon3", weapon3 != null }
            };
        }
        
        private IEnumerator UpdateShortcutsOnStart()
        {
            yield return 0;
            
            BindPanelManager.Instance.UpdateBind();
        }
    }
}
