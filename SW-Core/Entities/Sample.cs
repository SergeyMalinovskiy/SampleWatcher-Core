using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;

namespace SW_Core.Entities
{
    class Sample
    {
        public Types Type { get; private set; }
        public string Name = "";

        private string _fullPath;
        public string FileExtension { get; private set; }

        public string FullPath
        {
            get => _fullPath;
            set
            {
                _fullPath = File.Exists(value)
                ? value
                : throw new FileNotFoundException();
            }
        }

        private string prefix = "";
        private char divider = '_';
        private static char[] dividers = "_.- ".ToCharArray();


        public enum Types
        {
            Kick,
            Snare,
            Clap,
            Hat,
            Cymbal,
            Crash,
            Fx,
            Perc,
            Pad,
            Piano,
            Synth,
            Other,
            Ride
        }

        private static string[] SampleTypes = Enum.GetNames(typeof(Types));

        public Sample(string fullPath, Types type)
        {
            FullPath = fullPath;
            Type = type;
            Name = GetSampleName(fullPath);
            FileExtension = Path.GetExtension(fullPath);
        }

        public Sample(string fullPath)
        {
            FullPath = fullPath;
            Type = ParseType(fullPath);
            Name = GetSampleName(fullPath);
            FileExtension = Path.GetExtension(fullPath);
        }

        private string GetSampleName(string fullPath)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fullPath);
            return (Type == Types.Other 
                        ? nameWithoutExtension 
                        : nameWithoutExtension.Replace(Type.ToString(), "")
                    ).Trim(dividers);
        }

        private static Types GetTypeByTypeName(string typeName)
        {
            string titleCaseTypeName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(typeName.ToLower());
            Types foundType = (Types)Enum.Parse(typeof(Types), titleCaseTypeName);

            Console.WriteLine($"Detect type is \"{foundType}\"");
            return foundType;
        }

        #region ParseType methods
        // В данном блоке определены методы для определения типа семпла
        // Возвращает тип семпла исходя из названия семпла, т е если имя файла "FX_Sample.wav" то тип = "FX"
        public static Types ParseType(string fullPath, char charDivider = '\0')
        {
            return GetTypeByTypeName(ParseTypeR(fullPath, charDivider));
        }

        private static string ParseTypeR(string fileName, char concreteDivider = '\0')
        {
            // Уровень вложенности при рекурсии вызовов - понадобится для отслеживания иерархии типов
            int level = 0;

            // Console.WriteLine("Divider - " + concreteDivider); // #DEBUG#

            // Получаем имя файла без расширения
            fileName = Path.GetFileNameWithoutExtension(fileName);
            // Console.WriteLine(fileName);
            // Рекурсивная функция
            return ParseTypeRecurce(fileName, level, concreteDivider);

        }
        private static string ParseTypeRecurce(string fileName, int level, char concreteDivider = '\0')
        {
            string UndefinedSampleType = Types.Other.ToString();
            // Ограничение вложенности - если будет много знаков разделителей, то программа зависнет!
            const int LEVELORDER = 100;

            if (fileName != "")
            {
                // Текущий знак разделения
                char currDivider;
                // Конечный индекс для получения новой строки
                int endIndex = -1;

                // Если мы хотим парсить только через конкретно определенный символ-разделитель, то
                // проверяем чему он равен этот
                if (concreteDivider == '\0')
                {
                    // иначе если переданный символ concreteDivider РАВЕН символу-разделителю, то
                    // используем для разделения только его!
                    endIndex = fileName.IndexOfAny(dividers);
                }
                else
                {
                    // если он НЕ РАВЕН символу-разделителю по умолчанию,
                    // то записываем индекс первого попавшегося символа-разделителя,
                    // определенного в массиве символов dividers                    
                    endIndex = fileName.IndexOf(concreteDivider);
                }

                // Проверяем есть ли вообще символы - разделители в строке
                bool isAvailableDividers = ContainCharsAny(fileName, dividers);

                // Проверяем есть ли вообще символы-разделители в строке
                if (isAvailableDividers && endIndex != -1)
                {
                    // Console.WriteLine(); // #DEBUG#

                    // Получаем новую строку от начала до первого символа разделителя
                    string tmpType = fileName.Substring(0, endIndex);
                    // и запоминаем символ разделитель для текущего уровня вложенности
                    currDivider = fileName[endIndex];

                    // Оставшуюся часть строки сохраняем
                    string last = fileName.Substring(fileName.IndexOf(currDivider) + 1);

                    // Производим проверку на тип через CheckType и сохраняем в переменную
                    bool isType = CheckType(tmpType, SampleTypes);

                    // Console.WriteLine($"Last: {last}"); // #DEBUG#

                    // Если тип семпла
                    if (isType)
                    {
                        // ... то просто его возвращаем
                        // Console.WriteLine("Level: " + level + " -> find type: " + tmpType + " OK"); // #DEBUG#
                        return tmpType;
                    }
                    else
                    // Если не тип семпла и строка еще не закончилась
                    if (!isType && last.Length > 0)
                    {
                        // Console.WriteLine("Level: " + level); // #DEBUG#
                        // Контроллируем уровень вложенности
                        if (level < LEVELORDER)
                        {
                            // ... поднимаем уровень вложенности и рекурсивно вызываем эту функцию снова!
                            level++;
                            return ParseTypeRecurce(last, level, concreteDivider);
                        }
                        else
                        {
                            throw new OverflowException($"{level} equal {LEVELORDER}!");
                        }
                    }
                    else
                    {
                        // Console.WriteLine($"Not found an available type, was set {UndefinedSampleType}"); // #DEBUG#
                        return UndefinedSampleType;
                    }

                    // Если символов разделителей не было найдено
                }
                else
                {
                    // ...то, возвращаем тип неопределенного семпла - UndefinedSampleType = "OTHER"
                    // Console.WriteLine($"Not found a dividing chars, will set on default type \"{UndefinedSampleType}\" "); // #DEBUG#
                    return UndefinedSampleType;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        // Ищет тип (tmpType) из списка (array) допустимых, если такой тип допустим, то возвращает True
        private static bool CheckType(string tmpType, params string[] array)
        {
            bool isAvailable = false;

            foreach (var item in array)
            {
                // Console.WriteLine("item: " + item);
                if (item.ToUpper().Equals(tmpType.ToUpper()))
                {
                    isAvailable = true;
                    break;
                }
            }

            // Console.WriteLine("Done!");
            return isAvailable;
        }

        // Проверяет содержит ли target строка любой символ из набора символов compareChars
        public static bool ContainCharsAny(string target, char[] compareChars)
        {
            bool isContain = false;

            foreach (char item in compareChars)
            {
                isContain |= target.Contains(item.ToString());
            }


            return isContain;
        }
        #endregion

    }
}
