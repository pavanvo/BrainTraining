using BrainTraining.Controls;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrainTraining.Model {
    /// <summary>
    /// Интерейс Задача
    /// </summary>
    public interface ITask {

        /// <summary>
        /// Название задачи
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Обисание задачи
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Сcылка на Главную форму
        /// </summary>
        MainForm MainForm { get; }

        /// <summary>
        /// Кнопка возврата назад 
        /// </summary>
        RoundButton ButtonBack { get; }

        /// <summary>
        /// Верхняя Панель (Шапка)
        /// </summary>
        Panel Header { get; }

        /// <summary>
        /// Средняя панель с содержанием
        /// </summary>
        Panel Content { get; }

        /// <summary>
        /// Нихжняя панель (Подвал)
        /// </summary>
        Panel Footer { get; }

        /// <summary>
        /// Высота верхней панели
        /// </summary>
        int HeaderHeight { get; }

        /// <summary>
        /// Высота Содержания
        /// </summary>
        int ContentHeight { get; }

        /// <summary>
        ///  Высота нижней панели
        /// </summary>
        int FooterHeight { get; }

        /// <summary>
        /// Функция Установки Задачи
        /// </summary>
        Task Setup();
    }
}
