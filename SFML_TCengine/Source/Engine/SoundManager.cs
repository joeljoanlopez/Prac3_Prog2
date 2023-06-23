using SFML.Audio;
using System.Collections.Generic;
using System.Media;

namespace TCEngine
{
    public class SoundManager
    {
        private List<Sound> m_Sounds = new List<Sound>();

        public void Update()
        {
            var soundsToRemove = m_Sounds.FindAll(x => x.Status == SoundStatus.Stopped);
            soundsToRemove.ForEach(x => x.Dispose());
            m_Sounds.RemoveAll(soundsToRemove.Contains);
        }

        public void PlaySound(string _soundName, float _volume = 100.0f)
        {
            SoundBuffer buffer = new SoundBuffer(_soundName);
            Sound sound = new Sound(buffer);
            sound.Volume = _volume;
            sound.Play();
            m_Sounds.Add(sound);
        }
        public void PlayMusic (string _musicName, float _volume = 100f)
        {
                SoundBuffer buffer = new SoundBuffer(_musicName);
                Sound music = new Sound(buffer);
                music.Volume = _volume;
                music.Play();
                m_Sounds.Add(music);
           
        }
    }
}
