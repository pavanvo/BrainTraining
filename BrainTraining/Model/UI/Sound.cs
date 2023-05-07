using System.Collections.Generic;
using System.IO;
using System.Media;

namespace BrainTraining.Model.UI {
    enum SoundType {
        Button,
        Error,
        TimeIsUp,
        Good,
        Win,
    }
    static class Sound {
        static Dictionary<SoundType, UnmanagedMemoryStream> Map = new Dictionary<SoundType, UnmanagedMemoryStream>(){
                {SoundType.Button, Properties.Resources.Звук_1},
                {SoundType.Error, Properties.Resources.Звук_2},
                {SoundType.TimeIsUp, Properties.Resources.Звук_3},
                {SoundType.Good, Properties.Resources.Звук_4},
                {SoundType.Win, Properties.Resources.Звук_5},
            };

        static SoundPlayer player = new SoundPlayer();


        public static void Play(SoundType soundType) {
            var stream = Map[soundType];
            player.Stream = stream;
            player.PlaySync();
        }
    }
}
