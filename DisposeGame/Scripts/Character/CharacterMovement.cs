using GameEngine.Animation;
using GameEngine.Graphics;
using SharpDX;

namespace GameLibrary.Scripts.Character
{
    public class CharacterMovement
    {
        private float _speed;
        private Animation _animation;
        private bool _isAnimationPaused;


        public CharacterMovement(Animation animation, float speed)
        {
            _speed = speed;
            _animation = animation;
            _animation.Pause();
            _isAnimationPaused = true;
        }

        public void Move(Game3DObject character, Vector3 direction, float delta)
        {
            if (direction == Vector3.Zero)
            {
                if (!_isAnimationPaused)
                {
                    _isAnimationPaused = true;
                    _animation.Pause();
                }
                return;
            }
            if (_isAnimationPaused)
            {
                _animation.Restart();
                _isAnimationPaused = false;
            }
        }
    }
}
