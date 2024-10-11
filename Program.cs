namespace AddToPath;

internal class Program
{
    const int delay = 100;

    static void Main(string[] args)
    {
        string envPath = Environment.CurrentDirectory;
        Console.Write($"Adding new PATH: {envPath}... ");

        string PathString = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
        List<string> PathVars = PathString.Split(";").ToList();
        int countBefore = PathVars.Count;
        PathVars = PathVars[0..^1];
        PathVars = PathVars.Distinct().ToList();
        PathVars.Add(envPath);
        string newPath = String.Join(";", PathVars);
        newPath += ";";
        Console.WriteLine(newPath);

        var coord = Console.GetCursorPosition();
        CancellationTokenSource cts = new CancellationTokenSource();
        Task.Run(async () => 
        { 
            while(!cts.IsCancellationRequested)
            {
                Console.SetCursorPosition(coord.Left, coord.Top);
                Console.Write("/");
                await Task.Delay(delay);

                Console.SetCursorPosition(coord.Left, coord.Top);
                Console.Write("─");
                await Task.Delay(delay);

                Console.SetCursorPosition(coord.Left, coord.Top);
                Console.Write("\\");
                await Task.Delay(delay);

                Console.SetCursorPosition(coord.Left, coord.Top);
                Console.Write("|");
                await Task.Delay(delay);
            }
        });
        Environment.SetEnvironmentVariable("Path", newPath, EnvironmentVariableTarget.User);

        cts.Cancel();

        PathString = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
        PathVars = PathString.Split(";").ToList();

        if (countBefore == PathVars.Count)
            Console.WriteLine("\nDid not added!");
        else
            Console.WriteLine("\nSuccess");

        Console.ReadKey();
    }
}
