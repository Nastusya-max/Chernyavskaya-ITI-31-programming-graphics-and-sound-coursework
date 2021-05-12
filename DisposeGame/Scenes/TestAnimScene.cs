using AmazingUILibrary;
using AmazingUILibrary.Backgrounds;
using AmazingUILibrary.Containers;
using AmazingUILibrary.Drawing;
using AmazingUILibrary.Elements;
using DisposeGame.Components;
using DisposeGame.Scripts;
using DisposeGame.Scripts.Bonuses;
using DisposeGame.Scripts.Character;
using GameEngine.Animation;
using GameEngine.Collisions;
using GameEngine.Game;
using GameEngine.Graphics;
using SharpDX;
using SharpDX.Mathematics.Interop;
using Sound;

namespace DisposeGame.Scenes
{
    public class TestAnimScene : Scene
    {
        private Camera _camera;

        private Game3DObject _player;

        private UIProgressBar _healthBar;
        private UIText _ammoCounter;

        protected override void InitializeObjects(Loader loader, SharpAudioDevice audioDevice)
        {
            //_camera = new OrthoCamera(new Vector3(0, 20, -20), rotY: MathUtil.Pi / 4f, fovY: 0.1f);
            _camera = new Camera(new Vector3(0, 60, -60), rotY: MathUtil.Pi / 4f);

            _player = CreatePlayer(loader);
            var z1 = CreateZombie(loader);
            //var z2 = CreateZombie(loader);
            //var z3 = CreateZombie(loader);

            z1.MoveTo(Vector3.UnitX * 40);
            //z2.MoveTo(Vector3.UnitX * -40);
            //z3.MoveTo(Vector3.UnitZ * -40);


            AddGameObject(_player);
            //AddGameObject(loader.LoadGameObjectFromFile(@"Models\FirstAidKit.fbx", Vector3.Zero, Vector3.Zero, @"Textures\ammo.tif"));
            AddGameObject(z1);
            //AddGameObject(z2);
            //AddGameObject(z3);

            var ammo = loader.LoadGameObjectFromFile(@"Models\BonusAmmo.fbx", Vector3.UnitX * 20, Vector3.Zero);
            ammo.Collision = new SphereCollision(2);
            ammo.AddScript(new AmmoBonusScript(_player));

            var health = loader.LoadGameObjectFromFile(@"Models\BonusHeart.fbx", Vector3.UnitZ * 20, Vector3.Zero);
            health.Collision = new SphereCollision(2);
            health.AddScript(new HealthBonusScript(_player));

            var invisibility = loader.LoadGameObjectFromFile(@"Models\BonusStels.fbx", Vector3.UnitX * -20, Vector3.Zero);
            invisibility.Collision = new SphereCollision(2);
            invisibility.AddScript(new InvisibilityBonusScript(_player));

            AddGameObject(ammo);
            AddGameObject(health);
            AddGameObject(invisibility);
        }

