using System.Text.Json;

namespace BankApp.db.io
{
    public class FileManager
    {


        private string path { get; set; }
        private bool append { get; set; }

        public FileManager(string path, bool append)
        {
            this.path = path;
            this.append = append;
        }

        public void Write(string text)
        {
            try
            {
                using var sw = new StreamWriter(path, append);
                sw.Write(text);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                Write(text);
            }
            catch (FileNotFoundException)
            {
                File.Create(path);
                Write(text);
            }
        }

        public static List<T> ReadAll<T>(string path)
        {
            var arr = new List<T>();
            try
            {
                using var sw = new StreamReader(path);
                var arrStr = sw.ReadToEnd().Split(" ,\n, ");
                foreach (var item in arrStr)
                {
                    if (item != null && !item.Equals("")) arr.Add(JsonSerializer.Deserialize<T>(item));
                }
            }
            catch (Exception)
            {
            }
            return arr;
        }
    }
}