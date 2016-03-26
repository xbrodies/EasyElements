using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EasyElements.Configs
{
    public static class ConfigConverter
    {
        /// <summary>
        /// Преобразовывает sEdit конфиги в easyElements конфиг с учётом версий
        /// Если конфигов много, может занять некоторое кол-во времени
        /// Старайтесь чтобы у конфига было название примерно такое: PW_1.2.4_v6.cfg
        /// Чтобы можно было определить версию
        /// </summary>
        /// <param name="files">Пути к файлам конфигураций sEdit</param>
        /// <returns>EasyElements конфиг</returns>
        public static Config sEditToEasyElements(string[] files)
        {
            if (files == null || files.Length == 0)
                throw new ArgumentException(nameof(files));

            var el = new SeleditConfigReader(files.First(), GetVersion(files.First())).Open();

            foreach (var file in files)
            {
                var buf = new SeleditConfigReader(file, GetVersion(file)).Open();

                foreach (var bList in buf.Lists)
                {
                    foreach (var eList in el.Lists)
                    {
                        if (eList.Name == bList.Name)
                        {
                            eList.Version = bList.Version;

                            foreach (var bTypes in bList.Types)
                            {
                                foreach (var eTypes in eList.Types)
                                {
                                    if (eTypes.Name == bTypes.Name)
                                        eTypes.Version = bTypes.Version;
                                }
                            }
                        }
                    }
                }
            }
            return el;
        }

        /// <summary>
        /// Получает версию sEdit конфига из названия файла
        /// </summary>
        /// <param name="path">Название конфига sEdit</param>
        /// <returns>Версия elements</returns>
        private static int GetVersion(string path)
        {
            return int.Parse(path.Split('v')[1].Split('.')[0]);
        }
    }
}
