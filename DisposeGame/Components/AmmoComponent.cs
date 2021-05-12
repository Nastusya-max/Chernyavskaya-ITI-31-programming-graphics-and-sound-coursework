using GameEngine.Components;
using System;

namespace GameLibrary.Components
{
    public class AmmoComponent : ObjectComponent
    {
        public event Action<int> OnChanged;

        public int Ammo { get; private set; }

        public AmmoComponent(int ammo = 10)
        {
            Ammo = ammo;
        }

        public void AddAmmo(int amount)
        {
            Ammo += amount;

            OnChanged?.Invoke(Ammo);
        }

        public void DecrementAmmo()
        {
            Ammo--;

            OnChanged?.Invoke(Ammo);
        }
    }
}
