public class TravelSummary
{
    public long LastEntryStation { get; set; }
    public long LastExitStation { get; set; }

    public long LastEntryTime { get; set; }
    public long LastExitTime { get; set; }

    public double TotalFarePaid { get; set; }

    public int TotalTrips { get; set; }

    public double AverageFarePerTrip { get; set; }
}



// Do not modify

public class Commuter
{
    public int CardNumber { get; set; }

    public string? CommuterName { get; set; }

    public string? CommuterType { get; set; }

    public TravelSummary? TravelSummary { get; set; }
}



// Do not modify

public class Station
{
    public int StationId { get; set; }

    public string StationName { get; set; }

    public int Zone { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}
public class Journey
{
    public int EntryStationId { get; set; }

    public long EntryTime { get; set; }
}
public class RevenueRecord
{
    public string ZonePair { get; set; }

    public double Fare { get; set; }

    public long ExitTime { get; set; }
}


// Do not modify

public interface IMetroOperations
{
    void IssueCard(int cardNumber, string commuterName, string commuterType);

    bool TapIn(int cardNumber, int stationId, long epochTime);

    bool TapOut(int cardNumber, int stationId, long epochTime);

    Commuter GetCommuterInfo(int cardNumber);

    List<double> FareHistory(int cardNumber);

    Dictionary<string, double> GetZoneWiseRevenue(long startTime, long endTime);

    List<string> GetFrequentRoute(int cardNumber);

    double GetDailyPassSavings(int cardNumber, long date);
}

public class MetroCardManager : IMetroOperations
{
    // Fare configuration
    private readonly double baseFare;
    private readonly double perKmRate;
    private readonly double maxDailyCap;

    // Master Data
    private readonly Dictionary<int, Station> stations;
    private readonly Dictionary<int, Commuter> commuters;

    // Active journeys (CardNumber -> Journey)
    private readonly Dictionary<int, Journey> activeJourneys;

    // CardNumber -> Last 5 fares
    private readonly Dictionary<int, List<double>> fareHistory;

    // CardNumber -> (Route -> Count)
    private readonly Dictionary<int, Dictionary<string, int>> routeFrequency;

    // CardNumber -> (Date -> Total Fare)
    private readonly Dictionary<int, Dictionary<long, double>> dailyFare;

    // Revenue Records
    private readonly List<RevenueRecord> revenueRecords;

    // Discount percentages
    private readonly Dictionary<string, double> discounts;

