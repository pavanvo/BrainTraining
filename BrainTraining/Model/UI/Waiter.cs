using BrainTraining.Controls;
using BrainTraining.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace BrainTraining.Model.UI {
    public class Waiter {
        public readonly ProgressBar ProgressBar = new PavanvoBar {
            Width = ControlHelper.DefaultWidth,
            ForeColor = ControlHelper.Blue,
            BackColor = Color.WhiteSmoke
        };

        private ManualResetEvent mre;
        private Stopwatch stopWatch;
        private System.Timers.Timer timer;


        public async Task Wait(int timeout) {
            stopWatch = new Stopwatch();
            timer = new System.Timers.Timer();
            mre = new ManualResetEvent(false);

            ProgressBar.Value = 0;
            ProgressBar.Maximum = timeout;

            timer.Elapsed += (o, e) => {
                var elapsed = Convert.ToInt32(stopWatch.ElapsedMilliseconds);

                ProgressBar.Invoke(new Action(() => ProgressBar.Value = elapsed));

                if (elapsed > timeout) {
                    Sound.Play(SoundType.TimeIsUp);
                    mre.Set();
                }
            };
            timer.Interval = 10;
            stopWatch.Start();
            timer.Start();

           
            await Task.Run(() => mre.WaitOne());
        }
        public void Go() {
            stopWatch.Stop();
            timer.Stop();
            mre.Set();
        }
    }
}
