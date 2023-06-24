using SFML.Audio;
using System.Collections.Generic;
using System.Media;
using System.Threading;

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
            
        public void PlaySound(string _soundName, bool loop, float _volume = 100.0f)
        {
            Thread soundThread = new Thread(() =>
            {
                SoundBuffer buffer = new SoundBuffer(_soundName);
                Sound sound = new Sound(buffer);
                sound.Loop = loop;
                sound.Volume = _volume;
                sound.Play();

                while (sound.Status == SoundStatus.Playing)
                {

                }

                sound.Dispose();
            });

            soundThread.Start();
        }
        public void PlayMusic (string _musicName, bool loop, float _volume = 100f)
        {
            Thread musicThread = new Thread(() =>
            {
                SoundBuffer buffer = new SoundBuffer(_musicName);
                Sound music = new Sound(buffer);
                music.Loop = loop;
                music.Volume = _volume;
                music.Play();

                while (music.Status == SoundStatus.Playing)
                {
                    // Espera activa hasta que se detenga la música
                }

                music.Dispose();
            });

            musicThread.Start();
        }
    }
}
    
