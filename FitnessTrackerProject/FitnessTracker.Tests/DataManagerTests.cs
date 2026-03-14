namespace FitnessTracker.Tests;

using FitnessTracker;

public class DataManagerTests
{
    DataManager dataManager;

    public DataManagerTests() {
        dataManager = new DataManager();
    }

    [Fact]
    public void Test_ReservedWorkoutsExist()
    {
        var reservedWorkouts = dataManager.ReservedWorkoutSubtypes;
        Assert.True(reservedWorkouts.Count >= 6);
        
        Assert.Equal(3, reservedWorkouts.Count(x => x.WorkoutType == WorkoutType.Movement));
        Assert.Equal(4, reservedWorkouts.Count(x => x.WorkoutType == WorkoutType.Weightlifting));
    }

    [Fact]
    public void Test_TouchFile()
    {
        var fileName = $"{Guid.NewGuid()}.txt";
        Assert.False(File.Exists(fileName));

        dataManager.TouchFile(fileName);
        Assert.True(File.Exists(fileName));

        File.Delete(fileName);
        Assert.False(File.Exists(fileName));
    }

    [Fact]
    public void Test_Add_And_Remove_Users()
    {
        // Create a username that should not exist
        var userName = Guid.NewGuid().ToString();
        var user = new User(userName);

        // Verify that the user does not yet exist
        Assert.Equal(0, dataManager.Users.Count(x => x.Name == userName));

        // Verify that we can add the user successfully
        Assert.True(dataManager.AddUser(user));
        Assert.Equal(1, dataManager.Users.Count(x => x.Name == userName));

        // Verify that subsequent addition is blocked
        Assert.False(dataManager.AddUser(user));

        // Verify that we can remove the user successfully
        dataManager.RemoveUser(user);
        Assert.Equal(0, dataManager.Users.Count(x => x.Name == userName));
    }

    [Theory]
    [InlineData(WorkoutType.Movement)]
    [InlineData(WorkoutType.Weightlifting)]
    public void Test_Add_And_Remove_Custom_Workout_Subtype(WorkoutType workoutType)
    {
        // Create a workout that should not exist
        var workoutName = Guid.NewGuid().ToString();
        var workoutSubtype = new WorkoutSubtype(workoutName, workoutType);

        // Verify that the workout does not yet exist
        Assert.Equal(0, dataManager.CustomWorkoutSubtypes.Count(x => x.WorkoutType == workoutType && x.Name == workoutName));

        // Verify that we can add the workout successfully
        Assert.True(dataManager.AddCustomWorkoutSubtype(workoutSubtype));
        Assert.Equal(1, dataManager.CustomWorkoutSubtypes.Count(x => x.WorkoutType == workoutType && x.Name == workoutName));

        // Verify that subsequent addition is blocked
        Assert.False(dataManager.AddCustomWorkoutSubtype(workoutSubtype));

        // Verify that we can remove the workout successfully
        dataManager.RemoveCustomWorkoutSubtype(workoutSubtype);
        Assert.Equal(0, dataManager.CustomWorkoutSubtypes.Count(x => x.WorkoutType == workoutType && x.Name == workoutName));
    }

    [Fact]
    public void Test_Add_And_Remove_Movement_Workout()
    {
        var workoutId = Guid.NewGuid();
        var distanceQuantity = 2;
        var distanceUnits = DistanceUnits.Miles;
        var timeQuantity = 30;
        var timeUnits = TimeUnits.Minutes;

        // Test that movement workout details can be added
        dataManager.AddMovementWorkoutDetails(workoutId, distanceQuantity, distanceUnits.ToString(), timeQuantity, timeUnits.ToString());
        Assert.True(dataManager.GetMovementWorkoutDetails(workoutId, out string movementWorkoutDetails));
        Assert.Equal($"{workoutId},{distanceQuantity},{distanceUnits},{timeQuantity},{timeUnits}", movementWorkoutDetails);
    }

    [Fact]
    public void Test_Add_And_Remove_Weightlifting_Workout()
    {
        var workoutId = Guid.NewGuid();
        var weightQuantity = 150;
        var weightUnits = WeightUnits.Pounds;
        var numberOfSets = 3;
        var numberOfReps = 5;

        // Test that weightlifting workout details can be added
        dataManager.AddWeightliftingWorkoutDetails(workoutId, weightQuantity, weightUnits.ToString(), numberOfSets, numberOfReps);
        Assert.True(dataManager.GetWeightliftingWorkoutDetails(workoutId, out string weightliftingWorkoutDetails));
        Assert.Equal($"{workoutId},{weightQuantity},{weightUnits},{numberOfSets},{numberOfReps}", weightliftingWorkoutDetails);
    }
}