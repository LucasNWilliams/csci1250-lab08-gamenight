
const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;
const int PADDING = 11;

string projectFolder = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);


string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);

Console.WriteLine();

Console.Write("PLAYER".PadRight(PADDING));
Console.WriteLine("POINTS");
for (int i = 0; i < players.Length; i++)
{
    Console.Write(players[i].PadRight(PADDING));
    Console.WriteLine(points[i]);
}
int leaderIndex = LeaderIndex(points);
Console.WriteLine($"Leader: {players[leaderIndex]} with {points[leaderIndex]}");
Console.WriteLine("Season total: " + TotalPoints(points));

UpdateAllPoints(players, points);

Console.WriteLine();


static int LeaderIndex(string[] points)
{
    int highestScore = 0;
    int leaderIndex = 0;
    for(int i = 0; i < points.Length; i++)
    {
        int point = Convert.ToInt32(points[i]);
        if (point > highestScore)
        {
            highestScore = point;
            leaderIndex = i;
        }
    }

    return leaderIndex;
}

static int TotalPoints(string[] points)
{
    int total = 0;
    foreach(string point in points)
    {
        total += Convert.ToInt32(point);
    }
    return total;
}

// static string HistoryEntry(int night, string winner, int points);

static void UpdateAllPoints(string[] players, string[] points)
{
    string[] pointsTonight = new string[players.Length];
    for(int i = 0; i < players.Length; i++)
    {
        Console.Write($"Points tonight for {players[i]}: ");
        int tonight = Convert.ToInt32(Console.ReadLine());

        while(tonight > MAX_POINTS || tonight < 0)
        {
            Console.WriteLine("Points must be from 0 to 20");
            Console.Write($"Points tonight for {players[i]}: ");
            tonight = Convert.ToInt32(Console.ReadLine());
        }

        pointsTonight[i] = tonight.ToString();

        int season = Convert.ToInt32(points[i]) + tonight;
        points[i] = season.ToString();
    }

    int leaderIndex = LeaderIndex(pointsTonight);
    Console.WriteLine($"Tonight's Winner: {players[leaderIndex]} with {pointsTonight[leaderIndex]}");
    
    File.WriteAllLines(POINTS_FILE, points);
    Console.WriteLine("Saved " + POINTS_FILE);
}