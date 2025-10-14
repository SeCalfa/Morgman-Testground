using HFPS.Player;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.CameraShake
{
    public class AxeCameraShake : MonoBehaviour
    {
        private CameraShaker _cameraShaker;
        
        private void Start()
        {
            _cameraShaker = CameraShaker.Instance;
        }
        
        public void StartHit()
        {
            _cameraShaker.ShakeOnce(
                0.8f,
                2f,
                0.05f,
                0.25f,
                new Vector3(0.05f, 0.1f, 0.05f),
                new Vector3(0.5f, 0.3f, 0.2f));
        }

        public void Hit()
        {
            _cameraShaker.ShakeOnce(
                1.2f,
                3.5f,
                0.05f,
                0.25f,
                new Vector3(0.08f, 0.12f, 0.08f),
                new Vector3(0.8f, 0.6f, 0.4f));
        }

        public void EndHit()
        {
            _cameraShaker.ShakeOnce(
                1.8f,
                5f,
                0.03f,
                0.25f,
                new Vector3(0.1f, 0.15f, 0.08f),
                new Vector3(1.0f, 0.6f, 0.5f));
        }
    }
}
