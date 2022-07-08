using NAudio.Wave;

namespace HuntTheWumpus.Models
{
    public class GameAudio
    {
        private string MediaPath { get; } = "Media/Audio/";
        public string Win { get; set; }
        public string Lose { get; set; }
        public string[] Ambients { get; set; }
        public string Opening { get; set; }

        public GameAudio(string name)
        {
            Ambients = new string[2];
            Ambients[0] = MediaPath + "Ambient-1.wav";
            Ambients[1] = MediaPath + "Ambient-2.wav";

            Win = MediaPath + "Win-" + name + ".wav";
            Lose = MediaPath + "Lose-" + name + ".wav";
            Opening = MediaPath + "Opening-" + name + ".wav";
        }
        public void PlayAudio(string soundLocation)
        {
            try
            {
                var audioFile = new AudioFileReader(soundLocation);
                var outputDevice = new WaveOutEvent();

                outputDevice.Init(audioFile);
                outputDevice.Play();
            }
            catch
            {
                //Something is wrong with audio
                //So continue running with no audio
            }
        }
    }
}
