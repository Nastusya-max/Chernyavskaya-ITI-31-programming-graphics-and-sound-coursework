using AmazingUILibrary;
using AmazingUILibrary.Backgrounds;
using AmazingUILibrary.Containers;
using AmazingUILibrary.Drawing;
using AmazingUILibrary.Elements;
using GameEngine.Animation;
using GameEngine.Collisions;
using GameEngine.Game;
using GameEngine.Graphics;
using GameLibrary.Components;
using GameLibrary.Scripts;
using GameLibrary.Scripts.Character;
using SharpDX;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using Sound;

namespace GameLibrary.Scenes
{
    class MainGameScene : Scene
    {
        private Camera camera;
        private UIProgressBar _healthBar;

        private SharpAudioVoice _egg;
        private SharpAudioVoice _cake;
        private SharpAudioVoice _die;
        private SharpAudioVoice _duck;

        protected override Camera CreateCamera()
        {
            return camera;
        }

        protected override void InitializeObjects(Loader loader, SharpAudioDevice audioDevice)
        {
            //Create sounds
            _egg = new SharpAudioVoice(audioDevice, @"Sounds\egg.wav");
            _cake = new SharpAudioVoice(audioDevice, @"Sounds\cake.wav");
            _die = new SharpAudioVoice(audioDevice, @"Sounds\die.wav");
            _duck = new SharpAudioVoice(audioDevice, @"Sounds\duck.wav");
            //Create camera
            camera = new Camera(new Vector3());
            //Create bullet of enemy
            var egg = loader.LoadGameObjectFromFile(@"Models/egg.fbx", new Vector3(0, 0, 0), new Vector3(0));
            egg.Collision = new BoxCollision(0.03f, 0.03f);
            //Create bullet of player
            var cake = loader.LoadGameObjectFromFile(@"Models/cake.fbx", new Vector3(0, 0, 0), new Vector3(0));
            cake.Collision = new BoxCollision(0.03f, 0.03f);
            //Create player
            var player = AddGameObject(loader.LoadGameObjectFromFile(@"Models/player.fbx", new Vector3(0, 1, 0), new Vector3(0)));
            player.Collision = new BoxCollision(0.3f, 0.7f);
            player.AddChild(camera);
            var physics = new PhysicsComponent();
            player.AddComponent(physics);
            player.AddScript(new PhysicsScript(physics));
            player.AddScript(new PlayerMovementScript(camera, physics));
            var cakeBullet = new PlayerGunScript(cake);
            player.AddScript(cakeBullet);
            cakeBullet.OnCakeShoot += () =>
            {
                _cake.Stop();
                _cake.Play();
            };

            var health = new HealthComponent(10);
            health.OnDeath += () =>
            {
                //_die.Stop();
                //_die.Play();
                //player.Scene.Game.ChangeScene(new LoseScene());
            };
            health.OnChanged += (changedHp) => _healthBar.Value = changedHp;
            health.OnDeath += () => Game.ChangeScene(new LoseScene());
            player.AddComponent(health);

            var platform0 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(-4, 0, 0), new Vector3(0)));
            platform0.Collision = new BoxCollision(2.2f, 0.7f);
            platform0.AddScript(new FinishPlatformScript(player));

