using SFML.Graphics;
using SFML.System;


namespace TCEngine
{
    public class GameOverScreen
    {
        private RenderWindow m_Window;
        private Font m_Font;
        private Text m_GameOverText;

        public GameOverScreen(RenderWindow window)
        {
            SoundManager gameOverMusic = new SoundManager();
            m_Window = window;

   
            m_Font = new Font("arial.ttf"); 

            m_GameOverText = new Text("Game Over", m_Font, 50);
            m_GameOverText.Position = new Vector2f((window.Size.X - m_GameOverText.GetLocalBounds().Width) / 2, (window.Size.Y - m_GameOverText.GetLocalBounds().Height) / 2);
            m_GameOverText.FillColor = Color.Red;
            gameOverMusic.PlayMusic("Data/Sounds/GameOver.wav", true);
        }

        public void ShowGameOverScreen()
        {

            while (m_Window.IsOpen)
            {
                m_Window.Clear();

                m_Window.Draw(m_GameOverText);

                m_Window.Display();
            }
        }
    }
}