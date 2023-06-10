using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Diagnostics;
using System.Threading;

namespace TCEngine
{

    public class TecnoCampusEngine
    {
        //
        // Members
        //  
        public const uint WINDOW_WIDTH = 1194;
        public const uint WINDOW_HEIGHT = 825;

        private const byte FRAME_RATE = 60;
        private const float FIXED_DELTA_TIME = 1.0f / FRAME_RATE;
        private const float MAX_DELTA_TIME = 5.0f;
        private const int MAX_FRAMES_WITHOUT_RENDERING = 5;

        private static TecnoCampusEngine ms_Instance;
        private RenderWindow m_Window;
        private View m_View;
        private DebugManager m_DebugManager;
        private Scene m_Scene;

        //
        // Accessors
        //
        public static TecnoCampusEngine Get
        {
            get
            {
                if( ms_Instance == null)
                {
                    ms_Instance = new TecnoCampusEngine();
                }
                return ms_Instance;
            }
        }

        public RenderWindow Window 
        {
            get => m_Window;
        }

        public Vector2f ViewportSize
        {
            get => m_View.Size;
            set => m_View.Reset(new FloatRect(0.0f, 0.0f, value.X, value.Y));
        }


        public DebugManager DebugManager
        {
            get => m_DebugManager;
        }

        public Scene Scene
        {
            get => m_Scene;
        }


        // 
        // Methods
        //
        private TecnoCampusEngine()
        {
        
        }


        private void Init()
        {
            
            VideoMode videoMode = new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT);
            m_Window = new RenderWindow(videoMode, "Game");
            m_Window.SetVerticalSyncEnabled(true);

            m_View = new View(new FloatRect(0f, 0f, videoMode.Width, videoMode.Height));
            m_View.Viewport = new FloatRect(0f, 0f, 1f, 1f);

            m_DebugManager = new DebugManager();
            m_DebugManager.Init();

            m_Scene = new Scene();

            m_Window.Closed += (object i_sender, EventArgs i_args) => { m_Window.Close(); };
        }



        private void DeInit()
        {
            m_DebugManager.DeInit();
            m_Window.Dispose();
        }

        private void Update(float _dt)
        {
            m_Window.DispatchEvents();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                m_Window.Close();
            }

            m_DebugManager.Update(_dt);
            m_Scene.Update(_dt);
        }

        private void Draw()
        {
            Color ClearColor = new Color(40, 40, 40); 
            Window.Clear(ClearColor);
            Window.SetView(m_View);

            Window.Draw(m_Scene);
            Window.Draw(m_DebugManager);

            Window.Display();
        }

        private void EndFrame()
        {
            m_Scene.EndFrame();
        }

        private bool IsAlive()
        {
            return m_Window.IsOpen;
        }

        public void Run(Game _game)
        {
            Debug.Assert(_game != null, "The _game parameter of type Game cannot be null");

            Init();
            _game.Init(m_Window);

            DateTime startGameTime = DateTime.Now;
            double nextTime = (DateTime.Now - startGameTime).TotalMilliseconds / 1000.0;
            DateTime lastFrameTime = DateTime.Now;
            while (IsAlive())
            {
                DateTime begin = DateTime.Now;
                TimeSpan delta = begin - lastFrameTime;
                lastFrameTime = begin;

                Update(FIXED_DELTA_TIME);
                _game.Update(FIXED_DELTA_TIME);
                Draw();

                DateTime end = DateTime.Now;
                TimeSpan sleepTime = TimeSpan.FromSeconds(FIXED_DELTA_TIME) - (end - begin);
                if (sleepTime.TotalMilliseconds < 0)
                {
                    
                }
                while (sleepTime.TotalMilliseconds > 0)
                {
                    end = DateTime.Now;
                    sleepTime = TimeSpan.FromSeconds(FIXED_DELTA_TIME) - (end - begin);
                }
                EndFrame();
            }

            _game.DeInit();
            DeInit();
        }
    }
}
