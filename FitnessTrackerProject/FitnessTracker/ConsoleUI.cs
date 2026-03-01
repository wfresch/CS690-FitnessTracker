namespace FitnessTracker;

using Spectre.Console;


public class ConsoleUI {
    DataManager dataManager;
    User selectedUser;
    //WorkoutSubtype selectedWorkoutSubtype;

    public ConsoleUI() {
        dataManager = new DataManager();
    }

    public void Show() {
        
        var mode = AnsiConsole.Prompt(
				            new SelectionPrompt<string>()
				                .Title("Main Menu")
				                .AddChoices(new[] {
				                    "User Mode","Admin Mode"
				                }));

        if (mode == "Admin Mode")
        {
            string command;

            do {
                
                command = AnsiConsole.Prompt(
				                    new SelectionPrompt<string>()
				                        .Title("Admin Options")
				                        .AddChoices(new[] {
				                            "Manage Users", "Manage Workout Types", "end"
				                        }));

                if(command=="Manage Users") {
                    ShowAdminUserOptions();
                }
                else if(command=="Manage Workout Types") {
                    ShowAdminWorkoutOptions();
                }

            } while(command!="end");

        }
        else if (mode == "User Mode")
        {
            if (!dataManager.UsersExist())
            {
                var styled = new Text("Sorry, no users currently exist.", new Style(foreground: Color.Red));
                AnsiConsole.Write(styled);
                AnsiConsole.WriteLine();
            }
            else {
                selectedUser
                     = AnsiConsole.Prompt(
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
                                                "Log Workout", "View Stats", "end"
                                            }));
                    if(command=="Log Workout") {
                        ShowLogWorkoutOptions();
                    }
                    else if(command=="View Stats") {
                        //ShowViewStatsOptions();
                    }

                } while(command!="end");
            }
        }
    }

    public void ShowAdminUserOptions()
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
                    var styled = new Text($"New user {newUserName} has been added.", new Style(foreground: Color.Green));
                    AnsiConsole.Write(styled);
                }
                else
                {
                    var styled = new Text($"Sorry, user {newUserName} already exists.", new Style(foreground: Color.Red));
                    AnsiConsole.Write(styled);
                }
                AnsiConsole.WriteLine();
            } else if(command=="Remove User") {
                if (!dataManager.UsersExist())
                {
                    var styled = new Text("Sorry, no users currently exist.", new Style(foreground: Color.Red));
                    AnsiConsole.Write(styled);
                    AnsiConsole.WriteLine();
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
                    }
                }
            }
         } while(command!="back");
    }

    public void ShowAdminWorkoutOptions()
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
                    var styled = new Text($"New workout subtype {newWorkoutSubtypeName} has been added.", new Style(foreground: Color.Green));
                    AnsiConsole.Write(styled);
                }
                else
                {
                    var styled = new Text($"Sorry, workout subtype {newWorkoutSubtypeName} already exists.", new Style(foreground: Color.Red));
                    AnsiConsole.Write(styled);
                }
                AnsiConsole.WriteLine();
            } else if(command=="Remove Workout Subtype") {
                if (!dataManager.CustomWorkoutSubtypesExist())
                {
                    var styled = new Text("Sorry, no custom workout subtypes currently exist.", new Style(foreground: Color.Red));
                    AnsiConsole.Write(styled);
                    AnsiConsole.WriteLine();
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
                    }
                }
            } else if(command=="Rename Workout Subtype") {
                if (!dataManager.CustomWorkoutSubtypesExist())
                {
                    var styled = new Text("Sorry, no custom workout subtypes currently exist.", new Style(foreground: Color.Red));
                    AnsiConsole.Write(styled);
                    AnsiConsole.WriteLine();
                }
                else
                {
                    WorkoutSubtype selectedWorkoutSubtype = AnsiConsole.Prompt(
                        new SelectionPrompt<WorkoutSubtype>()
                            .Title("Select a custom workout subtype:")
                            .AddChoices(dataManager.CustomWorkoutSubtypes));

                    var newWorkoutSubtypeName = AnsiConsole.Prompt(new TextPrompt<string>("Enter new workout name:"));

                    var confirm = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title($"Are you sure you wish to rename {selectedWorkoutSubtype.Name} to {newWorkoutSubtypeName}?")
                        .AddChoices("No","Yes"));
                    
                    if (confirm == "Yes")
                    {
                        dataManager.RenameCustomWorkoutSubtype(selectedWorkoutSubtype, newWorkoutSubtypeName);
                    }
                }
            }
                
         } while(command!="back");
    }

    public void ShowLogWorkoutOptions()
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
                
                //We have all we need for a MovementWorkoutDetails object
                //Remember, we also need: WorkoutId, User, WorkoutDate, Subtype
                // First, create a Workout record, with GUID, user, date, and subtype
                // Then, create a MovementWorkoutDetails record, tieing back to Workout GUID

            }




            
                
         } while(command!="back");
    }


}