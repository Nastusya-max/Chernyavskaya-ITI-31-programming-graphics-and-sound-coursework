using GameLibrary.Components;
using GameLibrary.Scenes;
using GameEngine.Animation;
using GameEngine.Game;
using GameEngine.Graphics;
using GameEngine.Scripts;
using SharpDX;
using SharpDX.DirectInput;

namespace GameLibrary.Scripts.Character
{
    public class PlayerMovementScript : KeyboardListenerScript
    {
        private InputController _inputController;
        private Vector3 _moveDirection;
        private float _mouseSensitivity;
        private float _speed;
        private Camera _camera; 


        public PlayerMovementScript(Camera camera, PhysicsComponent physics, float speed = 3f, float jump = 0.14f, float mouseSensitivity = 0.25f)
        {
            _camera = camera;
            _mouseSensitivity = mouseSensitivity;
            _speed = speed;

            Actions.Add(Key.W, delta => _moveDirection += Vector3.UnitZ);
            Actions.Add(Key.S, delta => _moveDirection -= Vector3.UnitZ);
            Actions.Add(Key.A, delta => _moveDirection -= Vector3.UnitX);
            Actions.Add(Key.D, delta => _moveDirection += Vector3.UnitX);
            Actions.Add(Key.Space, delta =>
            {
                if (physics.IsPlatformCollision)
                {
                    physics.AddImpulse(Vector3.UnitY * jump);
                }
            });
          
            _inputController = InputController.GetInstance();
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            if (_inputController.MouseUpdate)
            {
                GameObject.RotateZ(delta * _mouseSensitivity * _inputController.MouseRelativePositionX);
                _camera.RotateY(delta * _mouseSensitivity * _inputController.MouseRelativePositionY); 
            }

            if (GameObject.Position.Y < -20)
            {
                GameObject.Scene.Game.ChangeScene(new LoseScene());
            }
        }

        protected override void BeforeKeyProcess(float delta)
        {
            _moveDirection = Vector3.Zero;
        }

        protected override void AfterKeyProcess(float delta)
        {
            _moveDirection.Normalize();
            Vector3 rotation = GameObject.Rotation;
            Matrix rotationMatrix = Matrix.RotationYawPitchRoll(rotation.Z, rotation.Y, rotation.X);
            GameObject.MoveBy((Vector3)Vector3.Transform(_moveDirection * _speed * delta, rotationMatrix));
        }
    }
}
