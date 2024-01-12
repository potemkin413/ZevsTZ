namespace ETL
{
    internal class Program
    {
        private static List<string> countries = new List<string>();

        static APIData _apiData;
        static void Main(string[] args)
        {
            Console.Write("Введите названия желаемых стран, после ввода каждой страны требуется нажать ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("enter");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\r\nЧтобы запустить по случайному списку стран (10) просто нажми ");
            Console.ForegroundColor= ConsoleColor.Green;
            Console.Write("enter");
            Console.ResetColor();

            Console.WriteLine();

            var enteredString = Console.ReadLine();
            if (!string.IsNullOrEmpty(enteredString))
            {
                while (enteredString != "go") 
                {
                    if (enteredString == "go")
                        break;
                    if (enteredString != "")
                        countries.Add(enteredString);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Страна: {enteredString} - добавлена в список");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("введите");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(" go ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Для запуска ETL");
                    Console.ResetColor();

                    enteredString = Console.ReadLine();

                }
            }

            if (!countries.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Запуск будет выполнен по случайному списку стран");
                Console.ResetColor();
                
                CountrieListGenerator generator = new CountrieListGenerator();
                generator.GetRandomCountries(ref countries);
            }

            Console.WriteLine("Необходимо задать кол-во потоков, которые мы будем использовать для получения данных, введите число");
            var countOfTasksInput = Console.ReadLine();
            int countOfTasks;
            var isValueRight = int.TryParse(countOfTasksInput, out countOfTasks);
            while (!isValueRight) 
            {
                Console.WriteLine("Вы ввели недопустимое значение, попробуйте еще раз");
        
                isValueRight = int.TryParse(Console.ReadLine(), out countOfTasks);
            };

            _apiData = new APIData(countries, countOfTasks);

            Writer.Save(_apiData.GetData());
        }
    }
}