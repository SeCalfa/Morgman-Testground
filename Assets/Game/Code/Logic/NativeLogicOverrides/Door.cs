using HFPS.Player;
using HFPS.Systems;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private float timeToAutoClose;
        [SerializeField] private float distanceToAutoClose;
        [Space]
        [SerializeField] private Transform parent;
        [SerializeField] private string openPosAnim;
        [SerializeField] private string closePosAnim;
        [SerializeField] private string openNegAnim;
        [SerializeField] private string closeNegAnim;

        private DynamicObject _dynamicObject;

        private bool _frontOfDoor;
        private float _timer;
        
        private void Awake()
        {
            _dynamicObject = GetComponent<DynamicObject>();
        }

        private void Update()
        {
            AutoCloseDoor();
        }

        public void UpdateOpenSettings()
        {
            if (_dynamicObject.isOpened) return;
            
            _frontOfDoor = InFrontOfDoor();
            _dynamicObject.useAnim = _frontOfDoor ? openPosAnim : openNegAnim;
            _dynamicObject.backUseAnim = _frontOfDoor ? closePosAnim : closeNegAnim;

            _timer = timeToAutoClose;
        }

        private void AutoCloseDoor()
        {
            if (!_dynamicObject.isOpened) return;
            
            _timer -= Time.deltaTime;

            if (_timer <= 0 && CloseDoorDistance())
            {
                _dynamicObject.UseObject();
            }
        }

        private bool InFrontOfDoor()
        {
            var toPlayer = (PlayerController.Instance.transform.position - transform.position).normalized;
            var dot = Vector3.Dot(parent.right, toPlayer);
            
            return dot > 0;
        }

        private bool CloseDoorDistance()
        {
            var distance = Vector3.Distance(PlayerController.Instance.transform.position, transform.position);
            return distance >= distanceToAutoClose;
        }
    }
}
