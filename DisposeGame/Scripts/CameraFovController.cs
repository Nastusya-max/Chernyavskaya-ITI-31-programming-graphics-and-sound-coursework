using GameEngine.Graphics;
using GameEngine.Scripts;
using SharpDX.DirectInput;

namespace GameLibrary.Scripts
{
    public class CameraFovController : KeyboardListenerScript
    {
        public CameraFovController(Camera camera)
        {
            Actions.Add(Key.Z, delta => camera.FOVY += 0.05f);
            Actions.Add(Key.X, delta => camera.FOVY -= 0.05f);
        }
    }
}
