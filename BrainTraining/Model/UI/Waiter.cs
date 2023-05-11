using BrainTraining.Controls;
using BrainTraining.Helpers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrainTraining.Model.UI {

    /// <summary>
    /// Класс для Ожидания пользовательского ввода,
    /// отображения ожидания
    /// </summary>
    public class Waiter {

        /// <summary>
        /// Прогресс бар
        /// </summary>
        public readonly ProgressBar ProgressBar = new GoodProgressBarBar {
            Width = ControlHelper.DEFAULT_WIDTH,
            ForeColor = ControlHelper.Blue,
            BackColor = Color.WhiteSmoke
        };

        /// <summary>
        /// Лейбл времени
        /// </summary>
        public readonly Label LabelTime = new GrowLabel {
            Font = ControlHelper.BiggerFont,
            AutoSize = true,
            ForeColor = ControlHelper.Orange,
        };

        public string TimeFormat { get; set; } = "ss";

        private ManualResetEvent mre;
        private Stopwatch stopWatch;
        private System.Timers.Timer timer;

        /// <summary>
        /// Ожидание получения пользователького ввода
        /// </summary>
        public async Task Wait(int timeout = 0, bool label = false) {
            stopWatch = new Stopwatch();
            timer = new System.Timers.Timer();
            mre = new ManualResetEvent(false);

            if (timeout != 0) {
                ProgressBar.Value = 0;
                ProgressBar.Maximum = timeout;

                timer.Elapsed += (o, e) => {
                    var elapsed = Convert.ToInt32(stopWatch.ElapsedMilliseconds);

                    ProgressBar.Invoke(new Action(() => ProgressBar.Value = elapsed < ProgressBar.Maximum ? elapsed : ProgressBar.Maximum));

                    if (elapsed > timeout) {
                        Go();
                    }
                };
            }
            if (label) {
                LabelTime.Text = stopWatch.Elapsed.ToString(TimeFormat);
                LabelTime.Move2Centr();

                timer.Elapsed += (o, e) => {
                    var time = stopWatch.Elapsed;
                    if (timeout != 0) time = TimeSpan.FromMilliseconds(timeout) - stopWatch.Elapsed;


                    LabelTime.Invoke(new Action(() => LabelTime.Text = time.ToString(TimeFormat)));
                };
            }

            timer.Interval = 10;
            stopWatch.Start();
            timer.Start();

            await Task.Run(() => mre.WaitOne());
        }

        /// <summary>
        /// Сигнал получения пользователького ввода
        /// </summary>
        public void Go() {
            stopWatch.Stop();
            timer.Stop();
            mre.Set();
        }

        /// <summary>
        /// Остановка ожидания
        /// </summary>
        public void Stop() {
            stopWatch.Stop();
            timer.Stop();
        }
    }
}
