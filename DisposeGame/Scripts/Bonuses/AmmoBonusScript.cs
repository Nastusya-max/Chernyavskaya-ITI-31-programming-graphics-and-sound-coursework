using GameLibrary.Components;
using GameEngine.Graphics;

namespace GameLibrary.Scripts.Bonuses
{
    public class AmmoBonusScript : PickableBonusScript
    {
        private int _ammo;

        public AmmoBonusScript(Game3DObject picker, int ammo = 10) : base(picker)
        {
            _ammo = ammo;
        }

        protected override void Picked()
        {
            _picker.GetComponent<AmmoComponent>().AddAmmo(_ammo);
        }
    }
}
