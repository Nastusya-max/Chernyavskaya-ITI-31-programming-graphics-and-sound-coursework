using GameLibrary.Components;
using GameEngine.Graphics;

namespace GameLibrary.Scripts.Bonuses
{
    public class InvisibilityBonusScript : PickableBonusScript
    {
        private float _duration;

        public InvisibilityBonusScript(Game3DObject picker, float duration = 5) : base(picker)
        {
            _duration = duration;
        }

        protected override void Picked()
        {
            _picker.GetComponent<VisibilityComponent>().MakeInvisible(_duration);
        }
    }
}
