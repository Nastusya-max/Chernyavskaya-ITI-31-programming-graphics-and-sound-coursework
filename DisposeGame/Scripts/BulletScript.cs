using GameLibrary.Components;
using GameEngine.Animation;
using GameEngine.Collisions;
using GameEngine.Graphics;
using GameEngine.Scripts;
using SharpDX;
using System;
using System.Threading;

namespace GameLibrary.Scripts
{
    public class BulletScript : Script
    {
        private Vector3 _direction;
        private float _speed;
        private int _damage;

        public BulletScript(Vector3 direction, float speed = 1f, int damage = 5)
        {
            _direction = direction;
            _speed = speed;
            _damage = damage;
        }

        public override void Init()
        {
            new Transition(0, 0, 1).TransitionEnded += () => {
                GameObject.Scene.RemoveGameObject(GameObject);
            };
        }

        public override void Update(float delta)
        {
            GameObject.MoveBy(_direction * delta * _speed);
            ObjectCollision collision = GameObject.Collision;
            foreach (Game3DObject gameObject in GameObject.Scene.GameObjects)
            {
                if (gameObject == GameObject) continue;

                if (gameObject.Collision != null)
                {
                    if (ObjectCollision.Intersects(gameObject.Collision, collision))
                    {
                        gameObject.GetComponent<HealthComponent>()?.DealDamage(_damage);
                        GameObject.Scene.RemoveGameObject(GameObject);
                    }
                }
            }
        }

    }
}
