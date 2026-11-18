/*
 *  Name:           Lucas Williams
 *  Course:         CSCI 1250, Section 002
 *  Assignment:     Lab 08, Game Night
 *  Date:           November 18, 2026
 *  Description:    Keeps a game night leaderboard in three text files.
 *                  Shows the season so far, records tonight's points, saves the new totals, 
 *                  and adds tongiht's result to a histroy that grows every week.
 */

const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;
const int PADDING = 11;
const int HISTORY_PADDING = 2;

string projectFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);


string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);


Console.WriteLine("\n=== GAME NIGHT LEADERBOARD ===");
string[] history = File.ReadAllLines(HISTORY_FILE);

if (history.Length == 0)
{
    Console.WriteLine("No nights played yet.");
} else
{
    Console.WriteLine("Nights played: " + history.Length);
    // foreach: Just need to write to console for each previous night. Index is unnecessary.
    foreach(string night in history)
    {
        Console.WriteLine(night.PadLeft(HISTORY_PADDING));
    }
}

int tonightNumber = history.Length + 1;
Console.WriteLine($"Tonight is Night {tonightNumber}");

// Part 1 //
Console.WriteLine();
Console.WriteLine("PLAYER".PadRight(PADDING) + "POINTS");
// for: need to display the player and their points together, shared index is required as arrays are separate
for (int i = 0; i < players.Length; i++)
{
    Console.WriteLine(players[i].PadRight(PADDING) + points[i]);
}

int leaderIndex = LeaderIndex(points);
string leaderName = players[leaderIndex];
string leaderPoints = points[leaderIndex];
Console.WriteLine($"Leader: {leaderName} with {leaderPoints}");
Console.WriteLine("Season total: " + TotalPoints(points));
Console.WriteLine();

// Part 2
string[] pointsTonight = new string[players.Length];

// for: to save the points to their respective player, the index must be the same for each array
for(int i = 0; i < players.Length; i++)
{
    Console.Write($"Points tonight for {players[i]}: ");
    int tonight = Convert.ToInt32(Console.ReadLine());

    // while: if entered points is invalid, ask again until input is valid.
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

int leaderTonightIndex = LeaderIndex(pointsTonight);
string winnerTonightName = players[leaderTonightIndex];
int winnerTonightPoints = Convert.ToInt32(pointsTonight[leaderTonightIndex]);
Console.WriteLine($"Tonight's Winner: {winnerTonightName} with {winnerTonightPoints}");

File.WriteAllLines(POINTS_FILE, points);
Console.WriteLine("\nSaved " + POINTS_FILE);

// Part 3
string[] entry = { HistoryEntry(tonightNumber, winnerTonightName, winnerTonightPoints) };
File.AppendAllLines(HISTORY_FILE, entry);
Console.WriteLine($"Added to {HISTORY_FILE}: {entry[0]}");

Console.WriteLine();

// Returns the index of the leader based on an entered array of points  
static int LeaderIndex(string[] points)
{
    int highestScore = 0;
    int leaderIndex = 0;
    // for: method needs to return an index, a for loop is the best way to do this cleanly
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

// Returns total of all points in the inputed points array
static int TotalPoints(string[] points)
{
    int total = 0;
    // foreach: need to total up all of the points, but order/index does not matter
    foreach(string point in points)
    {
        total += Convert.ToInt32(point);
    }
    return total;
}

// Formats a string that can be saved to the history file to log the current night
static string HistoryEntry(int night, string winner, int points)
{
    return $"Night {night}: {winner} won with {points} points";
}