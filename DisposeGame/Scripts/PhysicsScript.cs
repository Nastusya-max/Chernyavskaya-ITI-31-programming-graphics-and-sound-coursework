using GameLibrary.Components;
using GameEngine.Scripts;

namespace GameLibrary.Scripts
{
    public class PhysicsScript : Script
    {
        private PhysicsComponent _physics;

        public PhysicsScript(PhysicsComponent physics)
        {
            _physics = physics;
        }

        public override void Update(float delta)
        {
            _physics.UpdateObjectPosition(delta);
        }
    }
}
