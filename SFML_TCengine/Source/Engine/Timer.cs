using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCEngine
{
    class Timer
    {
        float m_ellapsed = 0.0f;
        float m_period = 1.0f;
        public Timer(float i_deltaSeconds)
        {
            Debug.Assert(i_deltaSeconds > 0.0f);
            m_period = i_deltaSeconds;
        }

        public bool Update(float i_dt)
        {
            m_ellapsed += i_dt;
            return Ready();
        }

        public bool Ready()
        {
            return m_ellapsed >= m_period;
        }

        public void Reset()
        {
            m_ellapsed = 0.0f;
        }
    }
}
