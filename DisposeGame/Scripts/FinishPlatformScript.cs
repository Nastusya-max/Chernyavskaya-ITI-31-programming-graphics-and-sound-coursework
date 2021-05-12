using GameEngine.Collisions;
using GameEngine.Graphics;
using GameEngine.Scripts;
using GameLibrary.Scenes;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Scripts
{
    class FinishPlatformScript : Script
    {
        public Game3DObject _player;
        public FinishPlatformScript(Game3DObject player)
        {
            _player = player;
        }

        public override void Update(float delta)
        {
            GameObject.MoveBy(Vector3.UnitY);

            if (ObjectCollision.Intersects(GameObject.Collision, _player.Collision))
            {
                GameObject.Scene.Game.ChangeScene(new WonScene());
            }

            GameObject.MoveBy(-Vector3.UnitY);
        }
    }
}
