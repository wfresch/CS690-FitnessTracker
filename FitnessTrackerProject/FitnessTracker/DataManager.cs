using System.Security.Cryptography.X509Certificates;

namespace FitnessTracker;

public class DataManager {

    // FileSaver workoutSaver;
    //FileSaver workoutSubtypeSaver;

    public List<WorkoutSubtype> ReservedWorkoutSubtypes { get; }
    public List<WorkoutSubtype> CustomWorkoutSubtypes {get;}
    //public List<WorkoutSubtype> WorkoutSubtypes {get;}
    public List<User> Users { get; }
    // public List<Workout> Workouts {get;}
    // public List<MovementWorkoutDetails> MovementWorkoutDetailList {get; }
    // public List<WeightliftingWorkoutDetails> WeightliftingWorkoutDetailList {get;}
    private const int MOVEMENT_WORKOUT_TYPE = 0; 
    private const int WEIGHTLIFTING_WORKOUT_TYPE = 1; 
    

    public DataManager() {

        // Initialize and Load User Data
        Users = [];
        TouchFile("users.txt");
        var usersFileContent = File.ReadAllLines("users.txt");

        foreach(var user in usersFileContent) {
            Users.Add(new User(user));
        }

        ReservedWorkoutSubtypes =
        [
            new("Running", WorkoutType.Movement, true),
            new ("Walking", WorkoutType.Movement, true),
            new ("Biking", WorkoutType.Movement, true),
            new ("Back Squat", WorkoutType.Weightlifting, true),
            new ("Deadlift", WorkoutType.Weightlifting, true),
            new ("Bench Press", WorkoutType.Weightlifting, true),
            new ("Frong Squat", WorkoutType.Weightlifting, true)
        ];

        //workoutSubtypeSaver = new FileSaver("custom-workout-subtypes.txt");

        //List<WorkoutSubtype> CustomWorkoutSubtypes = [];
        CustomWorkoutSubtypes = [];
        TouchFile("custom-workout-subtypes.txt");

        var subTypesFromFile = File.ReadAllLines("custom-workout-subtypes.txt");
        foreach(var subtype in subTypesFromFile)
        {
            var splitted = subtype.Split(",",StringSplitOptions.RemoveEmptyEntries);
            var inputName = splitted[0];
            var inputType = splitted[1];    

            var workoutType = Enum.Parse<WorkoutType>(inputType);

            CustomWorkoutSubtypes.Add(new WorkoutSubtype(inputName, workoutType));
        }

        //WorkoutSubtypes = ReservedWorkoutSubtypes.Concat(Custom)

    }

    public void TouchFile(string filename)
    {
        if(!File.Exists(filename)) {
            File.Create(filename).Close();
        }
    }

#region User methods
    public void SynchronizeUsers() {
        string fileName = "users.txt";
        File.Delete(fileName);

        if (Users.Count == 0)
        {
            TouchFile(fileName);
        }
        else 
        {
            foreach(var user in Users) {
                File.AppendAllText("users.txt",user.Name+Environment.NewLine);
            }  
        }
    }

    public bool UsersExist()
    {
        return Users.Count != 0;
    }

    public bool AddUser(User user) {
        if (Users.Any(x => x.Name == user.Name))
        {
            return false;
        }

        Users.Add(user);
        SynchronizeUsers();
        return true;
    }

    public void RemoveUser(User user) {
        //TODO: Remove related workouts
        //Workouts.RemoveAll(x => x.User == user.Name);
        //var workoutsToRemove = Workouts.Where(x => x.User == user.Name);


        Users.Remove(user);
        SynchronizeUsers();
    }
#endregion

#region Custom workout methods
    public void SynchronizeCustomWorkoutSubtypes() {
        string fileName = "custom-workout-subtypes.txt";
        File.Delete(fileName);

        if (CustomWorkoutSubtypes.Count == 0)
        {
            TouchFile(fileName);
        }
        else 
        {
            foreach(var customWorkoutSubtype in CustomWorkoutSubtypes) {
                int workoutType = (customWorkoutSubtype.WorkoutType == WorkoutType.Movement) 
                    ? MOVEMENT_WORKOUT_TYPE
                    : WEIGHTLIFTING_WORKOUT_TYPE;

                File.AppendAllText(fileName, $"{customWorkoutSubtype.Name},{workoutType}{Environment.NewLine}");
            }    
        }
    }

    public bool CustomWorkoutSubtypesExist()
    {
        return CustomWorkoutSubtypes.Count != 0;
    }

    public bool AddCustomWorkoutSubtype(WorkoutSubtype workoutSubtype) {
        if (ReservedWorkoutSubtypes.Any(x => x.Name == workoutSubtype.Name))
        {
            return false;
        }
        
        if (CustomWorkoutSubtypes.Any(x => x.Name == workoutSubtype.Name))
        {
            return false;
        }

        //Console.WriteLine($"Adding a custom workout in DataManager with type {workoutSubtype.WorkoutType}.");
        CustomWorkoutSubtypes.Add(workoutSubtype);
        SynchronizeCustomWorkoutSubtypes();
        return true;
    }

    public List<WorkoutSubtype> GetWorkoutSubTypes(WorkoutType workoutType)
    {
        List<WorkoutSubtype> workoutSubtypes = [];
        workoutSubtypes = [.. ReservedWorkoutSubtypes.Where(x => x.WorkoutType == workoutType)];

        // List<WorkoutSubtype> customWorkoutSubtypes = [];
        // customWorkoutSubtypes = [.. CustomWorkoutSubtypes.Where(x => x.WorkoutType == workoutType)];

        return [.. workoutSubtypes, .. CustomWorkoutSubtypes.Where(x => x.WorkoutType == workoutType)];
    }

    public void RenameCustomWorkoutSubtype(WorkoutSubtype workoutSubtype, string newName) {
        //TODO: What else needs renamed here?

        foreach (var subtype in CustomWorkoutSubtypes.Where(x => x.Name == workoutSubtype.Name))
        {
            subtype.Rename(newName);
        }

        SynchronizeCustomWorkoutSubtypes();
    }

    public void RemoveCustomWorkoutSubtype(WorkoutSubtype workoutSubtype) {
        //TODO: Remove related workouts
        //Workouts.RemoveAll(x => x.User == user.Name);
        //var workoutsToRemove = Workouts.Where(x => x.User == user.Name);

        CustomWorkoutSubtypes.Remove(workoutSubtype);
        SynchronizeCustomWorkoutSubtypes();
    }
#endregion


}