using GameEngine.Components;
using GameEngine.Graphics;
using GameEngine.Scripts;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.Scripts
{
    public class SpawnScript : Script
    {
        #region Fields

        private const float SpawnDelay = 2f;

        private readonly List<(List<Game3DObject> enemies, float timeBetweenWaves)> _waves;

        private int _currentWaveIndex = 0;

        private int _currentEnemyIndex = 0;

        private bool _isReloading;

        private float _reloadingTime;

        private float _cooldown;

        #endregion

        public SpawnScript(
            List<(List<Game3DObject> enemies, float timeBetweenWaves)> waves)
        {
            _waves = waves;
            _isReloading = false;
            _reloadingTime = 0;
            _cooldown = SpawnDelay;
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

            SpawnObject(_waves[_currentWaveIndex].enemies[_currentEnemyIndex]);

            _currentEnemyIndex++;

            if (_currentEnemyIndex >= _waves[_currentWaveIndex].enemies.Count)
            {
                _cooldown = _waves[_currentWaveIndex].timeBetweenWaves;
                _currentWaveIndex++;
                _currentEnemyIndex = 0;

                if (_currentWaveIndex >= _waves.Count)
                {
                    GameObject.RemoveScript(this);
                    return;
                }
            }
            else
            {
                _cooldown = SpawnDelay;
            }

            _reloadingTime = 0;
            _isReloading = true;       
        }

        private void SpawnObject(Game3DObject game3DObject)
        {
            GameObject.Scene.AddGameObject(game3DObject);
            game3DObject.IsHidden = false;
        }
    }
}
