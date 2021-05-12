using GameLibrary.Components;
using GameEngine.Game;
using GameEngine.Graphics;
using GameEngine.Scripts;
using SharpDX;
using System;

namespace GameLibrary.Scripts.Character
{
    public class PlayerGunScript : Script
    {
        public event Action OnCakeShoot;

        private InputController _inputController;

        private Game3DObject _bulletPrototype;

        //private AmmoComponent _ammo;

        private bool _isReloading;
        private float _reloadingTime;
        private float _cooldown;

        public PlayerGunScript(Game3DObject bullet, float cooldown = 0.2f)
        {
            _inputController = InputController.GetInstance();
            _bulletPrototype = bullet;
            _cooldown = cooldown;
            _reloadingTime = 0;
            _isReloading = false;
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
            if (_inputController.MouseUpdate && _inputController.MouseButtons[0])
            {
                Vector3 rotation = GameObject.Rotation;
                rotation.Y = GameObject.Children[1].Rotation.Y;
                Matrix rotationMatrix = Matrix.RotationYawPitchRoll(rotation.Z, rotation.Y, rotation.X);
                Vector3 direction = (Vector3)Vector3.Transform(Vector3.UnitZ, rotationMatrix);
                Game3DObject bullet = _bulletPrototype.GetCopy();
                bullet.MoveTo(GameObject.Position + direction);
                bullet.AddScript(new BulletScript(direction, 1, 1));
                GameObject.Scene.AddGameObject(bullet);
                OnCakeShoot?.Invoke();

                _reloadingTime = 0;
                _isReloading = true;
            }
        }
    }
}
