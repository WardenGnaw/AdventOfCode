namespace AdventOfCode2021.Utilities
{
    public class IO
    {
        public static string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format(filePath, "does not exist."));
            } 

            return File.ReadAllText(filePath);
        }
    }
}