using System;
using System.Collections.Generic;
using System.Text;

namespace StoreProject
{
    public static class FileHelper
    {
        private static readonly Encoding Utf8NoBom =  new UTF8Encoding(encoderShouldEmitUTF8Identifier:false);
        public static void Creat(string path, string content)
        {
            var full = Normalize(path);
            EnsureDirectory(full);
            File.WriteAllText(full, content ?? string.Empty, Utf8NoBom);
        }
        public static void Append(string path , string content , bool addNewLine = false)
        {
            var full = Normalize(path);
            EnsureDirectory(full);

            var payLoad = addNewLine ? (content?? string.Empty) + Environment.NewLine : (content ?? string.Empty);

            File.AppendAllText(full, payLoad, Utf8NoBom);
        }
        public static string ReadAll(string path)
        {
            var full = Normalize(path);
            if(!File.Exists(full))
            {
                EnsureDirectory(full);
                File.WriteAllText(full, string.Empty, Utf8NoBom);
                return string.Empty;
            }
            return File.ReadAllText(full, Utf8NoBom);
        }
        public static string[] ReadAllLines(string path)
        {
            var full = Normalize(path);
            if (!File.Exists(full))
            {
                EnsureDirectory(full);
                File.WriteAllText(full, string.Empty, Utf8NoBom);
                return new string [1];
            }
            return File.ReadAllLines(full, Utf8NoBom);
        }
        public static bool Delete (string path)
        {
            var full = Normalize(path);
            if (!File.Exists(full)) return false;
            File.Delete(full);
            return true;
        }
        public static bool TryRead(string path, out string? content)
        {
            try { content = ReadAll(path); return true; }
            catch { content =  null; return false;  }
        }
        public static bool TryCreat(string path,  string content)
        {
            try {  Creat(path , content); return true; }
            catch { content = null; return false; }
        }

        //---Internals-----
        private static string Normalize(string path) =>
            Path.GetFullPath(path) ?? throw new ArgumentNullException(nameof(path));
        private static void EnsureDirectory(string fullPath)
        {
            var directory = Path.GetDirectoryName(fullPath);
            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
    }
}
