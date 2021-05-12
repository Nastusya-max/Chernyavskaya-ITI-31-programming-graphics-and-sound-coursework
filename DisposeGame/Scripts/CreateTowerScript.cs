using AmazingUILibrary;
using AmazingUILibrary.Elements;
using GameEngine.Collisions;
using GameEngine.Game;
using GameEngine.Graphics;
using GameEngine.Scripts;
using System;
using System.Collections.Generic;

namespace GameLibrary.Scripts
{
    public class CreateTowerScript : Script
    {
        private readonly UIElement _ui;

        private readonly InputController _inputController;

        private readonly Dictionary<string, UILibrary> _buttons;

        private Dictionary<string, Func<Game3DObject>> _towers;

        public CreateTowerScript(Game3DObject townHall, Dictionary<string, Func<Game3DObject>> towers, UIElement ui, Dictionary<string, UILibrary> buttons)
        {
            _ui = ui;
            _inputController = InputController.GetInstance();
            _buttons = buttons;
            _towers = towers;
            OnRedClick();
        }

        public override void Update(float delta)
        {
            if (ObjectCollision.Intersects(GameObject.Collision, _inputController.CursorRay))
            {
                _ui.IsVisible = true;
                _ui.LocalPosition = new SharpDX.Vector2(_inputController.MousePositionX, _inputController.MousePositionY);
                _inputController.CursorRay = null;
            }
        }

        public void OnRedClick()
        {
            var key = "Red";
            _buttons[key].OnClicked += () =>
            {
                var tower = _towers[key].Invoke();
                GameObject.Scene.AddGameObject(tower);
                tower.MoveTo(GameObject.Position);
                GameObject.Scene.RemoveGameObject(GameObject);
                tower.IsHidden = false;
                _ui.IsVisible = false;
            };
        }
    }
}
