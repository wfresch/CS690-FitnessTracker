namespace FitnessTracker;

public class User {
    public Guid UserId { get; }
    public string Name { get; }

    public User(string name) : this(name, Guid.NewGuid()) {
    }

    public User(string name, Guid userId) {
        this.UserId = userId;
        this.Name = name;
    }

    public override string ToString() {
        return this.Name;
    }
}

public class WorkoutSubtype {
    public Guid SubtypeId { get; }
    public string Name { get; private set; }
    public WorkoutType WorkoutType {get;}
    public bool Reserved {get; }

    public WorkoutSubtype(string name, WorkoutType workoutType, bool reserved=false) 
    : this(Guid.NewGuid(), name, workoutType, reserved) {
    }

    public WorkoutSubtype(Guid subtypeId, string name, WorkoutType workoutType, bool reserved=false) {
        this.SubtypeId = subtypeId;
        this.Name = name;
        this.WorkoutType = workoutType;
        this.Reserved = reserved;
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

    public string Storage()
    {
        return $"{WorkoutId},{DistanceQuantity},{DistanceUnits},{TimeQuantity},{TimeUnits}";
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

    public string Storage()
    {
        return $"{WorkoutId},{WeightQuantity},{WeightUnits},{NumberOfSets},{NumberOfReps}";
    }
}

public class Workout
{
    public Guid WorkoutId {get;}

    public User WorkoutUser {get; }

    public DateTime WorkoutDate {get;}

    public WorkoutSubtype Subtype {get;}    

    public Workout(User workoutUser, DateTime workoutDate, WorkoutSubtype subtype) : this(Guid.NewGuid(), workoutUser, workoutDate, subtype) {
    }

    public Workout(Guid workoutId, User workoutUser, DateTime workoutDate, WorkoutSubtype subtype) {
        this.WorkoutId = workoutId;
        this.WorkoutUser = workoutUser;
        this.WorkoutDate = workoutDate;
        this.Subtype = subtype;
    }

    public override string ToString() {
        return $"Date: {WorkoutDate}, {Subtype.Name}";
    }

    public string Storage()
    {
        return $"{WorkoutId},{WorkoutUser.UserId},{WorkoutDate},{Subtype.SubtypeId}";
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