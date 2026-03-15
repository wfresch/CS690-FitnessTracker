namespace FitnessTracker.Tests;

using FitnessTracker;

public class DomainTests
{
    [Fact]
    public void Test_NewUser_NoId()
    {
        var name = "Mark";
        var newUser = new User(name);

        Assert.Equal(name, newUser.Name);
        Assert.NotNull(newUser?.UserId);
        Assert.Equal(name, newUser.ToString());
    }

    [Fact]
    public void Test_NewUser_WithId()
    {
        var name = "Mark";
        var newId = Guid.NewGuid();
        var newUser = new User(name, newId);

        Assert.Equal(name, newUser.Name);
        Assert.Equal(newId, newUser.UserId);
        Assert.Equal(name, newUser.ToString());
    }

    [Theory]
    [InlineData(WorkoutType.Movement)]
    [InlineData(WorkoutType.Weightlifting)]
    public void Test_New_WorkoutSubtype(WorkoutType workoutType)
    {
        var name = "TestWorkout";
        var newWorkoutSubtype = new WorkoutSubtype(name, workoutType);

        Assert.Equal(name, newWorkoutSubtype.Name);
        Assert.Equal(workoutType, newWorkoutSubtype.WorkoutType);
        Assert.NotNull(newWorkoutSubtype?.SubtypeId);
        Assert.Equal(name, newWorkoutSubtype.ToString());
    }

    [Theory]
    [InlineData(WorkoutType.Movement)]
    [InlineData(WorkoutType.Weightlifting)]
    public void Test_Rename_WorkoutSubtype(WorkoutType workoutType)
    {
        var newWorkoutSubtype = new WorkoutSubtype("TestWorkout", workoutType);
        var newName = "NewWorkout";

        newWorkoutSubtype.Rename(newName);

        Assert.Equal(newName, newWorkoutSubtype.Name);
        Assert.Equal(newName, newWorkoutSubtype.ToString());
    }

    [Fact]
    public void Test_MovementWorkoutDetails()
    {
        var workoutId = Guid.NewGuid();
        var distanceQuantity = 5;
        var distanceUnits = DistanceUnits.Kilometers.ToString();
        var timeQuantity = 30;
        var timeUnits = TimeUnits.Minutes.ToString();

        var movementWorkoutDetails = new MovementWorkoutDetails(workoutId, distanceQuantity, distanceUnits, timeQuantity, timeUnits);

        Assert.Equal($"Distance: {distanceQuantity} {distanceUnits}, Time: {timeQuantity} {timeUnits}", movementWorkoutDetails.ToString());
        Assert.Equal($"{workoutId},{distanceQuantity},{distanceUnits},{timeQuantity},{timeUnits}", movementWorkoutDetails.Storage());
    }

    [Fact]
    public void Test_WeightliftingWorkoutDetails()
    {
        var workoutId = Guid.NewGuid();
        var weightQuantity = 80;
        var weightUnits = WeightUnits.Kilograms.ToString();
        var numberOfSets = 5;
        var numberOfReps = 8;

        var weightliftingWorkoutDetails = new WeightliftingWorkoutDetails(workoutId, weightQuantity, weightUnits, numberOfSets, numberOfReps);

        Assert.Equal($"Weight: {weightQuantity} {weightUnits}, {numberOfSets} sets of {numberOfReps}", weightliftingWorkoutDetails.ToString());
        Assert.Equal($"{workoutId},{weightQuantity},{weightUnits},{numberOfSets},{numberOfReps}", weightliftingWorkoutDetails.Storage());
    }

}