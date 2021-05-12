using GameLibrary.Components;
using GameEngine.Graphics;
using GameEngine.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Scripts
{
    public class TownHallAttackScript : Script
    {
        private DestinationComponent _destinationComponent;

        private readonly Game3DObject _target;

        private readonly int _dammage;

        private bool _isReloading;
        private float _reloadingTime;
        private float _cooldown;

        public TownHallAttackScript(Game3DObject target, int dammage = 1, float cooldown = 0.2f)
        {
            _target = target;
            _dammage = dammage;
            _cooldown = cooldown;
        }

        public override void Init()
        {
            _destinationComponent = GameObject.GetComponent<DestinationComponent>();    
        }

        public override void Update(float delta)
        {
            if (_isReloading)
            {
                _reloadingTime += delta;
                if (_reloadingTime >= _cooldown)
                {
                    _isReloading = false;
                }
                return;
            }

            if (_destinationComponent.IsAnemyGotToDestinationPoint)
            {
                var targetHealthComponent = _target.GetComponent<HealthComponent>();
                targetHealthComponent.DealDamage(_dammage);
                _reloadingTime = 0;
                _isReloading = true;
            }
        }
    }
}
