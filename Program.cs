namespace mdbook_summary
{
    internal class Program
    {
        public static string? BaseDir { get; set; }
        public static void Main(string[] args)
        {
            try
            {
                BaseDir = args.Length == 0 ? Path.GetFullPath("./src") : Path.GetFullPath(args[0]);
                if (!Path.EndsInDirectorySeparator(BaseDir))
                {
                    BaseDir += Path.DirectorySeparatorChar;
                }
                if (!Directory.Exists(BaseDir))
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                SendMessage("Invalid Path: Please check the 'src' folder or use a custom path", e);
                throw;
            }

            Console.WriteLine($"Using Path: {BaseDir}");
            DirectoryInfo directoryInfo = new(BaseDir);
            IOTools.GetAllFileSystems(directoryInfo.GetFileSystemInfos(), 0);

            Console.WriteLine("Start...");
            IOTools.WriteStream();
            Console.WriteLine($"------{Environment.NewLine}...Done");

            if (IOTools.unsafeStrings.Count>0)
            {
                Console.WriteLine($"------{Environment.NewLine}Warning: 检测到下列文件(夹)含非法字符, 这会导致mdbook异常, 请修改后重新运行{Environment.NewLine}------");
                while (IOTools.unsafeStrings.Count>0)
                {
                    Console.WriteLine(IOTools.unsafeStrings.Dequeue());
                }
            }

            SendMessage("------");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">信息</param>
        public static void SendMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public static void SendMessage(string message, Exception exception)
        {
            Console.WriteLine($"{message}, For Details:{Environment.NewLine}{exception}");
            Console.ReadKey(true);
        }
    }
}