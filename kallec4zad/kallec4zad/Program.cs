using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace kallec4zad
{
    class MusicCatalog
    {
        static Hashtable catalog = new Hashtable();

        static void Main()
        {
            LoadCatalog();
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить диск");
                Console.WriteLine("2. Удалить диск");
                Console.WriteLine("3. Добавить песню на диск");
                Console.WriteLine("4. Удалить песню с диска");
                Console.WriteLine("5. Просмотреть содержимое каталога");
                Console.WriteLine("6. Просмотреть содержимое диска");
                Console.WriteLine("7. Поиск записей по исполнителю");
                Console.WriteLine("0. Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddDisk();
                        SaveCatalog();
                        break;
                    case "2":
                        RemoveDisk();
                        SaveCatalog();
                        break;
                    case "3":
                        AddSong();
                        SaveCatalog();
                        break;
                    case "4":
                        RemoveSong();
                        SaveCatalog();
                        break;
                    case "5":
                        ViewCatalog();
                        break;
                    case "6":
                        ViewDisk();
                        break;
                    case "7":
                        SearchByArtist();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void AddDisk()
        {
            Console.WriteLine("Введите название диска:");
            string diskName = Console.ReadLine();
            Console.WriteLine("Введите исполнителя:");
            string artist = Console.ReadLine();
            ArrayList songs = new ArrayList();
            catalog[diskName] = new Disk(artist, songs);
            Console.WriteLine($"Диск {diskName} добавлен в каталог.");
        }

        static void RemoveDisk()
        {
            Console.WriteLine("Введите название диска:");
            string diskName = Console.ReadLine();
            if (catalog.ContainsKey(diskName))
            {
                catalog.Remove(diskName);
                Console.WriteLine($"Диск {diskName} удален из каталога.");
            }
            else
            {
                Console.WriteLine($"Диск {diskName} не найден в каталоге.");
            }
        }

        static void AddSong()
        {
            Console.WriteLine("Введите название диска:");
            string diskName = Console.ReadLine();
            if (catalog.ContainsKey(diskName))
            {
                Disk disk = (Disk)catalog[diskName];
                Console.WriteLine("Введите название песни:");
                string songName = Console.ReadLine();
                disk.Songs.Add(songName);
                Console.WriteLine($"Песня {songName} добавлена на диск {diskName}.");
            }
            else
            {
                Console.WriteLine($"Диск {diskName} не найден в каталоге.");
            }
        }

        static void RemoveSong()
        {
            Console.WriteLine("Введите название диска:");
            string diskName = Console.ReadLine();
            if (catalog.ContainsKey(diskName))
            {
                Disk disk = (Disk)catalog[diskName];
                Console.WriteLine("Введите название песни:");
                string songName = Console.ReadLine();
                if (disk.Songs.Contains(songName))
                {
                    disk.Songs.Remove(songName);
                    Console.WriteLine($"Песня {songName} удалена с диска {diskName}.");
                }
                else
                {
                    Console.WriteLine($"Песня {songName} не найдена на диске {diskName}.");
                }
            }
            else
            {
                Console.WriteLine($"Диск {diskName} не найден в каталоге.");
            }
        }
        static void ViewCatalog()
        {
            Console.WriteLine("Каталог:");
            foreach (DictionaryEntry entry in catalog)
            {
                Disk disk = (Disk)entry.Value;
                Console.WriteLine($"{entry.Key} ({disk.Artist}):");
                foreach (string song in disk.Songs)
                {
                    Console.WriteLine($"- {song}");
                }
            }
        }

        static void ViewDisk()
        {
            Console.WriteLine("Введите название диска:");
            string diskName = Console.ReadLine();
            if (catalog.ContainsKey(diskName))
            {
                Disk disk = (Disk)catalog[diskName];
                Console.WriteLine($"{diskName} ({disk.Artist}):");
                foreach (string song in disk.Songs)
                {
                    Console.WriteLine($"- {song}");
                }
            }
            else
            {
                Console.WriteLine($"Диск {diskName} не найден в каталоге.");
            }
        }

        static void SearchByArtist()
        {
            Console.WriteLine("Введите имя исполнителя:");
            string artist = Console.ReadLine();
            bool found = false;
            foreach (DictionaryEntry entry in catalog)
            {
                Disk disk = (Disk)entry.Value;
                if (disk.Artist == artist)
                {
                    if (!found)
                    {
                        Console.WriteLine($"Записи исполнителя {artist}:");
                        found = true;
                    }
                    Console.WriteLine($"{entry.Key}:");
                    foreach (string song in disk.Songs)
                    {
                        Console.WriteLine($"- {song}");
                    }
                }
            }
            if (!found)
            {
                Console.WriteLine($"Записи исполнителя {artist} не найдены в каталоге.");
            }
        }
        static void LoadCatalog()
        {
            if (File.Exists("catalog.txt"))
            {
                using (StreamReader reader = new StreamReader("catalog.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        string diskName = parts[0];
                        string artist = parts[1];
                        string[] songs = parts[2].Split(',');
                        ArrayList songList = new ArrayList(songs);
                        catalog[diskName] = new Disk(artist, songList);
                    }
                }
            }
        }

        static void SaveCatalog()
        {
            using (StreamWriter writer = new StreamWriter("catalog.txt"))
            {
                foreach (DictionaryEntry entry in catalog)
                {
                    Disk disk = (Disk)entry.Value;
                    string diskName = (string)entry.Key;
                    string artist = disk.Artist;
                    string songList = string.Join(",", disk.Songs.ToArray());
                    writer.WriteLine($"{diskName}|{artist}|{songList}");
                }
            }
        }

    }
    class Disk
    {
        public string Artist { get; set; }
        public ArrayList Songs { get; set; }
        public Disk(string artist, ArrayList songs)
        {
            Artist = artist;
            Songs = songs;
        }
    }
}
