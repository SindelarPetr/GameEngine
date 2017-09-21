namespace GameEngine.Menu
{
	/*class DisplayFramerate : DrawableGameComponent
    {
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;

        private int frameRate = 0;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;

        public DisplayFramerate(Game game)
            : base(game)
        {
            this.contentManager = new ContentManager(game.Services);
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteFont = contentManager.Load<SpriteFont>("Content\\fnt");
        }
        protected override void UnloadContent()
        {
            contentManager.Unload();
            contentManager = new ContentManager(Game.Services);
        }

        public override void Update()
        {
            elapsedTime += Options.GameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            frameCounter++;

            string fps = "fps: " + frameRate.ToString();

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, fps, new Vector2(420, 420), Color.White);
            spriteBatch.DrawString(spriteFont, fps, new Vector2(421, 421), Color.Red);
            spriteBatch.End();

            Game.GraphicsDevice.BlendState = BlendState.Opaque;
            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        }
    }*/
}
