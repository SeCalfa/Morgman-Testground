using HFPS.Player;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.CameraShake
{
    public class MorgmanShaker : MonoBehaviour
    {
        private CameraShaker _cameraShaker;
        
        private void Start()
        {
            _cameraShaker = CameraShaker.Instance;
        }

        public void PistolFireShake()
        {
            _cameraShaker.ShakeOnce(
                2f,
                8f,
                0.05f,
                0.2f,
                new Vector3(0.1f, 0.15f, 0.1f),
                new Vector3(1.0f, 0.8f, 0.5f));
        }
        
        public void ShotgunFireShake()
        {
            _cameraShaker.ShakeOnce(
                4.5f,
                10f,
                0.02f,
                0.35f,
                new Vector3(0.2f, 0.25f, 0.15f),
                new Vector3(3.0f, 2.5f, 1.2f));
        }
        
        public void AxeHitShake()
        {
            _cameraShaker.ShakeOnce(
                3.5f,
                7f,
                0.02f,
                0.4f);
            // new Vector3(0.18f, 0.28f, 0.15f),
            // new Vector3(2.0f, 1.3f, 1.0f));
        }
    }
}
