namespace OldFilesCleaner
{
    public class Cleaner
    {
        public void Clean(string Path)
        {
            try
            {
                var directory = new DirectoryInfo(Path);
                if (directory.Exists)
                {
                    foreach (DirectoryInfo dir in directory.GetDirectories())
                    {
                        try
                        {
                            Clean(dir.FullName);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Console.WriteLine("Недопустимое действие или недостаточно прав на выполнение.");
                            continue;
                        }
                    }
                    FileInfo[] files = directory.GetFiles();
                    if (files.Length > 0)
                    {
                        foreach (FileInfo file in files)
                        {
                            try
                            {
                                if (file.LastAccessTime.AddMinutes(30.0) < DateTime.Now)
                                    file.Delete();
                            }
                            catch (UnauthorizedAccessException)
                            {
                                Console.WriteLine("Недопустимое действие или недостаточно прав на выполнение.");
                                continue;
                            }
                        }
                    }
                    if (directory.GetFiles().Length == 0 && directory.GetDirectories().Length == 0) directory.Delete();
                }
                else
                {
                    Console.WriteLine("Указанный каталог не существует");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
