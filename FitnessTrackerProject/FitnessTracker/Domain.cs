namespace FitnessTracker;

public class User {
    public string Name { get; }

    public User(string name) {
        this.Name = name;
    }

    public override string ToString() {
        return this.Name;
    }
}

public class WorkoutSubtype {
    public string Name { get; private set; }

    public WorkoutType WorkoutType {get;}
    public bool Reserved {get; }


    public WorkoutSubtype(string name, WorkoutType workoutType, bool reserved=false) {
        this.Name = name;
        this.WorkoutType = workoutType;
        this.Reserved = reserved;
        //Console.WriteLine($"Created a new subtype with type {workoutType}.");
    }

    public void Rename(string newName)
    {
        Name = newName;
    }

    public override string ToString() {
        return this.Name;
    }
}

public class MovementWorkoutDetails
{
    public Guid WorkoutId {get;}
    public int DistanceQuantity { get; }
    public string DistanceUnits { get;}
    public int TimeQuantity { get; }
    public string TimeUnits { get;}
    

    public MovementWorkoutDetails(Guid workoutId, int distanceQuantity, string distanceUnits, int timeQuantity, string timeUnits) {
        this.WorkoutId = workoutId;
        this.DistanceQuantity = distanceQuantity;
        this.DistanceUnits = distanceUnits;
        this.TimeQuantity = timeQuantity;
        this.TimeUnits = timeUnits;
    }

    public override string ToString() {
        return $"Distance: {DistanceQuantity} {DistanceUnits}, Time: {TimeQuantity} {TimeUnits}";
    }
}

public class WeightliftingWorkoutDetails
{
    public Guid WorkoutId {get;}
    public int WeightQuantity { get; }
    public string WeightUnits { get;}
    public int NumberOfSets { get; }
    public int NumberOfReps { get;}
    

    public WeightliftingWorkoutDetails(Guid workoutId, int weightQuantity, string weightUnits, int numberOfSets, int numberOfReps) {
        this.WorkoutId = workoutId;
        this.WeightQuantity = weightQuantity;
        this.WeightUnits = weightUnits;
        this.NumberOfSets = numberOfSets;
        this.NumberOfReps = numberOfReps;
    }

    public override string ToString() {
        return $"Weight: {WeightQuantity} {WeightUnits}, {NumberOfSets} sets of {NumberOfReps}";
    }
}

public class Workout
{
    public Guid WorkoutId {get;}

    public string User {get; }

    public DateTime WorkoutDate {get;}

    public WorkoutSubtype Subtype {get;}    

    public Workout(string user, DateTime workoutDate, WorkoutSubtype subtype) {
        this.WorkoutId = Guid.NewGuid();
        this.User = user;
        this.WorkoutDate = workoutDate;
        this.Subtype = subtype;
    }

    public override string ToString() {
        return $"Date: {WorkoutDate}, {Subtype}";
    }
}

    #region Enums
public enum WorkoutType {
    Movement,
    Weightlifting
}

public enum DistanceUnits
{
    Miles,
    Kilometers
}

public enum TimeUnits
{
    Hours,
    Minutes,
    Seconds
}

public enum WeightUnits
{
    Pounds,
    Kilograms
}
#endregion