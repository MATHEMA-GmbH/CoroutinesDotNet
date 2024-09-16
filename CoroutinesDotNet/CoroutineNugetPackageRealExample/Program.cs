using Coroutine;

var downloadCompletedEvent = new Event();

// Start file download.
var seconds = CoroutineHandler.Start(DownloadFilesCoroutine(downloadCompletedEvent), "File Download Coroutine");

// Start a periodic status check
CoroutineHandler.Start(PeriodicStatusCheck(seconds));

var lastTime = DateTime.Now;
while (true)
{
    var currTime = DateTime.Now;
    CoroutineHandler.Tick(currTime - lastTime);
    lastTime = currTime;
    Thread.Sleep(1);
}

// Coroutine simulating file download.
static IEnumerator<Wait> DownloadFilesCoroutine(Event downloadCompletedEvent)
{
    var filesToDownload = new List<string> { "https://examplefile.com/file-download/259", "https://examplefile.com/file-download/259", "https://examplefile.com/file-download/259" };

    foreach (var file in filesToDownload)
    {
        Console.WriteLine($"Starting download of {file}...");
        using var httpClient = new HttpClient();
        using var response = httpClient.GetAsync(file);
        
        yield return new Wait(5);
        Console.WriteLine($"{file} downloaded.");
    }

    Console.WriteLine("All files downloaded.");

    // We raise the event to indicate that the downloads have finished.
    CoroutineHandler.RaiseEvent(downloadCompletedEvent);
}

// Coroutine that periodically checks the application's status.
static IEnumerator<Wait> PeriodicStatusCheck(ActiveCoroutine first)
{
    while (true)
    {
        // Waits 5 seconds between checks.
        Console.WriteLine("Checking status...");
        yield return new Wait(5);

        if (first.IsFinished)
        {
            Console.WriteLine("Download process completed! Notifying user...");
            // Exit the loop once the download process has been completed.
        }
    }
}