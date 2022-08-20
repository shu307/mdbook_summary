using System.Text;

namespace mdbook_summary
{
    internal static class IOTools
    {
        private static readonly char[] unsafeChars = { ';', '/', '?', ':', '@', '=', '&', '<', '>', '"', '#', '%', '{', '}', '|', '\\', '^', '~', '[', ']', '`', ' ' };
        private const string summaryFilename = "SUMMARY.md";
        private static string summaryFullName = $"{Program.BaseDir}{summaryFilename}";
        private static StringBuilder stringBuilder = new(128);
        private static Queue<string> stringQueue = new(128);
        public static Queue<string> unsafeStrings = new(128);
        static IOTools()
        {
            stringQueue.Enqueue($"# Summary{Environment.NewLine}");
        }
        /// <summary>
        /// 遍历文件,加入队列
        /// </summary>
        /// <param name="filSystemInfos">当前目录所有对象</param>
        /// <param name="depth">当前目录深度</param>
        internal static void GetAllFileSystems(FileSystemInfo[]? filSystemInfos, int depth)
        {
            if (filSystemInfos != null)
            {
                foreach (FileSystemInfo fs in filSystemInfos)
                {
                    if ((fs.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        CheckUnsafeChar(fs.Name);
                        EnqueueLine(fs.Name, depth);
                        GetAllFileSystems(((DirectoryInfo)fs).GetFileSystemInfos(), depth + 1);
                    }
                    else
                    {
                        if (fs.Extension == ".md" && fs.Name != summaryFilename)
                        {
                            CheckUnsafeChar(fs.Name);
                            EnqueueLine(fs.Name, fs.FullName, depth);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="name">文件名</param>
        /// <param name="fullname">文件完整路径</param>
        /// <param name="depth">所在深度</param>
        private static void EnqueueLine(string name, string fullname, int depth)
        {
            stringBuilder.Clear();
            // 缩进
            stringBuilder.Append(' ', depth * 4);
            //    - [Chapter 1.md]
            stringBuilder.Append($"- [{name}]");
            //    - [Chapter 1]
            stringBuilder.Replace(".md", "");
            //    - [Chapter 1](basedir/chapter_1.md)
            stringBuilder.Append($"({fullname})");
            //    - [Chapter 1](chapter_1.md)
            stringBuilder.Replace(Program.BaseDir, "");
            stringQueue.Enqueue(stringBuilder.ToString());
        }
        /// <summary>
        /// 写入文件夹
        /// </summary>
        /// <param name="name">文件夹名</param>
        /// <param name="depth">所在深度</param>
        private static void EnqueueLine(string name, int depth)
        {
            stringBuilder.Clear();
            // 缩进
            stringBuilder.Append(' ', depth * 4);
            //    - [Chapter 1]()
            stringBuilder.Append($"- [{name}]()");
            stringQueue.Enqueue(stringBuilder.ToString());
        }
        /// <summary>
        /// 写入队列
        /// </summary>
        public static void WriteStream()
        {
            try
            {
                Console.WriteLine($"Target File: {summaryFullName}");
                Console.WriteLine("------");
                using StreamWriter sw = new(summaryFullName, false);
                foreach (string str in stringQueue)
                {
                    Console.WriteLine(str);
                    sw.WriteLine(str);
                }
                stringQueue.Clear();
            }
            catch (Exception e)
            {
                Program.SendMessage("Failed, Please check is the file ReadOnly", e);
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">文件(夹)名</param>
        /// <returns></returns>
        public static void CheckUnsafeChar(string name)
        {
            foreach (char c in unsafeChars)
            {
                if (name.Contains(c))
                {
                    unsafeStrings.Enqueue(name);
                }
            }
        }
    }
}
