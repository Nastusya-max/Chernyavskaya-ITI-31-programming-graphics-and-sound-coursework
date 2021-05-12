using GameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Components
{
    public class DestinationComponent : ObjectComponent
    {
        public bool IsAnemyGotToDestinationPoint { get; set; } = false;
    }
}