        private void CreatePlayer(Loader loader)
        {
            //var body = loader.LoadGameObjectFromFile(@"Models\Steve.fbx", Vector3.Zero, Vector3.Zero, @"Textures\KatolisCastle.jpg");

            //var body = loader.MakeRectangle(Vector3.Zero, Vector3.Zero, Vector3.One + Vector3.UnitY * 3 + Vector3.UnitX * 2);

            //var leftLegBone = loader.MakeRectangle(Vector3.UnitY * -4 + Vector3.UnitX * -2, Vector3.Zero, Vector3.One * 1.2f);
            //var rightLegBone = loader.MakeRectangle(Vector3.UnitY * -4 + Vector3.UnitX * 2, Vector3.Zero, Vector3.One * 1.2f);
            //var leftArmBone = loader.MakeRectangle(Vector3.UnitY * 3 + Vector3.UnitX * -4, Vector3.Zero, Vector3.One * 1.2f);
            //var rightArmBone = loader.MakeRectangle(Vector3.UnitY * 3 + Vector3.UnitX * 4, Vector3.Zero, Vector3.One * 1.2f);
            //var headBone = loader.MakeRectangle(Vector3.UnitY * 4, Vector3.Zero, Vector3.One * 1.1f);

            //body.AddChild(leftLegBone);
            //body.AddChild(rightLegBone);
            //body.AddChild(leftArmBone);
            //body.AddChild(rightArmBone);
            //body.AddChild(headBone);
            //body.AddChild(_camera);

            //var leftLeg = loader.MakeRectangle(Vector3.UnitY * -3, Vector3.Zero, Vector3.One + Vector3.UnitY * 3);
            //var rightLeg = loader.MakeRectangle(Vector3.UnitY * -3, Vector3.Zero, Vector3.One + Vector3.UnitY * 3);
            //var leftArm = loader.MakeRectangle(Vector3.UnitY * -3, Vector3.Zero, Vector3.One + Vector3.UnitY * 3);
            //var rightArm = loader.MakeRectangle(Vector3.UnitY * -3, Vector3.Zero, Vector3.One + Vector3.UnitY * 3);
            //var head = loader.MakeRectangle(Vector3.UnitY * 2, Vector3.Zero, Vector3.One * 2);

            //leftLegBone.AddChild(leftLeg);
            //rightLegBone.AddChild(rightLeg);
            //leftArmBone.AddChild(leftArm);
            //rightArmBone.AddChild(rightArm);
            //headBone.AddChild(head);

            //leftLegBone.IsHidden = true;
            //rightLegBone.IsHidden = true;
            //leftArmBone.IsHidden = true;
            //rightArmBone.IsHidden = true;
            //headBone.IsHidden = true;

            //var gun = loader.MakeRectangle(Vector3.UnitY * -3, Vector3.Zero, Vector3.One * 1.2f);
            //var bullet = loader.MakeRectangle(Vector3.Zero, Vector3.Zero, Vector3.One * 0.2f);
            //bullet.Collision = new SphereCollision(1);

            //rightArm.AddChild(gun);

            //rightArmBone.SetRotationY(-MathUtil.PiOverTwo);

            //Animation characterMovementAnimation = new Animation(new float[] { 0, MathUtil.PiOverFour, 0, -MathUtil.PiOverFour, 0 }, 1, int.MaxValue);
            //characterMovementAnimation.AddProcess(value =>
            //{
            //    leftLegBone.SetRotationY(value);
            //    rightLegBone.SetRotationY(-value);
            //    leftArmBone.SetRotationY(-value);
            //    headBone.SetRotationZ(value);
            //});
            //characterMovementAnimation.AddTransitionPaused(() =>
            //{
            //    leftLegBone.SetRotationY(0);
            //    rightLegBone.SetRotationY(0);
            //    leftArmBone.SetRotationY(0);
            //    headBone.SetRotationZ(0);
            //});

            //body.AddScript(new DinoMovementScript());
            //var physics = new PhysicsComponent();
            //body.AddComponent(physics);
            //body.AddScript(new PhysicsScript(physics));

            //body.AddScript(new PlayerMovementScript(characterMovementAnimation, physics));

            //var visibility = new VisibilityComponent();
            //body.AddComponent(visibility);
            //body.AddScript(new VisibilityScript(visibility));

            //var health = new HealthComponent(100);
            //_healthBar.MaxValue = 100;
            //_healthBar.Value = 100;
            //health.OnChanged += value => _healthBar.Value = value;
            //body.AddComponent(health);

            //var ammo = new AmmoComponent();
            //_ammoCounter.Text = ammo.Ammo.ToString();
            //ammo.OnChanged += value => _ammoCounter.Text = value.ToString();
            //body.AddComponent(ammo);

            //gun.AddScript(new PlayerGunScript(bullet, ammo));

            //body.Collision = new BoxCollision(5, 20);

            //return body;
        }

