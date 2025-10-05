namespace AddToPath;

internal class Program
{
    const int delay = 100;

    static void Main(string[] args)
    {
        if (args.Length == 1 && args[0] == "-r")
        {
            RemoveFromPath();
        }
        else if (args.Length == 1 && args[0] == "-c")
        {
            List<string> path = GetPath();
            ShowPath(path);
            Console.WriteLine();
        }
        else if (args.Length == 1 && (args[0] == "-h" || args[0] == "?" || args[0] == "--help"))
        {
            Console.Write(
                "Use '-r' to remove existing PATH variable;\n" +
                "Use '-c' to print current PATH;\n" +
                "Use '-h' to print this help message.");
        }
        else
        {
            AddToPath();
        }
    }

    private static void AddToPath()
    {
        string envPath = Environment.CurrentDirectory;
        Console.WriteLine($"Adding new PATH: {envPath}... \n");

        List<string> pathVars = GetPath();

        if (pathVars.Contains(envPath))
        {
            ShowPath(pathVars);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThis path already exist.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!\n");
            Console.ResetColor();
        }
        else
        {
            pathVars.Add(envPath);
            string newPath = CreatePath(pathVars);

            Environment.SetEnvironmentVariable("Path", newPath, EnvironmentVariableTarget.User);

            pathVars = GetPath();
            ShowPath(pathVars);

            if (pathVars.Contains(envPath))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nSuccess!\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nFailer!\n");
                Console.ResetColor();
            }
        }
    }

    private static void RemoveFromPath()
    {
        string envPath = Environment.CurrentDirectory;
        Console.WriteLine($"Removing from PATH: {envPath}... \n");

        List<string> pathVars = GetPath();

        if (pathVars.Contains(envPath))
        {
            pathVars.Remove(envPath);
            string newPath = CreatePath(pathVars);

            Environment.SetEnvironmentVariable("Path", newPath, EnvironmentVariableTarget.User);

            pathVars = GetPath();
            ShowPath(pathVars);

            if (!pathVars.Contains(envPath))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nSuccess!\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nFailer!\n");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThis path is not presented.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!\n");
            Console.ResetColor();
        }
    }

    private static List<string> GetPath()
    {
        string PathString = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
        List<string> path = PathString.Split(";").ToList();
        path.RemoveAll((s) => s == "");
        return path;
    }

    private static string CreatePath(List<string> path)
    {
        return String.Join(";", path) + ";";
    }

    private static void ShowPath(List<string> pathVars)
    {
        foreach (string var in pathVars)
        {
            Console.WriteLine(var + ";");
        }
    }
}
