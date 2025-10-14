using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Flashlight
{
    public class NewFlashlight : MonoBehaviour
    {
        [SerializeField] private Light pointLight;
        [SerializeField] private Light spotLight;

        private float _startPointLightIntensity, _startSpotLightIntensity, _lowPointLightIntensity, _lowSpotLightIntensity;

        public void Construct(float percent)
        {
            _startPointLightIntensity = pointLight.intensity;
            _startSpotLightIntensity = spotLight.intensity;

            _lowPointLightIntensity = _startPointLightIntensity * (percent / 100f);
            _lowSpotLightIntensity = _startSpotLightIntensity * (percent / 100f);
        }

        public void Toggle(bool isOn, bool fullIntensity = false)
        {
            if (isOn)
            {
                if (fullIntensity)
                {
                    pointLight.intensity = _startPointLightIntensity;
                    spotLight.intensity = _startSpotLightIntensity;
                }
                else
                {
                    pointLight.intensity = _lowPointLightIntensity;
                    spotLight.intensity = _lowSpotLightIntensity;
                }
                
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
