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
        private string ThemePath { get; set; }
        public WaveOutEvent? OutputDevice { get; set; }
        public GameAudio(string name)
        {
            Ambients = new string[2];
            Ambients[0] = MediaPath + "Ambient-1";
            Ambients[1] = MediaPath + "Ambient-2";

            ThemePath = MediaPath + name + @"/";

            Win = ThemePath + "Win-" + name;
            Lose = ThemePath + "Lose-" + name;
            Opening = ThemePath + "Opening-" + name;

            SetExtensions();
        }
        public void PlayAudio(string soundLocation)
        {
            using var audioFile = new AudioFileReader(soundLocation);
            using var loop = new LoopStream(audioFile);

            if (OutputDevice != null && OutputDevice.PlaybackState != PlaybackState.Stopped)
                OutputDevice.Stop();

            if (OutputDevice != null)
            {
                OutputDevice.Dispose();
                OutputDevice = null;
            }
            OutputDevice = new WaveOutEvent();
            OutputDevice.Init(loop);
            OutputDevice.Play();
        }
        private void SetExtensions()
        {
            var directory = Directory.GetFiles(MediaPath);
            for(var i = 0; i < directory.Length; i++)
                Ambients[i] += Path.GetExtension(directory[i]);

            directory = Directory.GetFiles(ThemePath);
            for (var i = 0; i < directory.Length; i++)
            {
                var file =  directory[i];
                var extension = Path.GetExtension(file);

                if (file.Contains("Win"))
                    Win += extension;
                else if (file.Contains("Opening"))
                    Opening += extension;
                else
                    Lose += extension;
            }
        }
    }
}
