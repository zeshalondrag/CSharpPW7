namespace ConsoleApp10
{
    internal static class FilesnDirectories
    {
        public static void Lobby(ConsoleKeyInfo key, string path, string path2)
        {
            if (key.Key == ConsoleKey.F1)
            {
                DirectoriesCreate(path);
            }

            else if (key.Key == ConsoleKey.F2)
            {
                FilesCreate(path);
            }

            else if (key.Key == ConsoleKey.F3)
            {
                Delete(path, path2);
            }
        }

        static void FilesCreate(string path)
        {
            Console.SetCursorPosition(100, 5);
            Console.Write("Введите имя файла: ");
            Console.SetCursorPosition(100, 6);
            string name = Console.ReadLine();

            if (File.Exists(path + name))
            {
                Console.SetCursorPosition(82, 7);
                Console.Write("Внимание!");
                Console.SetCursorPosition(82, 8);
                Console.Write("Файл с таким названием уже существует.");
                Console.SetCursorPosition(82, 9);
                Console.Write("Enter - чтобы продолжить");

                ConsoleKeyInfo choice = Console.ReadKey(true);

                if (choice.Key == ConsoleKey.Enter)
                {
                    using FileStream fs = File.Create(path + name);
                }
            }

            else
            {
                using FileStream fs = File.Create(path + name);
            }
        }

        static void DirectoriesCreate(string path)
        {
            Console.SetCursorPosition(100, 5);
            Console.Write("Введите имя папки: ");
            Console.SetCursorPosition(100, 6);
            string name = Console.ReadLine();

            if (Directory.Exists(path + name))
            {
                Console.SetCursorPosition(81, 7);
                Console.Write("Внимание!");
                Console.SetCursorPosition(81, 8);
                Console.Write("Папка с таким названием уже существует.");
            }

            else
            {
                Directory.CreateDirectory(path + name);
            }
        }

        static void Delete(string path, string path2)
        {
            if (File.Exists(path2))
            {
                File.Delete(path2);
            }

            else if (Directory.Exists(path2))
            {
                Directory.Delete(path2, true);
            }
        }
    }
}
