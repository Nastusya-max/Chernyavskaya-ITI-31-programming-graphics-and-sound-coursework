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
    public class CreateBulletScript : Script
    {
        private float _maxDistance;

        private readonly float _bulletFlySpeed;

        private readonly Func<Game3DObject> _createBullet;

        private float _fireTime;

        private float _time;

        private readonly int _damage;

        public CreateBulletScript(float fireTime, float distance, Func<Game3DObject> createBullet, float bulletFlySpeed, int damage)
        {
            _maxDistance = distance;
            this._bulletFlySpeed = bulletFlySpeed;
            this._createBullet = createBullet;
            _fireTime = fireTime;
            _damage = damage;
        }

        public override void Update(float delta)
        {
            _time += delta;
            if (_time > _fireTime)
            {
                _time = 0;
                var objects = GameObject.Scene.GameObjects.Where(_ => _.HasComponent<EnemyComponent>());
                var distances = new List<(Game3DObject @object, float distance)>();
                foreach (var currentObject in objects)
                {
                    var distance = Vector3.Distance(GameObject.Position, currentObject.Position);
                    if (distance <= _maxDistance)
                    {
                        distances.Add((currentObject, distance));
                    }
                }
                if (distances.Count > 0)
                {
                    distances.OrderBy(_ => _.distance);
                    var target = distances[0].@object;
                    var bullet = _createBullet.Invoke();
                    bullet.MoveTo(GameObject.Position);
                    bullet.AddScript(new BulletFlyScript(_bulletFlySpeed, target, _damage));
                    bullet.Collision = new SphereCollision(0.5f);
                    GameObject.Scene.AddGameObject(bullet);
                }
            }
        }
    }
}