        private Game3DObject CreateZombie(Loader loader)
        {
            var body = loader.MakeRectangle(Vector3.Zero, Vector3.Zero, Vector3.One + Vector3.UnitY * 3 + Vector3.UnitX * 2);

            var leftLegBone = loader.MakeRectangle(Vector3.UnitY * -4 + Vector3.UnitX * -2, Vector3.Zero, Vector3.One * 1.2f);
            var rightLegBone = loader.MakeRectangle(Vector3.UnitY * -4 + Vector3.UnitX * 2, Vector3.Zero, Vector3.One * 1.2f);
            var leftArmBone = loader.MakeRectangle(Vector3.UnitY * 3 + Vector3.UnitX * -4, Vector3.Zero, Vector3.One * 1.2f);
            var rightArmBone = loader.MakeRectangle(Vector3.UnitY * 3 + Vector3.UnitX * 4, Vector3.Zero, Vector3.One * 1.2f);
            var headBone = loader.MakeRectangle(Vector3.UnitY * 4, Vector3.Zero, Vector3.One * 1.1f);

            body.AddChild(leftLegBone);
            body.AddChild(rightLegBone);
            body.AddChild(leftArmBone);
            body.AddChild(rightArmBone);
            body.AddChild(headBone);

            var leftLeg = loader.MakeRectangle(Vector3.UnitY * -3, Vector3.Zero, Vector3.One + Vector3.UnitY * 3);
            var rightLeg = loader.MakeRectangle(Vector3.UnitY * -3, Vector3.Zero, Vector3.One + Vector3.UnitY * 3);
            var leftArm = loader.MakeRectangle(Vector3.UnitY * -3, Vector3.Zero, Vector3.One + Vector3.UnitY * 3);
            var rightArm = loader.MakeRectangle(Vector3.UnitY * -3, Vector3.Zero, Vector3.One + Vector3.UnitY * 3);
            var head = loader.MakeRectangle(Vector3.UnitY * 2, Vector3.Zero, Vector3.One * 2);

            leftLegBone.AddChild(leftLeg);
            rightLegBone.AddChild(rightLeg);
            leftArmBone.AddChild(leftArm);
            rightArmBone.AddChild(rightArm);
            headBone.AddChild(head);

            leftLegBone.IsHidden = true;
            rightLegBone.IsHidden = true;
            leftArmBone.IsHidden = true;
            rightArmBone.IsHidden = true;
            headBone.IsHidden = true;


            Animation zombieIdleAnimation = new Animation(new float[] { 0, MathUtil.Pi / 16f, 0, -MathUtil.Pi / 16f, 0 }, 1, int.MaxValue);
            zombieIdleAnimation.AddProcess(value =>
            {
                headBone.SetRotationZ(value);
                headBone.SetRotationY(value);
                leftArmBone.SetRotationY(value - MathUtil.PiOverTwo);
                rightArmBone.SetRotationY(-value - MathUtil.PiOverTwo);
            });
            Animation movementAnimation = new Animation(new float[] { 0, MathUtil.PiOverFour, 0, -MathUtil.PiOverFour, 0 }, 1, int.MaxValue);
            movementAnimation.AddProcess(value =>
            {
                leftLegBone.SetRotationY(value);
                rightLegBone.SetRotationY(-value);
            });
            movementAnimation.AddTransitionPaused(() =>
            {
                leftLegBone.SetRotationY(0);
                rightLegBone.SetRotationY(0);
            });

            body.Collision = new BoxCollision(5, 20);
            body.AddScript(new ZombieMovementScript(_player, movementAnimation));
            body.AddComponent(new HealthComponent(100));

            return body;
        }

        protected override UIElement InitializeUI(Loader loader, DrawingContext context, int screenWidth, int screenHeight)
        {
            context.NewNinePartsBitmap("glowingBorder", loader.LoadBitmapFromFile(@"Textures\GlowingBorder.png"), 21, 29, 21, 29);
            context.NewBitmap("bulletsTexture", loader.LoadBitmapFromFile(@"Textures\Bullets.png"));
            context.NewSolidBrush("neonBrush", new RawColor4(144f / 255f, 238f / 255f, 233f / 255f, 1f));
            context.NewTextFormat("ammoFormat", 
                fontWeight: SharpDX.DirectWrite.FontWeight.Black,
                textAlignment: SharpDX.DirectWrite.TextAlignment.Center,
                paragraphAlignment: SharpDX.DirectWrite.ParagraphAlignment.Center);

            var ui = new UISequentialContainer(Vector2.Zero, new Vector2(screenWidth, screenHeight))
            {
                MainAxis = UISequentialContainer.Alignment.End,
                CrossAxis = UISequentialContainer.Alignment.Start
            };
            
            _healthBar = new UIProgressBar(Vector2.Zero, new Vector2(100, 20), "neonBrush");
            _ammoCounter = new UIText("0", new Vector2(48, 10), "ammoFormat", "neonBrush");
            var ammoImage = new UIPanel(Vector2.Zero, new Vector2(40, 36))
            {
                Background = new TextureBackground("bulletsTexture")
            };

            var ammoContainer = new UISequentialContainer(Vector2.Zero, new Vector2(100, 36), false)
            {
                MainAxis = UISequentialContainer.Alignment.Start,
                CrossAxis = UISequentialContainer.Alignment.Center
            };
            var healthContainer = new UIMarginContainer(_healthBar, 15)
            {
                Background = new NinePartsTextureBackground("glowingBorder")
            };

            ammoContainer.Add(new UIMarginContainer(ammoImage, 6, 0));
            ammoContainer.Add(_ammoCounter);

            ui.Add(ammoContainer);
            ui.Add(healthContainer);

            return ui;
        }

        protected override Renderer.IlluminationProperties CreateIllumination()
        {
            Renderer.IlluminationProperties illumination = base.CreateIllumination();
            Renderer.LightSource lightSource = new Renderer.LightSource();
            lightSource.lightSourceType = Renderer.LightSourceType.Directional;
            lightSource.color = Vector3.One;
            lightSource.direction = Vector3.Normalize(new Vector3(0.5f, -2.0f, 1.0f));
            illumination[0] = lightSource;
            return illumination;
        }

        protected override Camera CreateCamera()
        {
            return _camera;
        }
    }
}
