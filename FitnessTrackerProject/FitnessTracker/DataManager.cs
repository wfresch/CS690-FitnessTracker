using System.Security.Cryptography.X509Certificates;

namespace FitnessTracker;

public class DataManager {

    FileSaver mainworkoutSaver;
    FileSaver movementWorkoutSaver;
    FileSaver weightliftingWorkoutSaver;
    
    public List<WorkoutSubtype> ReservedWorkoutSubtypes { get; }
    public List<WorkoutSubtype> CustomWorkoutSubtypes {get;}
    
    public List<User> Users { get; }
    public List<Workout> Workouts {get;}
    public List<MovementWorkoutDetails> MovementWorkoutDetailList {get; }
    public List<WeightliftingWorkoutDetails> WeightliftingWorkoutDetailList {get;}
    private const int MOVEMENT_WORKOUT_TYPE = 0; 
    private const int WEIGHTLIFTING_WORKOUT_TYPE = 1; 
    
    public DataManager() {

        // Initialize and Load User Data
        Users = [];
        LoadUserData();
        
        // Initialize and Load Workout Types
        // These workout subtypes will exist regardless of the presence or state of data files
        ReservedWorkoutSubtypes =
        [
            new(new Guid("4AB3393B-B70E-4125-BD5A-5B2D87F1A282"), "Running", WorkoutType.Movement, true),
            new(new Guid("C30D9B3B-0833-4F48-9829-8C7CF82E3065"), "Walking", WorkoutType.Movement, true),
            new(new Guid("4C6AEDBC-0C85-4B53-8EC9-BE5FB16273A1"), "Biking", WorkoutType.Movement, true),
            new(new Guid("01E60368-3851-401E-A557-9E5589750CE2"), "Back Squat", WorkoutType.Weightlifting, true),
            new(new Guid("E67A8D8D-334F-40CE-9DFC-FF2FEAB511B7"), "Deadlift", WorkoutType.Weightlifting, true),
            new(new Guid("F0568158-E80A-4A25-B7F0-B18A22B8FAD6"), "Bench Press", WorkoutType.Weightlifting, true),
            new(new Guid("4E90E853-1115-4C28-AA1D-1805991EEEE4"), "Front Squat", WorkoutType.Weightlifting, true)
        ];

        CustomWorkoutSubtypes = [];
        LoadCustomWorkoutTypes();

        mainworkoutSaver = new FileSaver("main-workout-data.txt");
        movementWorkoutSaver = new FileSaver("movement-workout-data.txt");
        weightliftingWorkoutSaver = new FileSaver("weightlifting-workout-data.txt");

        Workouts = [];
        MovementWorkoutDetailList = [];
        WeightliftingWorkoutDetailList = [];
        
        var mainWorkoutFileContent = File.ReadAllLines("main-workout-data.txt");
        foreach(var line in mainWorkoutFileContent) {
            var splitted = line.Split(",",StringSplitOptions.RemoveEmptyEntries);
            var workoutId = Guid.Parse(splitted[0]);
            User workoutUser = GetUser(Guid.Parse(splitted[1]));
            DateTime workoutDate = DateTime.Parse(splitted[2]);
            WorkoutSubtype workoutSubtype = GetWorkoutSubtype(Guid.Parse(splitted[3]));

            Workouts.Add(new Workout(workoutId, workoutUser, workoutDate, workoutSubtype));
        }

        var movementWorkoutFileContent = File.ReadAllLines("movement-workout-data.txt");
        foreach(var line in movementWorkoutFileContent) {
            var splitted = line.Split(",",StringSplitOptions.RemoveEmptyEntries);
            var workoutId = Guid.Parse(splitted[0]);
            var distanceQuantity = int.Parse(splitted[1]);
            var distanceUnits = splitted[2];
            var timeQuantity = int.Parse(splitted[3]);
            var timeUnits = splitted[4];

            MovementWorkoutDetailList.Add(new MovementWorkoutDetails(workoutId, distanceQuantity, distanceUnits, timeQuantity, timeUnits));
        }

        var weightliftingWorkoutFileContent = File.ReadAllLines("weightlifting-workout-data.txt");
        foreach(var line in weightliftingWorkoutFileContent) {
            var splitted = line.Split(",",StringSplitOptions.RemoveEmptyEntries);
            var workoutId = Guid.Parse(splitted[0]);
            var weightQuantity = int.Parse(splitted[1]);
            var weightUnits = splitted[2];
            var numberOfSets = int.Parse(splitted[3]);
            var numberOfReps = int.Parse(splitted[4]);

            WeightliftingWorkoutDetailList.Add(new WeightliftingWorkoutDetails(workoutId, weightQuantity, weightUnits, numberOfSets, numberOfReps));
        }

    }

