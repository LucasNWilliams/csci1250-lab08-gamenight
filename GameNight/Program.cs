
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

// static string HistroyEntry(int night, string winner, int points);