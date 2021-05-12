using AmazingUILibrary;
using AmazingUILibrary.Backgrounds;
using AmazingUILibrary.Containers;
using AmazingUILibrary.Drawing;
using AmazingUILibrary.Elements;
using GameLibrary.Components;
using GameLibrary.Scripts;
using GameEngine.Collisions;
using GameEngine.Components;
using GameEngine.Game;
using GameEngine.Graphics;
using GameEngine.Scripts;
using GameEngine.Utils;
using SharpDX;
using SharpDX.Mathematics.Interop;
using Sound;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.Scenes
{
    public class GameScene : Scene
    {
        private Camera camera;

        private UIProgressBar _healthBar;

        private UISequentialContainer _createTowerUI;

        private Dictionary<string, UILibrary> _buttonsToCreateTower = new Dictionary<string, UILibrary>();

        private SharpAudioVoice _voice;

        protected override Camera CreateCamera()
        {
            return camera;
        }

        protected override void InitializeObjects(Loader loader, SharpAudioDevice audioDevice)
        {
            //
            _voice = new SharpAudioVoice(audioDevice, @"Sounds\Fuck_you.wav");

            // Create Town Hall
            var townHall = AddGameObject(loader.LoadGameObjectFromFile(@"Models/Forest.fbx", new Vector3(3, 0, 2.5f), new Vector3(0)));
            var healthComponent = new HealthComponent(100);
            healthComponent.OnChanged += (changedHp) => _healthBar.Value = changedHp;
            healthComponent.OnDeath += () => Game.ChangeScene(PreviousScene);
            townHall.AddComponent(healthComponent);

            // Create Spawner
            var ennemySpawner = new Game3DObject(Vector3.Zero, Vector3.Zero);
            ennemySpawner.IsHidden = true;
            var waves = CreateWaves(loader, townHall);
            ennemySpawner.AddScript(new SpawnScript(waves));
            AddGameObject(ennemySpawner);

            // Crete bullet template
            var bullet = loader.LoadGameObjectFromFile(@"Models/chlen2.fbx", new Vector3(0, 0, 0), new Vector3(0));
            var bulletScripts = Enumerable.Empty<Func<Script>>().ToList();
            var bulletComponents = Enumerable.Empty<Func<ObjectComponent>>().ToList();
            var bulletTemplate = new CopyableGameObject(bullet, bulletScripts, bulletComponents);

            // Crete towers template
            var towers = new Dictionary<string, Func<Game3DObject>>();
            var redTower = loader.LoadGameObjectFromFile(@"Models/chlen.fbx", new Vector3(5, 0, 0), new Vector3(0));
            redTower.IsHidden = true;
            var redScripts = new List<Func<Script>>
                {
                    () => new CreateBulletScript(0.5f, 10, () => bulletTemplate.Copy(), 10f, 1),
                };
            var redComponents = Enumerable.Empty<Func<ObjectComponent>>().ToList();
            var redtowerTemplate = new CopyableGameObject(redTower, redScripts, redComponents);
            towers.Add("Red", () => redtowerTemplate.Copy());

            // Create building
            var build = AddGameObject(loader.LoadGameObjectFromFile(@"Models/Forest.fbx", new Vector3(6, 0, 0), new Vector3(0)));
            build.Collision = new BoxCollision(2, 2);
            var tower = loader.LoadGameObjectFromFile(@"Models/Forest.fbx", new Vector3(1, 0, 0), new Vector3(0));
            build.AddScript(new CreateTowerScript(townHall, towers, _createTowerUI, _buttonsToCreateTower));

            // Create Camera
            camera = new Camera(new Vector3());
            AddGameObject(camera);
            camera.AddScript(new DinoMovementScript());
        }

        protected override UIElement InitializeUI(Loader loader, DrawingContext context, int screenWidth, int screenHeight)
        {
            context.NewBitmap("RedTower", loader.LoadBitmapFromFile(@"Textures/Towers/tower_red.png"));
            context.NewBitmap("BlueTower", loader.LoadBitmapFromFile(@"Textures/Towers/tower_blue.png"));
            context.NewBitmap("YellowTower", loader.LoadBitmapFromFile(@"Textures/Towers/tower_yellow.png"));
            context.NewBitmap("GreenTower", loader.LoadBitmapFromFile(@"Textures/Towers/tower_green.png"));

            // Create 

            _createTowerUI = new UISequentialContainer(Vector2.Zero, new Vector2(400, 100), false);
            var createRedTower = new UILibrary(new UIPanel(Vector2.Zero, new Vector2(80)) { Background = new TextureBackground("RedTower") });
            var createBlueTower = new UILibrary(new UIPanel(Vector2.Zero, new Vector2(80)) { Background = new TextureBackground("BlueTower") });
            var createYellowTower = new UILibrary(new UIPanel(Vector2.Zero, new Vector2(80)) { Background = new TextureBackground("YellowTower") });
            var createGreenTower = new UILibrary(new UIPanel(Vector2.Zero, new Vector2(80)) { Background = new TextureBackground("GreenTower") });

            _createTowerUI.Add(new UIMarginContainer(createRedTower, 10));
            _createTowerUI.Add(new UIMarginContainer(createYellowTower, 10));
            _createTowerUI.Add(new UIMarginContainer(createBlueTower, 10));
            _createTowerUI.Add(new UIMarginContainer(createGreenTower, 10));
            _createTowerUI.IsVisible = false;

            _buttonsToCreateTower.Add("Red", createRedTower);
            _buttonsToCreateTower.Add("Blue", createBlueTower);
            _buttonsToCreateTower.Add("Yellow", createYellowTower);
            _buttonsToCreateTower.Add("Green", createGreenTower);


            // Create ui and gui
            context.NewSolidBrush("ProgressBarBrush", new RawColor4(144f / 255f, 238f / 255f, 233f / 255f, 1f));
            _healthBar = new UIProgressBar(Vector2.Zero, new Vector2(100, 20), "ProgressBarBrush");
            _healthBar.MaxValue = 100;
            _healthBar.Value = 100;
            var ui = new UIMultiElementsContainer(Vector2.Zero, new Vector2(screenWidth, screenHeight));
            var gui = new UISequentialContainer(Vector2.Zero, new Vector2(screenWidth, screenHeight))
            {
                MainAxis = UISequentialContainer.Alignment.Start,
                CrossAxis = UISequentialContainer.Alignment.Center,
            };
            ui.OnResized += () => gui.Size = ui.Size;
            ui.Add(gui);
            ui.Add(_createTowerUI);
            gui.Add(new UIMarginContainer(_healthBar, 15));
            return ui;
        }

        private Vector3[] CreatePath()
        {
            var path = new Vector3[]
            {
                new Vector3(-10,0,0),
                new Vector3(3,0,0),
                new Vector3(3,0,2),
            };

            return path;
        }

        private List<(List<Game3DObject> enemies, float timeBetweenWaves)> CreateWaves(Loader loader, Game3DObject townHall)
        {
            var smallEnemy = loader.LoadGameObjectFromFile(@"Models/Forest.fbx", new Vector3(0, 0, 0), new Vector3(0));
            smallEnemy.Collision = new BoxCollision(0.5f, 0.5f);
            var smallEnemyScripts = new List<Func<Script>>
            {
                () => new EnemyMovementScript(CreatePath(), 5),
                () => new TownHallAttackScript(townHall),
            };
            var smallEnemyComponents = new List<Func<ObjectComponent>>
            {
                () => new DestinationComponent(),
                () =>
                {
                    var component = new HealthComponent(2);
                    component.OnDamaged += (_, _1) =>
                    {
                        _voice.Stop();
                        _voice.Play();
                    };
                    return component;
                },
                () => new EnemyComponent(),
            };

            var smallEnemyTemplate = new CopyableGameObject(smallEnemy, smallEnemyScripts, smallEnemyComponents);

            var waves = new List<(List<Game3DObject> enemies, float timeBetweenWaves)>
            {
                (MakeCopies(smallEnemyTemplate, 3), 5),
                (MakeCopies(smallEnemyTemplate, 3), 0),
            };

            return waves;
        }

        private List<Game3DObject> MakeCopies(CopyableGameObject template, int count)
        {
            var copies = new List<Game3DObject>();
            for (int currentCopy = 0; currentCopy < count; currentCopy++)
            {
                copies.Add(template.Copy());
            }

            return copies;
        }

        public override void Dispose()
        {
            _voice.Dispose();
            base.Dispose();
        }
    }
}
