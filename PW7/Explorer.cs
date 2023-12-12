using System.Diagnostics;

namespace ConsoleApp10
{
    internal static class Explorer
    {
        public static void Drivers()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{"Этот компьютер",60}");
            Console.WriteLine("".PadRight(120, '-'));

            int[] pos = new int[3] { 2, 1 + DriveInfo.GetDrives().Length, 2 };

            ConsoleKeyInfo key;

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            for (int i = 0; i < allDrives.Length; i++)
            {
                Console.SetCursorPosition(0, i + 2);
                Console.Write($"{"Свободное место: " + allDrives[i].AvailableFreeSpace / 1073741824 + "ГБ из " + allDrives[i].TotalSize / 1073741824,68}");
                Console.SetCursorPosition(0, i + 2);
                Console.Write($"{"Формат диска: " + allDrives[i].DriveFormat,32}");
                Console.SetCursorPosition(0, i + 2);

                if (i == 0)
                {
                    Console.Write("->" + allDrives[i].Name);
                }

                else
                {
                    Console.Write($"  {allDrives[i].Name,2}");
                }
            }

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(0, 0);

                    for (int i = 0; i < Console.LargestWindowWidth * allDrives.Length + 2; i++)
                    {
                        Console.Write(" ");
                    }

                    FirstDirectory(allDrives, pos);
                }

                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                pos = LightArrows.Arrows(key, pos);

            } while (true);
        }

        static void FirstDirectory(DriveInfo[] allDrives, int[] pos)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"{"F1 - создать папку",118}");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"{"F2 - создать файл",117}");
            Console.SetCursorPosition(0, 4);
            Console.WriteLine($"{"F3 - удалить",112}");
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{allDrives[pos[0] - 2].Name,60}");
            Console.WriteLine("".PadRight(120, '-'));

            ConsoleKeyInfo key;

            string[] all = Directory.GetFileSystemEntries(allDrives[pos[0] - 2].Name);

            int oldPos = pos[0] - 2;

            Console.WriteLine($"{"Название",20} {"Дата создания",30} {"Тип",25}");

            int removeCount = 3;

            for (int i = 0; i < all.Length; i++)
            {
                string file = all[i];
                string file1 = file.Remove(0, removeCount);

                Console.SetCursorPosition(0, i + 3);
                Console.Write($"{Path.GetExtension(file),77}");
                Console.SetCursorPosition(0, i + 3);
                Console.Write($"{Directory.GetCreationTime(file),54}");
                Console.SetCursorPosition(0, i + 3);

                if (i == 0)
                {
                    Console.Write("->" + file1);
                }

                else
                {
                    Console.Write("  " + file1);
                }
            }

            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);

            pos[0] = 3; pos[1] = 2 + all.Length; pos[2] = 3;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    if (File.Exists(all[pos[0] - 3]))
                    {
                        Process.Start(new ProcessStartInfo { FileName = all[pos[0] - 3], UseShellExecute = true });

                        Console.SetCursorPosition(0, 5);
                        Console.WriteLine($"{"Процесс запущен",115}");
                    }

                    else if (Directory.Exists(all[pos[0] - 3]))
                    {
                        Console.SetCursorPosition(0, 0);

                        for (int i = 0; i < Console.LargestWindowWidth * all.Length + 3; i++)
                        {
                            Console.Write(" ");
                        }

                        int first = 1;
                        Directories(all, pos, first, 0, 0);
                    }
                }

                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.SetCursorPosition(0, 0);

                    for (int i = 0; i < Console.LargestWindowWidth * all.Length + 3; i++)
                    {
                        Console.Write(" ");
                    }

                    Drivers();
                }

                else if ((key.Key == ConsoleKey.F1) || (key.Key == ConsoleKey.F2) || (key.Key == ConsoleKey.F3))
                {
                    FilesnDirectories.Lobby(key, allDrives[oldPos].Name, "");

                    pos[0] = oldPos + 2;
                    FirstDirectory(allDrives, pos);
                }

                pos = LightArrows.Arrows(key, pos);

            } while (true);
        }

        static void Directories(string[] all, int[] pos, int first, int returnAdress, int startIndex)
        {
            string[] newAll = Directory.GetFileSystemEntries(all[pos[0] - 3].Remove(startIndex, returnAdress));

            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"{"F1 - создать папку",118}");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"{"F2 - создать файл",117}");
            Console.SetCursorPosition(0, 4);
            Console.WriteLine($"{"F3 - удалить",112}");
            Console.SetCursorPosition(0, 0);

            if (startIndex != 0)
            {
                startIndex--;
                returnAdress++;
            }

            Console.WriteLine($"{all[pos[0] - 3].Remove(startIndex, returnAdress),60}");
            Console.WriteLine("".PadRight(120, '-'));
            Console.WriteLine($"{"Название",20} {"Дата создания",30} {"Тип",25}");

            ConsoleKeyInfo key;

            int removeCount;

            if (!newAll.SequenceEqual(all))
            {
                removeCount = all[pos[0] - 3].Length + 1 - returnAdress;
            }

            else
            {
                removeCount = startIndex + 1;
            }

            for (int i = 0; i < newAll.Length; i++)
            {
                string file = newAll[i];
                string file1 = file.Remove(0, removeCount);

                Console.SetCursorPosition(0, i + 3);
                Console.Write($"{Path.GetExtension(file),77}");
                Console.SetCursorPosition(0, i + 3);
                Console.Write($"{Directory.GetCreationTime(file),54}");
                Console.SetCursorPosition(0, i + 3);

                if (i == 0)
                {
                    Console.Write("->" + file1);
                }

                else
                {
                    Console.Write("  " + file1);
                }
            }

            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 3);

            int oldPos = pos[0] - 3;
            pos[0] = 3; pos[1] = 2 + newAll.Length; pos[2] = 3;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    if (newAll.Length != 0)
                    {

                        if (File.Exists(newAll[pos[0] - 3]))
                        {
                            Process.Start(new ProcessStartInfo { FileName = newAll[pos[0] - 3], UseShellExecute = true });

                            Console.SetCursorPosition(0, 5);
                            Console.WriteLine($"{"Процесс запущен",115}");
                        }

                        else if (Directory.Exists(newAll[pos[0] - 3]))
                        {
                            Console.SetCursorPosition(0, 0);

                            for (int i = 0; i < Console.LargestWindowWidth * newAll.Length + 3; i++)
                            {
                                Console.Write(" ");
                            }

                            first++;
                            Directories(newAll, pos, first, 0, 0);
                        }
                    }
                }

                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.SetCursorPosition(0, 0);

                    for (int i = 0; i < Console.LargestWindowWidth * newAll.Length + 3; i++)
                    {
                        Console.Write(" ");
                    }

                    if (first == 1)
                    {
                        DriveInfo[] allDrives = DriveInfo.GetDrives();
                        pos[0] = 2; pos[1] = allDrives.Length + 1; pos[2] = 2;
                        FirstDirectory(allDrives, pos);
                    }

                    else
                    {
                        int rCount = 0;
                        int iCount = 0;

                        if (newAll.Length != 0)
                        {
                            for (int i = 1; i <= 2; i++)
                            {
                                rCount += newAll[pos[0] - 3].Split("\\")[^i].Length;
                            }

                            for (int i = 0; i < first; i++)
                            {
                                iCount += newAll[pos[0] - 3].Split("\\")[i].Length + 1;
                            }

                            rCount += 1;
                            first--;

                            Directories(newAll, pos, first, rCount, iCount);
                        }

                        else
                        {
                            rCount = all[pos[0] - 3].Split("\\")[^1].Length;

                            for (int i = 0; i < first; i++)
                            {
                                iCount += all[pos[0] - 3].Split("\\")[i].Length + 1;
                            }

                            first--;

                            Directories(all, pos, first, rCount, iCount);
                        }
                    }
                }

                else if ((key.Key == ConsoleKey.F1) || (key.Key == ConsoleKey.F2))
                {
                    string path = all[oldPos] + "\\";
                    FilesnDirectories.Lobby(key, path, "");

                    pos[0] = oldPos + 3;
                    Directories(all, pos, first, returnAdress, startIndex);
                }

                else if (key.Key == ConsoleKey.F3)
                {
                    string path2 = newAll[pos[0] - 3];
                    FilesnDirectories.Lobby(key, "", path2);

                    int rCount = newAll[pos[0] - 3].Split("\\")[^1].Length;

                    int iCount = newAll[pos[0] - 3].Length - newAll[pos[0] - 3].Split("\\")[^1].Length;

                    Directories(newAll, pos, first, rCount, iCount);
                }

                pos = LightArrows.Arrows(key, pos);

            } while (true);
        }
    }
}
