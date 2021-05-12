using GameEngine.Graphics;
using GameEngine.Scripts;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Scripts.Character
{
    class PlatformsSpawnerScript : Script
    {
        private Game3DObject _platform;

        private bool _isCreating;
        private float _lastCreatingTime;
        private float _cooldown;
        private Random random;

        public PlatformsSpawnerScript(Game3DObject platform, float cooldown = 0.51f)
        {
            _cooldown = cooldown;
            _lastCreatingTime = 0;
            _isCreating = false;
            _platform = platform;
            random = new Random();
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

            var copyPlatform = _platform.GetCopy();
            GameObject.Scene.AddGameObject(copyPlatform);
            copyPlatform.MoveTo(new Vector3((float)random.NextDouble(-6, 6), 8, (float)random.NextDouble(-8, 8)));

            //copyPlatform.AddScript(new PlatformsMovementScript((float)random.NextDouble(0.1, 0.1)));

            _lastCreatingTime = 0;
            _isCreating = true;
        }
    }
}
