using GameEngine.Graphics;
using GameEngine.Scripts;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Scripts
{
    class BulletSpawnerScript : Script
    {
        public event Action OnEggShoot;

        private Game3DObject _bullet;
        private Game3DObject _target;

        private bool _isCreating;
        private float _lastCreatingTime;
        private float _cooldown;

        public BulletSpawnerScript(Game3DObject target, Game3DObject bullet, float cooldown = 2f)
        {
            _cooldown = cooldown;
            _lastCreatingTime = 0;
            _isCreating = false;
            _bullet = bullet;
            _target = target;
        }

        public override void Update(float delta)
        {
            if (_isCreating)
            {
                _lastCreatingTime += delta;
                if (_lastCreatingTime >= _cooldown)
                {
                    _isCreating = false;
                }
                return;
            }
            var position = new Vector3(GameObject.Position.X, GameObject.Position.Y - 0.27f, GameObject.Position.Z);
            var direction = _target.Position - position;
            direction.Normalize();
            Game3DObject copyBullet = _bullet.GetCopy();
            copyBullet.MoveTo(position);
            //copyBullet.MoveTo(GameObject.Position);
            if ((GameObject.Position - _target.Position).Length() <= 1.5f)
            {
                copyBullet.AddScript(new BulletScript(direction));
                GameObject.Scene.AddGameObject(copyBullet);
                OnEggShoot?.Invoke();
            }


            _lastCreatingTime = 0;
            _isCreating = true;
        }
    }
}
