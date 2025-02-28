using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

class Consumer {
    private static readonly Mutex mutex = new();
    public static void Run() {
        using NamedPipeClientStream pipeClient = new(".", "WordCountPipe", PipeDirection.In);
        Console.WriteLine("[Consumer] Connecting to Producer...");
        pipeClient.Connect();
        Console.WriteLine("[Consumer] Connected. Waiting for data...");
        using StreamReader reader = new(pipeClient);
        var lines = new ConcurrentBag<string>(); 
        string? line;

        while ((line = reader.ReadLine()) != null) {
            lines.Add(line);
        }
        
        int totalWords = 0;
        Parallel.ForEach(lines, (text) => {
            int count = CountWords(text);
            mutex.WaitOne();
            totalWords += count;
            mutex.ReleaseMutex();
        });
        Console.WriteLine($"[Consumer] Total words counted: {totalWords}");
    }

    static int CountWords(string line) {
        string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }
}