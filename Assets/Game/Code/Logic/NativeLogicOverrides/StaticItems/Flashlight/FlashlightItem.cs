using ThunderWire.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Flashlight
{
    public class FlashlightItem : IInventoryItem
    {
        private InputAction _input;

        private StaticInventoryPresenter _staticInventoryPresenter;
        private NewFlashlight _flashlight;
        private FlashlightPresenter _flashlightPanel;
        private float _powerPercentPerSecond;

        private bool _active;
        private float _currentPower;
        public int ActiveBatteries { get; private set; } = 1;

        private const string FLASHLIGHT_ACTION_NAME = "Flashlight";
        private const float MAX_POWER = 100f;
        private const int MAX_BATTERIES = 5;

        public void Construct(StaticInventoryPresenter staticInventoryPresenter, NewFlashlight flashlight, FlashlightPresenter flashlightPanel, float powerPercentPerSecond)
        {
            _input = InputHandler.Instance.inputActionAsset.FindAction(FLASHLIGHT_ACTION_NAME, true);
            
            _staticInventoryPresenter = staticInventoryPresenter;
            _flashlight = flashlight;
            _flashlightPanel = flashlightPanel;
            _powerPercentPerSecond = powerPercentPerSecond;

            _currentPower = MAX_POWER;
            
            _flashlightPanel.UpdateBatteries(ActiveBatteries);
        }

        public void Update()
        {
            if (_input.WasPressedThisFrame())
            {
                ToggleLight(true);
            }

            if (_input.WasReleasedThisFrame())
            {
                ToggleLight(false);
            }

            if (_active)
            {
                _currentPower = Mathf.Clamp(_currentPower - Time.deltaTime * _powerPercentPerSecond, 0f, MAX_POWER);
                
                _flashlightPanel.UpdateProgress(_currentPower, MAX_POWER);
                
                if (_currentPower > 0f || ActiveBatteries > 0)
                {
                    _flashlight.Toggle(true, true);
                }
                else
                {
                    _flashlight.Toggle(true);
                    
                    return;
                }

                if (_currentPower == 0)
                {
                    UseBattery();
                }
            }
        }

        public void AddBattery()
        {
            if (ActiveBatteries == MAX_BATTERIES)
            {
                return;
            }

            ActiveBatteries += 1;
            _flashlightPanel.UpdateBatteries(ActiveBatteries);
        }

        private bool UseBattery()
        {
            if (ActiveBatteries == 0)
            {
                return false;
            }

            ActiveBatteries -= 1;
            _currentPower = MAX_POWER;
            _flashlightPanel.UpdateBatteries(ActiveBatteries);
            _staticInventoryPresenter.UpdateBatteries(ActiveBatteries);
            return true;
        }

        private void ToggleLight(bool isOn)
        {
            if (isOn)
            {
                _flashlightPanel.Show();
            }
            else
            {
                _flashlightPanel.Hide();
                _flashlight.Toggle(false);
            }
                
            _active = isOn;
        }
    }
}