using GameLibrary.Components;
using GameEngine.Graphics;

namespace GameLibrary.Scripts.Bonuses
{
    public class HealthBonusScript : PickableBonusScript
    {
        private int _heal;

        public HealthBonusScript(Game3DObject picker, int heal = 10) : base(picker)
        {
            _heal = heal;
        }

        protected override void Picked()
        {
            _picker.GetComponent<HealthComponent>().Heal(_heal);
        }
    }
}
