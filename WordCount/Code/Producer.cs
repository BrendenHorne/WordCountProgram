using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

class Producer {
    private static readonly SemaphoreSlim semaphore = new(5);
    public static void Run() {
        using NamedPipeServerStream pipeServer = new("WordCountPipe", PipeDirection.Out);
        Console.WriteLine("[Producer] Waiting for Consumer to connect...");
        pipeServer.WaitForConnection();
        Console.WriteLine("[Producer] Connected. Start entering text (empty line to finish):");
        using StreamWriter writer = new(pipeServer) { AutoFlush = true };
        var tasks = new List<Task>();
        
        while (true) {
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) break;
            tasks.Add(Task.Run(async () => {
                await semaphore.WaitAsync();
                try {
                    await writer.WriteLineAsync(input);
                }
                catch (IOException) {
                    Console.WriteLine("[Producer] Error: Consumer disconnected.");
                }
                finally {
                    semaphore.Release();
                }
            }));
        }

        Task.WaitAll(tasks.ToArray());
        Console.WriteLine("[Producer] Finished sending data.");
    }
}