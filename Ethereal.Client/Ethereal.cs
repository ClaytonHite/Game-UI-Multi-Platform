using Ethereal.Client.Source.Engine;
using Ethereal.Client.Source.Engine.Input;
using Ethereal.Client.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Ethereal.Client.SDL;
using System;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;

namespace Ethereal.Client
{
    public delegate void WindowEventHandler(object obj);
    public class Ethereal : Game
    {
        #region UpdateWhileWindowMoving
        bool _started = false;
        bool _isMaximized = false;
        SDL_EventFilter _filter = null!;
        [DllImport("user32.dll", ExactSpelling = true)]
        static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);
        delegate void TimerProc(IntPtr hWnd, uint uMsg, IntPtr nIDEvent, uint dwTime);
        IntPtr _handle;
        TimerProc _timerProc = null!;
        int _manualTickCount = 0;
        bool _manualTick;
        #endregion
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BaseWindow _currentWindow;
        public Basic2DSprite cursor;
        public static WindowEventHandler WindowEvent;
        private bool _startup = true;
        private float _timerDuration;
        private bool IsWindowResizing = false;
        public Ethereal()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            WindowEvent = HandleWindowEvent;
        }

        protected override void Initialize()
        {
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowSizeChanged;
            SDL_SetWindowMinimumSize(Window.Handle, 1600, 900);
            _filter = new SDL_EventFilter(HandleSDLEvent);
            SDL_AddEventWatch(_filter, IntPtr.Zero);
            _timerDuration = 1000;
            _timerProc = BackupTick;
            _handle = SetTimer(IntPtr.Zero, IntPtr.Zero, 1, _timerProc);
            // TODO: Add your initialization logic here
            Globals.Content = Content;
            Globals.DefaultFont = Content.Load<SpriteFont>("Fonts/DefaultFont");
            Globals.DefaultSmallFont = Content.Load<SpriteFont>("Fonts/DefaultFontSmall");
            Globals.DefaultLargeFont = Content.Load<SpriteFont>("Fonts/DefaultFontLarge");
            if (_startup)
            {
                Globals.ScreenWidth = 1600;
                Globals.ScreenHeight = 900;
                _graphics.PreferredBackBufferWidth = 1600;
                _graphics.PreferredBackBufferHeight = 900;
            }

            Window.Title = "Ethereal";
            _graphics.ApplyChanges();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            if (_startup)
            {
                _currentWindow = new IntroWindow(_spriteBatch, Content);
                _currentWindow.Initialize();
                _startup = false;
            }
            cursor = new Basic2DSprite("2D/Misc/MouseCursor", new Vector2(0, 0), new Vector2(28, 28), new SpriteEffects());
            Globals.ScreenWidth = _graphics.PreferredBackBufferWidth;
            Globals.ScreenHeight = _graphics.PreferredBackBufferHeight;
            Globals.Keyboard = new ClientKeyboard();
            Globals.Mouse = new ClientMouse();
            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            IsWindowResizing = false;
            _started = true;
            if (!_manualTick)
            {
                _manualTickCount = 0;
            }
            Globals.GameTime = gameTime;
            Globals.Keyboard.Update();
            Globals.Mouse.Update();

            _currentWindow.Update(gameTime);

            Globals.Keyboard.UpdateOld();
            Globals.Mouse.UpdateOld();
            // TODO: Add your update logic here
            string gameDuration = (_currentWindow.GameDuration != null) ? _currentWindow.GameDuration : "null";
            string currentFPS = (_currentWindow.FPS != null) ? _currentWindow.FPS : "null";
            Window.Title = $"Ethereal --- FPS : {currentFPS} --- Game Duration : {gameDuration} ";
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _currentWindow.Draw(gameTime);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        protected virtual void HandleWindowEvent(object obj)
        {
            switch ((string)obj)
            {
                case "MapEditor":
                    this.UnloadContent();
                    this.Initialize();
                    _currentWindow = new MapEditor(_spriteBatch, Content);
                    this.LoadContent();
                    break;
                case "SpriteEditor":
                    this.UnloadContent();
                    this.Initialize();
                    _currentWindow = new SpriteEditor(_spriteBatch, Content);
                    this.LoadContent();
                    break;
                case "Exit":
                    this.Exit();
                    break;
            }
        }

        private void WindowSizeChanged(object? sender, EventArgs e)
        {
            _currentWindow.ScreenResize();
        }
        private unsafe int HandleSDLEvent(IntPtr userdata, IntPtr ptr)
        {
            SDL_Event* e = (SDL_Event*)ptr;

            switch (e->type)
            {
                case SDL_EventType.SDL_WINDOWEVENT:
                    switch (e->window.windowEvent)
                    {
                        case SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                            int width = e->window.data1;
                            int height = e->window.data2;
                            var p = Window.Position;
                            IsWindowResizing = true;
                            if (IsWindowResizing)
                            {
                                _graphics.PreferredBackBufferWidth = width;
                                _graphics.PreferredBackBufferHeight = height;
                                Globals.ScreenWidth = _graphics.PreferredBackBufferWidth;
                                Globals.ScreenHeight = _graphics.PreferredBackBufferHeight;
                                _graphics.ApplyChanges();
                                WindowSizeChanged(this, EventArgs.Empty);
                            }
                            Window.Position = p;
                            break;
                        case SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:
                            break;
                        case SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                            _isMaximized = true;
                            break;
                        case SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                            _isMaximized = false;
                            break;
                    }
                    break;
            }

            return 0;
        }
        private void BackupTick(IntPtr hWnd, uint uMsg, IntPtr nIDEvent, uint dwTime)
        {
            if (_started)
            {
                if (_manualTickCount > 2)
                {
                    _manualTick = true;
                    this.Tick();
                    _manualTick = false;
                }
                _manualTickCount++;
            }
        }
    }
}