    public void TouchFile(string filename)
    {
        if(!File.Exists(filename)) {
            File.Create(filename).Close();
        }
    }

#region User methods
    private void LoadUserData()
    {
        TouchFile("users.txt");
        var usersFileContent = File.ReadAllLines("users.txt");

        foreach(var user in usersFileContent) {
            var splitted = user.Split(",",StringSplitOptions.RemoveEmptyEntries);
            var userId = Guid.Parse(splitted[0]);
            var userName = splitted[1];    

            Users.Add(new User(userName, userId));
        }
    }

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
                File.AppendAllText("users.txt",$"{user.UserId},{user.Name}{Environment.NewLine}");
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
        
        Users.Remove(user);
        SynchronizeUsers();
    }

    public User GetUser(Guid userId)
    {
        return Users.FirstOrDefault(x => x.UserId == userId)!;
    }

#endregion

#region Custom workout methods
    private void LoadCustomWorkoutTypes()
    {
        TouchFile("custom-workout-subtypes.txt");

        var subTypesFromFile = File.ReadAllLines("custom-workout-subtypes.txt");
        foreach(var subtype in subTypesFromFile)
        {
            var splitted = subtype.Split(",",StringSplitOptions.RemoveEmptyEntries);
            var inputId = Guid.Parse(splitted[0]);
            var inputName = splitted[1];
            var inputType = splitted[2];

            var workoutType = Enum.Parse<WorkoutType>(inputType);

            CustomWorkoutSubtypes.Add(new WorkoutSubtype(inputId, inputName, workoutType));
        }
    }

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

                File.AppendAllText(fileName, $"{customWorkoutSubtype.SubtypeId},{customWorkoutSubtype.Name},{workoutType}{Environment.NewLine}");
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

        CustomWorkoutSubtypes.Add(workoutSubtype);
        SynchronizeCustomWorkoutSubtypes();
        return true;
    }

    public List<WorkoutSubtype> GetWorkoutSubTypes(WorkoutType workoutType)
    {
        List<WorkoutSubtype> workoutSubtypes = [];
        workoutSubtypes = [.. ReservedWorkoutSubtypes.Where(x => x.WorkoutType == workoutType)];

        return [.. workoutSubtypes, .. CustomWorkoutSubtypes.Where(x => x.WorkoutType == workoutType)];
    }

    public void RenameCustomWorkoutSubtype(WorkoutSubtype workoutSubtype, string newName) {
        foreach (var subtype in CustomWorkoutSubtypes.Where(x => x.SubtypeId == workoutSubtype.SubtypeId))
        {
            subtype.Rename(newName);
        }

        SynchronizeCustomWorkoutSubtypes();
    }

    public void RemoveCustomWorkoutSubtype(WorkoutSubtype workoutSubtype) {
        //TODO: Remove related workouts
        
        CustomWorkoutSubtypes.Remove(workoutSubtype);
        SynchronizeCustomWorkoutSubtypes();
    }

    public WorkoutSubtype GetWorkoutSubtype(Guid subtypeId)
    {
        if(ReservedWorkoutSubtypes.Any(x => x.SubtypeId == subtypeId))
        {
            return ReservedWorkoutSubtypes.FirstOrDefault(x => x.SubtypeId == subtypeId)!;
        }

        return CustomWorkoutSubtypes.FirstOrDefault(x => x.SubtypeId == subtypeId)!;        
    }