            var platform = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(0, 0, 0), new Vector3(0)));
            platform.Collision = new BoxCollision(2.2f, 0.7f);
            //platform.AddScript(new PlatformsMovementScript())

            var platform1 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(4, 0, 0), new Vector3(0)));
            platform1.Collision = new BoxCollision(2.2f, 0.7f);
            platform1.AddScript(new PlatformsMovementScript(player, 0.01f, 2, 0));

            var platform2 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(8, 4, 0), new Vector3(0)));
            platform2.Collision = new BoxCollision(2.2f, 0.7f);

            var platform3 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(12, 4, 0), new Vector3(0)));
            platform3.Collision = new BoxCollision(2.2f, 0.7f);

            var platform4 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(16, 0, 0), new Vector3(0)));
            platform4.Collision = new BoxCollision(2.2f, 0.7f);
            platform4.AddScript(new PlatformsMovementScript(player, 0.007f, 2, 0));

            var platform5 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(20, 2, 4), new Vector3(0)));
            platform5.Collision = new BoxCollision(2.2f, 0.7f);

            var platform6 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(24, 4, 4), new Vector3(0)));
            platform6.Collision = new BoxCollision(2.3f, 0.7f);

            var platform7 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(28, 4, 0), new Vector3(0)));
            platform7.Collision = new BoxCollision(2.3f, 0.7f);
            platform7.AddScript(new PlatformsMovementScript(player, 0.016f, 8, 0));

            var platform8 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(32, 8, 0), new Vector3(0)));
            platform8.Collision = new BoxCollision(2.3f, 0.7f);
            platform8.AddScript(new PlatformsMovementScript(player, -0.012f, 8, 0));

            var platform9 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(36, 7, 0), new Vector3(0)));
            platform9.Collision = new BoxCollision(2.3f, 0.7f);

            var platform10 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(40, 7, 0), new Vector3(0)));
            platform10.Collision = new BoxCollision(2.3f, 0.7f);
            platform10.AddScript(new PlatformsMovementScript(player, -0.01f, 10, 6));

            var platform11 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(44, 12, 0), new Vector3(0)));
            platform11.Collision = new BoxCollision(2.3f, 0.7f);

            var platform12 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(48, 8, 0), new Vector3(0)));
            platform12.Collision = new BoxCollision(2.3f, 0.7f);

            var platform13 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(52, 4, 0), new Vector3(0)));
            platform13.Collision = new BoxCollision(2.3f, 0.7f);

            var platform14 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(56, 0, 0), new Vector3(0)));
            platform14.Collision = new BoxCollision(2.3f, 0.7f);

            var platform15 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(60, 2, 0), new Vector3(0)));
            platform15.Collision = new BoxCollision(2.3f, 0.7f);

            var platform16 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(64, 4, 0), new Vector3(0)));
            platform16.Collision = new BoxCollision(2.3f, 0.7f);

            var platform18 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(68, 0, 0), new Vector3(0)));
            platform18.Collision = new BoxCollision(2.3f, 0.7f);
            platform18.AddScript(new PlatformsMovementScript(player, 0.01f, 10, 0));

            var platform19 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(72, 10, 0), new Vector3(0)));
            platform19.Collision = new BoxCollision(2.3f, 0.7f);
            platform19.AddScript(new PlatformsMovementScript(player, -0.01f, 10, 0));

            var platformFinish = AddGameObject(loader.LoadGameObjectFromFile(@"Models/platform.fbx", new Vector3(76, 6, 0), new Vector3(0)));
            platformFinish.Collision = new BoxCollision(2.3f, 0.7f);
            platformFinish.AddScript(new FinishPlatformScript(player));

            var enemy1 = AddGameObject(loader.LoadGameObjectFromFile(@"Models/bird.fbx", new Vector3(0, 1f, 1), new Vector3(0, 0, 1.8f)));
            var healthEnemy = new HealthComponent(3);
            healthEnemy.OnDeath += () =>
            {
                _duck.Stop();
                _duck.Play();
            };
            enemy1.Collision = new BoxCollision(0.3f, 0.3f);
            enemy1.AddComponent(healthEnemy);
            var enemyBullet = new BulletSpawnerScript(player, egg);
            enemy1.AddScript(enemyBullet);
            enemyBullet.OnEggShoot += () =>
            {
                //_egg.Stop();
                //_egg.Play();
            };

            var heart = AddGameObject(loader.LoadGameObjectFromFile(@"Models/heart.fbx", new Vector3(-0.06f, 0.19f, 0.5f), new Vector3(0, 0, 2)));
            new Animation(new float[] { 0, MathUtil.Pi * 2 }, 2, int.MaxValue).AddProcess((value) =>
            {
                heart.SetRotationZ(value);
            });
            camera.AddChild(heart);
        }

        protected override UIElement InitializeUI(Loader loader, DrawingContext context, int screenWidth, int screenHeight)
        {
            var ui = new UIMultiElementsContainer(Vector2.Zero, new Vector2(screenWidth, screenHeight));

            // Create ui and gui
            context.NewSolidBrush("ProgressBarBrush", new RawColor4(30f / 255f, 144f / 255f, 255f / 255f, 0.6f));
            _healthBar = new UIProgressBar(Vector2.Zero, new Vector2(100, 20), "ProgressBarBrush");
            _healthBar.MaxValue = 100;
            _healthBar.Value = 100;
            var gui = new UISequentialContainer(Vector2.Zero, new Vector2(screenWidth, screenHeight))
            {
                MainAxis = UISequentialContainer.Alignment.Start,
                CrossAxis = UISequentialContainer.Alignment.Center,
            };
            ui.OnResized += () => gui.Size = ui.Size;
            ui.Add(gui);
            gui.Add(new UIMarginContainer(_healthBar, 15));
            return ui;
        }

        public override void Dispose()
        {
            _egg.Dispose();
            _cake.Dispose();
            _die.Dispose();
            _duck.Dispose();
            base.Dispose();
        }
    }
}

