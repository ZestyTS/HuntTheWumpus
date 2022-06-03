﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    internal class GameAudio
    {
        private string MediaPath { get; } = "Media/Audio/";
        public string Win { get; set; }
        public string Lose { get; set; }
        public string[] Ambients { get; set; }
        public string Opening { get; set; }

        public GameAudio(int themeSelection)
        {
            Ambients = new string[2];
            Ambients[0] = MediaPath + "Ambient-1.wav";
            Ambients[1] = MediaPath + "Ambient-2.wav";

            //Baddie
            if (themeSelection == 2)
            {
                Win = MediaPath + "Win-Baddie.wav";
                Opening = MediaPath + "Opening-Baddie.wav";
                Lose = MediaPath + "Lose-Baddie.wav"; 
            }
            //Wumpus (Default)
            else
            {
                Win = MediaPath + "Win-Wumpus.wav";
                Lose = MediaPath + "Lose-Wumpus.wav";
                Opening = MediaPath + "Opening-Wumpus.wav";
            }
        }
    }
}