#endregion

#region Workout log methods
    public Guid AddParentWorkoutDetails(User workoutUser, WorkoutSubtype subtype)
    {
        var workoutId = Guid.NewGuid();
        var workoutDate = DateTime.Now;

        var workout = new Workout(workoutId, workoutUser, workoutDate, subtype); 
        Workouts.Add(workout);
        mainworkoutSaver.AppendLine(workout.Storage());

        return workoutId;
    }

    public void AddMovementWorkoutDetails(Guid workoutId, int distanceQuantity, string distanceUnits, int timeQuantity, string timeUnits)
    {
        var movementWorkoutDetails = new MovementWorkoutDetails(workoutId, distanceQuantity, distanceUnits, timeQuantity, timeUnits);
        MovementWorkoutDetailList.Add(movementWorkoutDetails);
        movementWorkoutSaver.AppendLine(movementWorkoutDetails.Storage());
    }

    public void AddWeightliftingWorkoutDetails(Guid workoutId, int weightQuantity, string weightUnits, int numberOfSets, int numberOfReps)
    {
        var weightliftingWorkoutDetails = new WeightliftingWorkoutDetails(workoutId, weightQuantity, weightUnits, numberOfSets, numberOfReps);
        WeightliftingWorkoutDetailList.Add(weightliftingWorkoutDetails);
        weightliftingWorkoutSaver.AppendLine(weightliftingWorkoutDetails.Storage());
    }

    public bool GetMovementWorkoutDetails(Guid workoutId, out string workoutDetails)
    {
        workoutDetails = movementWorkoutSaver.GetLineDetails(workoutId.ToString());
        return !string.IsNullOrEmpty(workoutDetails);
    }

    public bool GetWeightliftingWorkoutDetails(Guid workoutId, out string workoutDetails)
    {
        workoutDetails = weightliftingWorkoutSaver.GetLineDetails(workoutId.ToString());
        return !string.IsNullOrEmpty(workoutDetails);
    }
    #endregion

    #region Workout reporting methods
    public List<string> GetWorkoutData(User workoutUser, DateTime startDate) => this.GetWorkoutData(workoutUser, startDate, DateTime.Now);

    public List<string> GetWorkoutData(User workoutUser, DateTime startDate, DateTime endDate)
    {
        List<string> workoutData = [];
        var userWorkouts = Workouts.Where(x => x.WorkoutUser == workoutUser && x.WorkoutDate >= startDate && x.WorkoutDate <= endDate);

        foreach(var userWorkout in userWorkouts)
        {
            var parentWorkoutDetails = userWorkout.ToString();

            if (MovementWorkoutDetailList.Any(x => x.WorkoutId == userWorkout.WorkoutId))
            {
                var movementWorkoutDetails = MovementWorkoutDetailList.FirstOrDefault(x => x.WorkoutId == userWorkout.WorkoutId);
                var workoutDetails = $"{parentWorkoutDetails}, {movementWorkoutDetails}";
                workoutData.Add(workoutDetails);
                continue;
            }

            if (WeightliftingWorkoutDetailList.Any(x => x.WorkoutId == userWorkout.WorkoutId))
            {
                var weightliftingWorkoutDetails = WeightliftingWorkoutDetailList.FirstOrDefault(x => x.WorkoutId == userWorkout.WorkoutId);
                var workoutDetails = $"{parentWorkoutDetails}, {weightliftingWorkoutDetails}";
                workoutData.Add(workoutDetails);
                continue;
            }
        }

        return workoutData;
    }

#endregion

}