using GameLibrary.Components;
using GameEngine.Animation;
using GameEngine.Scripts;
using SharpDX;
using System;

namespace GameLibrary.Scripts
{
    public class EnemyMovementScript : Script
    {
        private Vector3[] _path;

        private int _currentPosition = 0;

        private float _timeFromPreviousKey = 0;

        private float _timeBetweenKeys = 0;

        private DestinationComponent _destinationComponent;

        public EnemyMovementScript(Vector3[] path, float timeBetweenKeys)
        {
            _path = path;
            _timeBetweenKeys = timeBetweenKeys;
        }

        public override void Init()
        {
            _destinationComponent = GameObject.GetComponent<DestinationComponent>();
        }

        public override void Update(float delta)
        {


            _timeFromPreviousKey += delta;
            var interpolationTime = _timeFromPreviousKey / _timeBetweenKeys;
            GameObject.MoveTo(Vector3.Lerp( _path[_currentPosition], _path[_currentPosition+1], interpolationTime));

            if (_timeFromPreviousKey >= _timeBetweenKeys)
            {
                _timeFromPreviousKey = 0;
                _currentPosition++;

                if (_currentPosition + 1 == _path.Length)
                {
                    _destinationComponent.IsAnemyGotToDestinationPoint = true;
                    GameObject.RemoveScript(this);
                    return;
                }

                var direction = _path[_currentPosition] - _path[_currentPosition + 1];
                var angle = (float)Math.Atan2(direction.X, direction.Z);
                new Transition(GameObject.Rotation.Z, GameObject.Rotation.Z + angle, 0.5f).Process += rotationAngle => GameObject.SetRotationZ(rotationAngle);
                
            }
        }
    }
}
