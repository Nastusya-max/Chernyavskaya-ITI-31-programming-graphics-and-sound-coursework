using GameLibrary.Components;
using GameEngine.Collisions;
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
    public class BulletFlyScript : Script
    {
        private readonly float _flySpeed;

        private readonly Game3DObject _target;

        private readonly int _damage;

        public BulletFlyScript(float flySpeed, Game3DObject target, int damage)
        {
            this._flySpeed = flySpeed;
            this._target = target;
            this._damage = damage;
        }

        public override void Update(float delta = 1)
        {
            var direction = _target.Position - GameObject.Position;
            direction.Normalize();
            GameObject.MoveBy(direction * delta * _flySpeed);
            if(ObjectCollision.Intersects(_target.Collision, GameObject.Collision))
            {
                var targetHealthComponent = _target.GetComponent<HealthComponent>();
                targetHealthComponent.DealDamage(_damage);
                GameObject.Scene.RemoveGameObject(GameObject);
            }

        }
    }
}
