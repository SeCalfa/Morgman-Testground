using HFPS.Player;
using UnityEngine;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Damage
{
    public class DamageItem : IInventoryItem
    {
        private MeleeController _axe;
        private WeaponController _pistol;
        private WeaponController _shotgun;

        private Vector2 _axeDamageStart, _axeDamageDelta;
        private float _pistolDamageStart, _pistolDamageDelta, _shotgunDamageStart, _shotgunDamageDelta;

        private int _count = 1;

        public void Construct(MeleeController axe, WeaponController pistol, WeaponController shotgun)
        {
            _axe = axe;
            _pistol = pistol;
            _shotgun = shotgun;

            _axeDamageStart = _axe.AttackDamage;
            _axeDamageDelta = (Vector2)_axe.AttackDamage * 0.1f;
            
            _pistolDamageDelta = _pistol.weaponSettings.weaponDamage * 0.1f;
            _pistolDamageStart = _pistol.weaponSettings.weaponDamage;
            
            _shotgunDamageDelta = _shotgun.weaponSettings.weaponDamage * 0.1f;
            _shotgunDamageStart = _shotgun.weaponSettings.weaponDamage;
        }
        
        public void Update()
        {
            
        }

        public void UpdateDamage(int count)
        {
            _count = count;
            
            _axe.AttackDamage = new Vector2Int((int)(_axeDamageStart.x + _axeDamageDelta.x * _count), (int)(_axeDamageStart.y + _axeDamageDelta.y * _count));
            _pistol.weaponSettings.weaponDamage = (int)(_pistolDamageStart + _pistolDamageDelta * _count);
            _shotgun.weaponSettings.weaponDamage = (int)(_shotgunDamageStart + _shotgunDamageDelta * _count);
        }
    }
}