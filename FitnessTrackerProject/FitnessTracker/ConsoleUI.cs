namespace FitnessTracker;

using Spectre.Console;


public class ConsoleUI {
    DataManager dataManager;
    User? selectedUser;
    
    public ConsoleUI() {
        dataManager = new DataManager();
    }

    public void Show()
    {
        string mode;

        do
        {
            mode = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Main Menu")
                    .AddChoices(new[] {
                        "User Mode","Admin Mode","Exit"
                    }));

            if (mode == "Admin Mode")
            {
                ShowAdminMode();
            }
            else if (mode == "User Mode")
            {
                ShowUserMode();
            }

        } while (mode != "Exit");
    }

    private void ShowAdminMode()
    {
        string command;

        do
        {
            command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Admin Options")
                    .AddChoices(new[] {
                        "Manage Users", "Manage Workout Types", "back"
                    }));

            if (command == "Manage Users")
                ShowAdminUserOptions();
            else if (command == "Manage Workout Types")
                ShowAdminWorkoutOptions();

        } while (command != "back");
    }

    private void ShowUserMode()
    {
        if (!dataManager.UsersExist())
        {
            RenderStyledMessage("Sorry, no users currently exist. Please create a user in Admin mode.", Color.Red);
            AnsiConsole.MarkupLine("[grey]Press any key to return to main menu...[/]");
            Console.ReadKey();
            return;
        }

        selectedUser = AnsiConsole.Prompt(
            new SelectionPrompt<User>()
                .Title("Select a User")
                .AddChoices(dataManager.Users));

        string command;

        do
        {
            command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("User Options")
                    .AddChoices(new[] {
                        "Log Workout", "View Stats", "back"
                    }));

            if (command == "Log Workout")
                ShowLogWorkoutOptions();
            else if (command == "View Stats")
                ShowViewStatsOptions();

        } while (command != "back");
    }

    private void ShowAdminUserOptions()
    {    
        string command;

        do {
            command = AnsiConsole.Prompt(
				                    new SelectionPrompt<string>()
				                        .Title("Manage Users")
				                        .AddChoices(new[] {
				                            "Add User", "Remove User", "back"
				                        }));

            if(command=="Add User") {
                var newUserName = AnsiConsole.Prompt(new TextPrompt<string>("Enter new user name:"));
                if (dataManager.AddUser(new User(newUserName)))
                {
                    RenderStyledMessage($"New user {newUserName} has been added.", Color.Green);
                }
                else
                {
                    RenderStyledMessage($"Sorry, user {newUserName} already exists.", Color.Red);
                }
            } else if(command=="Remove User") {
                if (!dataManager.UsersExist())
                {
                    RenderStyledMessage("Sorry, no users currently exist. Please create a user in Admin mode.", Color.Red);
                }
                else
                {
                    User selectedUser = AnsiConsole.Prompt(
                        new SelectionPrompt<User>()
                            .Title("Select a User")
                            .AddChoices(dataManager.Users));

                    var confirm = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title($"Are you sure you wish to delete {selectedUser.Name}?")
                        .AddChoices("No","Yes"));
                    
                    if (confirm == "Yes")
                    {
                        dataManager.RemoveUser(selectedUser);  
                        RenderStyledMessage($"Deleted user {selectedUser.Name}", Color.Green);  
                    }
                }
            }
         } while(command!="back");
    }

    private void ShowAdminWorkoutOptions()
    {    
        string command;

        do {

            command = AnsiConsole.Prompt(
				                    new SelectionPrompt<string>()
				                        .Title("Manage Workout Subtypes:")
				                        .AddChoices(new[] {
				                            "Add Workout Subtype", "Rename Workout Subtype", "Remove Workout Subtype", "back"
				                        }));

            if(command=="Add Workout Subtype") {
                var workoutType = AnsiConsole.Prompt(
                    new SelectionPrompt<WorkoutType>()
                        .Title("Please specify the overall workout category:")
                        .AddChoices(Enum.GetValues<WorkoutType>()));

                //Console.WriteLine($"workoutType is {workoutType.ToString()}");
                var newWorkoutSubtypeName = AnsiConsole.Prompt(new TextPrompt<string>("Enter new workout name:"));

                if (dataManager.AddCustomWorkoutSubtype(new WorkoutSubtype(newWorkoutSubtypeName, workoutType)))
                {
                    RenderStyledMessage($"New workout subtype {newWorkoutSubtypeName} has been added.", Color.Green);
                }
                else
                {
                    RenderStyledMessage($"Sorry, workout subtype {newWorkoutSubtypeName} already exists.", Color.Red);
                }
            } else if(command=="Remove Workout Subtype") {
                if (!dataManager.CustomWorkoutSubtypesExist())
                {
                    RenderStyledMessage("Sorry, no custom workout subtypes currently exist.", Color.Red);
                }
                else
                {
                    WorkoutSubtype selectedWorkoutSubtype = AnsiConsole.Prompt(
                        new SelectionPrompt<WorkoutSubtype>()
                            .Title("Select a custom workout subtype:")
                            .AddChoices(dataManager.CustomWorkoutSubtypes));

                    var confirm = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title($"Are you sure you wish to delete {selectedWorkoutSubtype.Name}?")
                        .AddChoices("No","Yes"));
                    
                    if (confirm == "Yes")
                    {
                        dataManager.RemoveCustomWorkoutSubtype(selectedWorkoutSubtype);  
                        RenderStyledMessage($"Removed workout type {selectedWorkoutSubtype}", Color.Green);    
                    }
                }
            } else if(command=="Rename Workout Subtype") {
                if (!dataManager.CustomWorkoutSubtypesExist())
                {
                    RenderStyledMessage("Sorry, no custom workout subtypes currently exist.", Color.Red);
                }
                else
                {
                    WorkoutSubtype selectedWorkoutSubtype = AnsiConsole.Prompt(
                        new SelectionPrompt<WorkoutSubtype>()
                            .Title("Select a custom workout subtype:")
                            .AddChoices(dataManager.CustomWorkoutSubtypes));

                    var oldWorkoutSubtypeName = selectedWorkoutSubtype.Name;
                    var newWorkoutSubtypeName = AnsiConsole.Prompt(new TextPrompt<string>("Enter new workout name:"));

                    var confirm = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title($"Are you sure you wish to rename {oldWorkoutSubtypeName} to {newWorkoutSubtypeName}?")
                        .AddChoices("No","Yes"));
                    
                    if (confirm == "Yes")
                    {
                        dataManager.RenameCustomWorkoutSubtype(selectedWorkoutSubtype, newWorkoutSubtypeName);
                        RenderStyledMessage($"Renamed workout {oldWorkoutSubtypeName} to {newWorkoutSubtypeName}", Color.Green);  
                    }
                }
            }
                
         } while(command!="back");
    }

    private void ShowLogWorkoutOptions()
    {    
        string command = "";

        do {

            var workoutType = AnsiConsole.Prompt(
                    new SelectionPrompt<WorkoutType>()
                        .Title("Please specify the overall workout category:")
                        .AddChoices(Enum.GetValues<WorkoutType>()));
            
            if (workoutType == WorkoutType.Movement)
            {
                int distanceQuantity = -1;
                DistanceUnits distanceUnits;
                int timeQuantity = -1;
                TimeUnits timeUnits;

                var selectedWorkoutSubtype
                     = AnsiConsole.Prompt(
				            new SelectionPrompt<WorkoutSubtype>()
				                .Title("Choose Movement Workout")
				                .AddChoices(dataManager.GetWorkoutSubTypes(WorkoutType.Movement)));

                distanceUnits = AnsiConsole.Prompt(
                    new SelectionPrompt<DistanceUnits>()
                        .Title("For the distance of this workout, please specify the units of distance:")
                        .AddChoices(Enum.GetValues<DistanceUnits>()));

                distanceQuantity = AnsiConsole.Prompt(new TextPrompt<int>($"Please specify the number of {distanceUnits}:"));
                
                timeUnits = AnsiConsole.Prompt(
                    new SelectionPrompt<TimeUnits>()
                        .Title("For the time of this workout, please specify the units of time:")
                        .AddChoices(Enum.GetValues<TimeUnits>()));

                timeQuantity = AnsiConsole.Prompt(new TextPrompt<int>($"Please specify the number of {timeUnits}:"));
                
                var workoutId = dataManager.AddParentWorkoutDetails(selectedUser!, selectedWorkoutSubtype);
                dataManager.AddMovementWorkoutDetails(workoutId, distanceQuantity, distanceUnits.ToString(), timeQuantity, timeUnits.ToString());
                RenderStyledMessage($"New {selectedWorkoutSubtype.Name} workout has been logged.", Color.Green);
                command = "back";
            }
            else if (workoutType == WorkoutType.Weightlifting)
            {
                int weightQuantity = -1;
                WeightUnits weightUnits;
                int numberOfSets = -1;
                int numberOfReps = -1;
                
                var selectedWorkoutSubtype
                     = AnsiConsole.Prompt(
				            new SelectionPrompt<WorkoutSubtype>()
				                .Title("Choose Weightlifting Workout")
				                .AddChoices(dataManager.GetWorkoutSubTypes(WorkoutType.Weightlifting)));

                weightUnits = AnsiConsole.Prompt(
                    new SelectionPrompt<WeightUnits>()
                        .Title("For the weight lifted in this workout, please specify the units of weight:")
                        .AddChoices(Enum.GetValues<WeightUnits>()));

                weightQuantity = AnsiConsole.Prompt(new TextPrompt<int>($"Please specify the number of {weightUnits}:"));
                numberOfSets = AnsiConsole.Prompt(new TextPrompt<int>("Please specify the number of sets:"));
                numberOfReps = AnsiConsole.Prompt(new TextPrompt<int>("Please specify the number of reps:"));
                
                var workoutId = dataManager.AddParentWorkoutDetails(selectedUser!, selectedWorkoutSubtype);
                dataManager.AddWeightliftingWorkoutDetails(workoutId, weightQuantity, weightUnits.ToString(), numberOfSets, numberOfReps);
                RenderStyledMessage($"New {selectedWorkoutSubtype.Name} workout has been logged.", Color.Green);
                command = "back";
            }
         } while(command!="back");
    }

    private void ShowViewStatsOptions()
    {    
        string command = "";

        do {

            string timespan = AnsiConsole.Prompt(
				                    new SelectionPrompt<string>()
				                        .Title("Manage Users")
				                        .AddChoices(new[] {
				                            "Last 7 Days", "Last 30 Days", "Last 365 Days", "back"
				                        }));

            var startDate = DateTime.MinValue;
            var endDate = DateTime.Now;
            if (timespan == "Last 7 Days")
            {
                startDate = endDate.AddDays(-7);
            }
            else if (timespan == "Last 30 Days")
            {
                startDate = endDate.AddDays(-30);
            }
            else if (timespan == "Last 365 Days")
            {
                startDate = endDate.AddDays(-365);
            }

            var workoutResults = dataManager.GetWorkoutReport(selectedUser!, startDate, endDate);
            if (workoutResults.Count == 0)
            {
                RenderStyledMessage("There are no workouts that match this timeframe", Color.Red);
                command = "back";
            }
            else
            {
                var table = new Table();

                table.AddColumn("Workout Details");

                foreach(var workoutResult in workoutResults) {
                    table.AddRow(workoutResult);
                }
                AnsiConsole.Write(table);
                command = "back";
            }
                        
         } while(command!="back");
    }

    private void RenderStyledMessage(string message, Color color)
    {
        var styled = new Text(message, new Style(foreground: color));
        AnsiConsole.Write(styled);
        AnsiConsole.WriteLine();
    }

}