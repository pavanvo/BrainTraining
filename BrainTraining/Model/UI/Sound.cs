using BrainTraining.Properties;
using System.Collections.Generic;
using System.Media;

namespace BrainTraining.Model.UI {

    /// <summary>
    /// Варианты звуков
    /// </summary>
    enum SoundType {
        Button,
        Error,
        TimeIsUp,
        Good,
        Win,
    }

    /// <summary>
    /// Статичекий класс для воспроизведения звука
    /// </summary>
    static class Sound {

        /// <summary>
        /// Словарь со звкуками
        /// </summary>
        private static readonly Dictionary<SoundType, SoundPlayer> Map = new Dictionary<SoundType, SoundPlayer>(){
                {SoundType.Button, new SoundPlayer(Resources.Звук_1)},
                {SoundType.Error, new SoundPlayer(Resources.Звук_2)},
                {SoundType.TimeIsUp, new SoundPlayer(Resources.Звук_3)},
                {SoundType.Good, new SoundPlayer(Resources.Звук_4)},
                {SoundType.Win, new SoundPlayer(Resources.Звук_5)},
            };

        /// <summary>
        /// Воспроизвести
        /// </summary>
        public static void Play(SoundType soundType) {
            var player = Map[soundType];
            player.Stop();
            player.Play();
        }
    }
}
