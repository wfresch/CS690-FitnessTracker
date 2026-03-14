namespace FitnessTracker;

using Spectre.Console;

public class Reporter {
    public static Table DisplayWorkoutReport(List<string> workoutData)
    {
        var table = new Table();

        table.AddColumn("Workout Details");

        foreach(var individualWorkout in workoutData) {
            table.AddRow(individualWorkout);
        }

        return table;
    }
    
}