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
    class PlatformsMovementScript : Script
    {
        private float _speedY;
        private float _maxY;
        private float _minY;
        public Game3DObject _player;

        public PlatformsMovementScript(Game3DObject player, float speedY, float maxY, float minY)
        {

            _speedY = speedY;
            _maxY = maxY;
            _minY = minY;
            _player = player;
        }

        public override void Update(float delta)
        {
            var positionToMove = new Vector3(0, _speedY, 0);
            GameObject.MoveBy(positionToMove);

            if (ObjectCollision.Intersects(GameObject.Collision, _player.Collision))
            {
                _player.MoveBy(positionToMove);
            }

            if (GameObject.Position.Y >= _maxY || GameObject.Position.Y <= _minY)
            {
                _speedY *= -1;
            }
        }
    }
}
