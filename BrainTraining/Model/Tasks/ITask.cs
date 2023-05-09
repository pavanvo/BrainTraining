using BrainTraining.Model.UI;
using System;
using System.Windows.Forms;

namespace BrainTraining.Model {
    public interface ITask {
        string Name { get; }
        MainForm MainForm { get; }

        Button ButtonBack { get; }

        Panel Header { get; }
        Panel Content { get; }
        Panel Footer { get; }

        int HeaderHeight { get; }

        int ContentHeight { get; }

        int FooterHeight { get; }

        Action Setup { get; }


    }
}
