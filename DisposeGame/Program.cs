using System;
using System.Windows.Forms;
using GameLibrary.Scenes;
using GameEngine;
using GameEngine.Game;
using SharpDX.Direct3D;
using Device11 = SharpDX.Direct3D11.Device;

namespace GameLibrary
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            if (Device11.GetSupportedFeatureLevel() != FeatureLevel.Level_11_0)
            {
                MessageBox.Show("DirectX11 not Supported");
                return;
            }
            //Game game = new Game(new Dick());
            Game game = new Game(new MainMenuScene());
            game.Run();
            game.Dispose();
        }
    }
}
