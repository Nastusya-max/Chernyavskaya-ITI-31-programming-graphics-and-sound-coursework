using GameEngine.Collisions;
using GameEngine.Graphics;
using GameEngine.Scripts;

namespace GameLibrary.Scripts.Bonuses
{
    public abstract class PickableBonusScript : Script
    {
        protected Game3DObject _picker;

        public PickableBonusScript(Game3DObject picker)
        {
            _picker = picker;
        }

        public override void Update(float delta)
        {
            if (ObjectCollision.Intersects(_picker.Collision, GameObject.Collision))
            {
                Picked();

                GameObject.Scene.RemoveGameObject(GameObject);
            }
        }

        protected abstract void Picked();
    }
}
