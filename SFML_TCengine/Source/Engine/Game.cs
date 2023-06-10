using SFML.Graphics;

namespace TCEngine
{
    public interface Game
    {
        void Init(RenderWindow i_window);
        void DeInit();
        void Update(float _dt);
    }
}
