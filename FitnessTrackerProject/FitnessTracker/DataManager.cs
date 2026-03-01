using System.Security.Cryptography.X509Certificates;

namespace FitnessTracker;

public class DataManager {

    // FileSaver workoutSaver;
    FileSaver workoutSubtypeSaver;

    public List<WorkoutSubtype> ReservedWorkoutSubtypes { get; }
    public List<WorkoutSubtype> CustomWorkoutSubtypes {get;}
    public List<User> Users { get; }
    // public List<Workout> Workouts {get;}
    // public List<MovementWorkoutDetails> MovementWorkoutDetailList {get; }
    // public List<WeightliftingWorkoutDetails> WeightliftingWorkoutDetailList {get;}

    public DataManager() {

        // Initialize and Load User Data
        Users = new List<User>();
        if(!File.Exists("users.txt")) {
            File.Create("users.txt").Close();
        }
        var usersFileContent = File.ReadAllLines("users.txt");

        foreach(var user in usersFileContent) {
            Users.Add(new User(user));
        }

        workoutSubtypeSaver = new FileSaver("custom-workout-subtypes.txt");

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

        CustomWorkoutSubtypes = new List<WorkoutSubtype>();
        var subTypesFromFile = File.ReadAllLines("custom-workout-subtypes.txt");
        foreach(var subtype in subTypesFromFile)
        {
            var splitted = subtype.Split(":",StringSplitOptions.RemoveEmptyEntries);
            var inputName = splitted[0];
            var inputType = splitted[1];    

            var workoutType = Enum.Parse<WorkoutType>(inputType);

            CustomWorkoutSubtypes.Add(new WorkoutSubtype(inputName, workoutType));
        }

    }

    public void SynchronizeUsers() {
        if (Users.Count() > 0)
        {
            File.Delete("users.txt");
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

}