using HFPS.Systems;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides
{
    public class Plank : MonoBehaviour
    {
        [SerializeField] private AudioClip[] woodCrack;
        [SerializeField] private GameObject brakeEffect;

        [SaveableField, HideInInspector]
        public bool broken;
        
        private AudioSource _audioSource;
        private DynamicObjectPlank _dynamicObjectPlank;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _dynamicObjectPlank = GetComponent<DynamicObjectPlank>();

            BrakeOnStart();
        }

        private void BrakeOnStart()
        {
            if (!broken) return;
            _dynamicObjectPlank.UseObject();
            gameObject.SetActive(false);
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
