using BrainTraining.Properties;
using System.Collections.Generic;
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
        static Dictionary<SoundType, SoundPlayer> Map = new Dictionary<SoundType, SoundPlayer>(){
                {SoundType.Button, new SoundPlayer(Resources.switch_sound)},
                {SoundType.Error, new SoundPlayer(Resources.Звук_2)},
                {SoundType.TimeIsUp, new SoundPlayer(Resources.Звук_3)},
                {SoundType.Good, new SoundPlayer(Resources.Звук_4)},
                {SoundType.Win, new SoundPlayer(Resources.Звук_5)},
            };

        public static void Play(SoundType soundType) {
            var player = Map[soundType];
            player.Stop();
            player.Play();
        }
    }
}
