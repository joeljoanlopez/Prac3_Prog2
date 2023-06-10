using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCEngine
{
    public class AnimatedSprite : Sprite
    {
        uint m_numRows;
        uint m_numColumns;
        float m_frameTime;

        uint m_currentFrame = 0;
        float m_currentTime = 0.0f;
        bool m_loop;

        private List<IntRect> m_frames;
        public AnimatedSprite(Texture i_texture, uint i_numRows, uint i_numColumns, float i_timeBetweenFrames)
            : base(i_texture)
        {
            m_numColumns = i_numColumns;
            m_numRows = i_numRows;
            m_frameTime = i_timeBetweenFrames;
            m_frames = new List<IntRect>();
            m_loop = true;

            Initialize();
        }

        public void Update(float _deltaSeconds)
        {
            m_currentTime += _deltaSeconds;
            if (m_currentTime >= m_frameTime)
            {
                m_currentTime = m_currentTime % m_frameTime;

                if (m_loop)
                {
                    m_currentFrame = (uint)((m_currentFrame + 1) % m_frames.Count);
                }
                else
                {
                    m_currentFrame = (uint)Math.Min(m_currentFrame + 1, m_frames.Count - 1);
                }

                SetFrame(m_currentFrame);
            }
        }

        void Initialize()
        {
            m_currentFrame = 0;

            IntRect frame;
            frame.Width = (int)(this.Texture.Size.X / m_numColumns);
            frame.Height = (int)(this.Texture.Size.Y / m_numRows);
            for (uint r = 0; r < m_numRows; ++r)
            {
                for (uint c = 0; c < m_numColumns; ++c)
                {
                    frame.Top = (int)r * frame.Width;
                    frame.Left = (int)c * frame.Width;
                    m_frames.Add(frame);
                }
            }

            SetFrame(m_currentFrame);
        }

        void SetFrame(uint i_frame)
        {
            Debug.Assert(i_frame < m_frames.Count, "Frame icorrecte!");
            this.TextureRect = m_frames[(int)i_frame];
        }

        public bool loop
        {
            get
            {
                return m_loop;
            }
            set
            {
                m_loop = value;
            }
        }

        public float frameTime
        {
            get
            {
                return m_frameTime;
            }
            set
            {
                m_frameTime = value;
            }
        }

        public float animationTime
        {
            get
            {
                return m_frameTime * m_frames.Count();
            }
           
        }

        public FloatRect currentFrameRect
        {
            get
            {
                IntRect currentFrame = m_frames[(int)m_currentFrame];
                return new FloatRect(0, 0, currentFrame.Width, currentFrame.Height);
            }
        }

        public uint numColumns
        {
            get
            {
                return m_numColumns;
            }
        }

        public uint numRows
        {
            get
            {
                return m_numRows;
            }
        }
    }
}
