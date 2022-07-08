using ManagedBass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    public class GameAudio
    {
        private string MediaPath { get; } = "Media/Audio/";
        public string Win { get; set; }
        public string Lose { get; set; }
        public string[] Ambients { get; set; }
        public string Opening { get; set; }
        private int Stream { get; set; }

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
            if (Bass.Init())
            {
                Stream = Bass.CreateStream(soundLocation);
                if (Stream != 0)
                    Bass.ChannelPlay(Stream);
            }
            else
            {
                Bass.StreamFree(Stream);
                Bass.Free();
                PlayAudio(soundLocation);
            }
        }
    }
}
