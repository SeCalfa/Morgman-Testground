using System.Collections;
using HFPS.Systems;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides
{
    public class Plank : MonoBehaviour
    {
        [SerializeField] private AudioClip[] woodCrack;
        [SerializeField] private GameObject brakeEffect;
        
        private AudioSource _audioSource;
        private DynamicObjectPlank _dynamicObjectPlank;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _dynamicObjectPlank = GetComponent<DynamicObjectPlank>();
        }

        public void ApplyDamage(int amount)
        {
            _dynamicObjectPlank.UseObject();
        }

        public void Brake()
        {
            if (woodCrack.Length > 0)
            {
                _audioSource.PlayOneShot(woodCrack[Random.Range(0, woodCrack.Length)]);
            }

            if (brakeEffect)
            {
                Instantiate(brakeEffect, transform.position, Quaternion.identity);
            }

            gameObject.SetActive(false);
        }
    }
}
