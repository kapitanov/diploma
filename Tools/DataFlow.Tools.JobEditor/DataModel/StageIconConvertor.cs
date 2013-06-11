using System;
using System.Globalization;
using System.Windows.Data;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    public class StageIconConvertor : IValueConverter
    {
        #region Implementation of IValueConverter

        /// <summary>
        /// Преобразует значение. 
        /// </summary>
        /// <returns>
        /// Преобразованное значение. Если метод возвращает null, используется действительное значение null.
        /// </returns>
        /// <param name="value">Значение, произведенное исходной привязкой.</param><param name="targetType">Тип свойства цели связывания.</param><param name="parameter">Параметр используемого преобразователя.</param><param name="culture">Язык и региональные параметры, используемые в преобразователе.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                switch ((StageState)value)
                {
                    case StageState.Pending:
                        return "StagePending.png";
                    case StageState.Processing:
                        return "StageProcessing.png";
                    case StageState.Failed:
                        return "StageFailed.png";
                    case StageState.Completed:
                        return "StageCompleted.png";
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Преобразует значение. 
        /// </summary>
        /// <returns>
        /// Преобразованное значение. Если метод возвращает null, используется действительное значение null.
        /// </returns>
        /// <param name="value">Значение, произведенное целью привязки.</param><param name="targetType">Тип, к которому выполняется преобразование.</param><param name="parameter">Используемый параметр преобразователя.</param><param name="culture">Язык и региональные параметры, используемые в преобразователе.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