    // Constructor
    public MetroCardManager(
        List<Station> stationList,
        double baseFare,
        double perKmRate,
        double maxDailyCap)
    {
        this.baseFare = baseFare;
        this.perKmRate = perKmRate;
        this.maxDailyCap = maxDailyCap;

        // Store stations for O(1) lookup
        stations = stationList.ToDictionary(s => s.StationId);

        commuters = new Dictionary<int, Commuter>();

        activeJourneys = new Dictionary<int, Journey>();

        fareHistory = new Dictionary<int, List<double>>();

        routeFrequency = new Dictionary<int, Dictionary<string, int>>();

        dailyFare = new Dictionary<int, Dictionary<long, double>>();

        revenueRecords = new List<RevenueRecord>();

        discounts = new Dictionary<string, double>()
        {
            { "ADULT", 0.00 },
            { "SENIOR", 0.50 },
            { "STUDENT", 0.25 },
            { "CHILD", 0.75 }
        };
    }
    public void IssueCard(int cardNumber, string commuterName, string commuterType)
    {
        // Card already exists
        if (commuters.ContainsKey(cardNumber))
            return;

        Commuter commuter = new Commuter
        {
            CardNumber = cardNumber,
            CommuterName = commuterName,
            CommuterType = commuterType,
            TravelSummary = new TravelSummary()
        };

        commuters[cardNumber] = commuter;

        fareHistory[cardNumber] = new List<double>();

        routeFrequency[cardNumber] = new Dictionary<string, int>();

        dailyFare[cardNumber] = new Dictionary<long, double>();
    }
    public bool TapIn(int cardNumber, int stationId, long epochTime)
    {
        // Card doesn't exist
        if (!commuters.ContainsKey(cardNumber))
            return false;

        // Station doesn't exist
        if (!stations.ContainsKey(stationId))
            return false;

        // Already inside metro
        if (activeJourneys.ContainsKey(cardNumber))
            return false;

        activeJourneys[cardNumber] = new Journey
        {
            EntryStationId = stationId,
            EntryTime = epochTime
        };

        commuters[cardNumber].TravelSummary.LastEntryStation = stationId;
        commuters[cardNumber].TravelSummary.LastEntryTime = epochTime;

        return true;
    }
    public Commuter GetCommuterInfo(int cardNumber)
    {
        if (!commuters.ContainsKey(cardNumber))
            return null;

        return commuters[cardNumber];
    }
    public bool TapOut(int cardNumber, int stationId, long epochTime)
    {
        // Validation
        if (!commuters.ContainsKey(cardNumber))
            return false;

        if (!activeJourneys.ContainsKey(cardNumber))
            return false;

        if (!stations.ContainsKey(stationId))
            return false;

        Journey journey = activeJourneys[cardNumber];

        if (epochTime <= journey.EntryTime)
            return false;

        if (journey.EntryStationId == stationId)
            return false;

        Station entryStation = stations[journey.EntryStationId];
        Station exitStation = stations[stationId];

        // Distance
        double distance = CalculateDistance(entryStation, exitStation);

        // Duration in minutes
        double duration = (epochTime - journey.EntryTime) / (1000.0 * 60);

        // Fare Calculation
        double fare;

        if (duration > 120)
        {
            fare = baseFare * 3;
        }
        else
        {
            fare = baseFare + (distance * perKmRate);
        }

        // Apply Discount
        string type = commuters[cardNumber].CommuterType;

        if (discounts.ContainsKey(type))
        {
            fare = fare * (1 - discounts[type]);
        }

        // Daily Cap
        long day = GetDateFromEpoch(journey.EntryTime);

        if (!dailyFare[cardNumber].ContainsKey(day))
            dailyFare[cardNumber][day] = 0;

        double todayFare = dailyFare[cardNumber][day];

        if (todayFare >= maxDailyCap)
        {
            fare = 0;
        }
        else if (todayFare + fare > maxDailyCap)
        {
            fare = maxDailyCap - todayFare;
        }

        dailyFare[cardNumber][day] += fare;

        // Update Travel Summary
        Commuter commuter = commuters[cardNumber];

        commuter.TravelSummary.LastExitStation = stationId;
        commuter.TravelSummary.LastExitTime = epochTime;

        commuter.TravelSummary.TotalFarePaid += fare;
        commuter.TravelSummary.TotalTrips++;

        commuter.TravelSummary.AverageFarePerTrip =
            commuter.TravelSummary.TotalFarePaid /
            commuter.TravelSummary.TotalTrips;

        // Fare History
        fareHistory[cardNumber].Add(fare);

        if (fareHistory[cardNumber].Count > 5)
        {
            fareHistory[cardNumber].RemoveAt(0);
        }

        // Route Frequency
        string route =
            entryStation.StationName + " to " + exitStation.StationName;

        if (!routeFrequency[cardNumber].ContainsKey(route))
        {
            routeFrequency[cardNumber][route] = 0;
        }

        routeFrequency[cardNumber][route]++;

        // Revenue Record
        RevenueRecord revenue = new RevenueRecord
        {
            ZonePair = $"Zone{entryStation.Zone}-Zone{exitStation.Zone}",
            Fare = fare,
            ExitTime = epochTime
        };

        revenueRecords.Add(revenue);

        // Journey Finished
        activeJourneys.Remove(cardNumber);

        return true;
    }
    public List<double> FareHistory(int cardNumber)
{
    if (!fareHistory.ContainsKey(cardNumber))
        return new List<double>();

    return fareHistory[cardNumber]
            .OrderByDescending(f => f)
            .Take(5)
            .ToList();
}
    private double CalculateDistance(Station s1, Station s2)
    {
        double lat1 = Math.PI * s1.Latitude / 180.0;
        double lon1 = Math.PI * s1.Longitude / 180.0;
        double lat2 = Math.PI * s2.Latitude / 180.0;
        double lon2 = Math.PI * s2.Longitude / 180.0;

        double dLat = lat2 - lat1;
        double dLon = lon2 - lon1;

        double a =
            Math.Pow(Math.Sin(dLat / 2), 2) +
            Math.Cos(lat1) *
            Math.Cos(lat2) *
            Math.Pow(Math.Sin(dLon / 2), 2);

        double c = 2 * Math.Asin(Math.Sqrt(a));

        const double earthRadius = 6371;

        return earthRadius * c;
    }
    private long GetDateFromEpoch(long epochTime)
    {
        DateTime date = DateTimeOffset
            .FromUnixTimeMilliseconds(epochTime)
            .UtcDateTime;

        return long.Parse(date.ToString("yyyyMMdd"));
    }
    public Dictionary<string, double> GetZoneWiseRevenue(long startTime, long endTime)
    {
        Dictionary<string, double> revenue = new Dictionary<string, double>();

        foreach (RevenueRecord record in revenueRecords)
        {
            if (record.ExitTime >= startTime &&
                record.ExitTime <= endTime)
            {
                if (!revenue.ContainsKey(record.ZonePair))
                    revenue[record.ZonePair] = 0;

                revenue[record.ZonePair] += record.Fare;
            }
        }

        return revenue
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
    }
    public List<string> GetFrequentRoute(int cardNumber)
    {
        if (!routeFrequency.ContainsKey(cardNumber))
            return new List<string>();

        return routeFrequency[cardNumber]
                .OrderByDescending(x => x.Value)
                .Take(3)
                .Select(x => x.Key)
                .ToList();
    }
    public double GetDailyPassSavings(int cardNumber, long date)
    {
        if (!dailyFare.ContainsKey(cardNumber))
            return 0;

        if (!dailyFare[cardNumber].ContainsKey(date))
            return 0;

        double actualFare = dailyFare[cardNumber][date];

        double passCost = maxDailyCap * 0.8;

        double savings = actualFare - passCost;

        return savings > 0 ? savings : 0;
    }

}


class Program
{
    static void Main(string[] args)
    {
        // numberOfRequests baseFare perKmRate maxDailyCap
        string[] first = Console.ReadLine().Split();

        int numberOfRequests = int.Parse(first[0]);
        double baseFare = double.Parse(first[1]);
        double perKmRate = double.Parse(first[2]);
        double maxDailyCap = double.Parse(first[3]);

        // Number of stations
        int numberOfStations = int.Parse(Console.ReadLine());

        List<Station> stations = new List<Station>();

        for (int i = 0; i < numberOfStations; i++)
        {
            string[] data = Console.ReadLine().Split();

            Station station = new Station
            {
                StationId = int.Parse(data[0]),
                StationName = data[1],
                Zone = int.Parse(data[2]),
                Latitude = double.Parse(data[3]),
                Longitude = double.Parse(data[4])
            };

            stations.Add(station);
        }

        MetroCardManager manager =
            new MetroCardManager(stations, baseFare, perKmRate, maxDailyCap);

        for (int i = 0; i < numberOfRequests; i++)
        {
            string line = Console.ReadLine();

            string[] cmd = line.Split();

            switch (cmd[0])
            {
                case "issueCard":
                {
                    int cardNumber = int.Parse(cmd[1]);
                    string commuterName = cmd[2];
                    string commuterType = cmd[3];

                    manager.IssueCard(cardNumber, commuterName, commuterType);
                    break;
                }

                case "tapIn":
                {
                    int cardNumber = int.Parse(cmd[1]);
                    int stationId = int.Parse(cmd[2]);
                    long epochTime = long.Parse(cmd[3]);

                    Console.WriteLine(manager.TapIn(cardNumber, stationId, epochTime));
                    break;
                }

                case "tapOut":
                {
                    int cardNumber = int.Parse(cmd[1]);
                    int stationId = int.Parse(cmd[2]);
                    long epochTime = long.Parse(cmd[3]);

                    Console.WriteLine(manager.TapOut(cardNumber, stationId, epochTime));
                    break;
                }

                case "commuterInfo":
                {
                    int cardNumber = int.Parse(cmd[1]);

                    Commuter commuter = manager.GetCommuterInfo(cardNumber);

                    if (commuter != null)
                    {
                        Console.WriteLine(
                            $"{commuter.CardNumber} " +
                            $"{commuter.CommuterName} " +
                            $"{commuter.CommuterType} " +
                            $"{commuter.TravelSummary.LastEntryStation} " +
                            $"{commuter.TravelSummary.LastExitStation} " +
                            $"{commuter.TravelSummary.LastEntryTime} " +
                            $"{commuter.TravelSummary.LastExitTime} " +
                            $"{commuter.TravelSummary.TotalFarePaid:F2} " +
                            $"{commuter.TravelSummary.TotalTrips} " +
                            $"{commuter.TravelSummary.AverageFarePerTrip:F2}");
                    }

                    break;
                }

                case "fareHistory":
                {
                    int cardNumber = int.Parse(cmd[1]);

                    List<double> fares = manager.FareHistory(cardNumber);

                    foreach (double fare in fares)
                        Console.WriteLine(fare);

                    break;
                }

                case "zoneRevenue":
                {
                    long startTime = long.Parse(cmd[1]);
                    long endTime = long.Parse(cmd[2]);

                    Dictionary<string, double> revenue =
                        manager.GetZoneWiseRevenue(startTime, endTime);

                    foreach (var item in revenue)
                    {
                        Console.WriteLine($"{item.Key}:{item.Value:F2}");
                    }

                    break;
                }

                case "frequentRoute":
                {
                    int cardNumber = int.Parse(cmd[1]);

                    List<string> routes = manager.GetFrequentRoute(cardNumber);

                    foreach (string route in routes)
                        Console.WriteLine(route);

                    break;
                }

                case "dailySavings":
                {
                    int cardNumber = int.Parse(cmd[1]);
                    long date = long.Parse(cmd[2]);

                    Console.WriteLine(manager.GetDailyPassSavings(cardNumber, date));
                    break;
                }
            }
        }
    }
}