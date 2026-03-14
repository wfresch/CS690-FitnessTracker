namespace FitnessTracker.Tests;

using FitnessTracker;

public class ReporterTests
{
    List<string> workoutData;

    public ReporterTests() {
        workoutData = new List<string>();
    }

    [Fact]
    public void Test_DisplayWorkoutReport()
    {
        workoutData.Add("Date: 03/02/2026 03:00:58, Running, Distance: 4 Miles, Time: 1 Hours");
        workoutData.Add("Date: 03/02/2026 03:02:20, Bench Press, Weight: 155 Pounds, 5 sets of 3");

        var workoutReport = Reporter.DisplayWorkoutReport(workoutData);
        Assert.Equal(workoutData.Count, workoutReport.Rows.Count);
        Assert.Single(workoutReport.Columns);
    }

}