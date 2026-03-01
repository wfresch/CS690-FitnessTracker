namespace FitnessTracker;

using Spectre.Console;


public class ConsoleUI {
    DataManager dataManager;

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
                    ShowUserOptions();
                }
                else if(command=="Manage Workout Types") {
                    ShowWorkoutOptions();
                }

            } while(command!="end");

        }

    }

    public void ShowUserOptions()
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

    public void ShowWorkoutOptions()
    {    
        string command;

        do {

            command = AnsiConsole.Prompt(
				                    new SelectionPrompt<string>()
				                        .Title("Manage Workout Types:")
				                        .AddChoices(new[] {
				                            "Add Workout", "Rename Workout", "Remove Workout", "back"
				                        }));

                // if(command=="Add Workout") {
                //     var newWorkoutTypeName = AnsiConsole.Prompt(new SelectionPrompt<string>()
                //         .Title("Select Workout Type:")
                //         .AddChoices(new[] { "Movement Workout", "Weightlifting Workout"}));
                //     var workoutType = (newWorkoutTypeName == "Movement Workout") ? WorkoutType.Movement : WorkoutType.Weightlifting;
                //     var newWorkoutName = AnsiConsole.Prompt(new TextPrompt<string>("Enter new workout name:"));
                //     dataManager.AddCustomWorkoutSubtype(new WorkoutSubtype(newWorkoutName, workoutType));
                // } else if(command=="Remove Workout") {
                //     Driver selectedDriver = AnsiConsole.Prompt(
				//             new SelectionPrompt<Driver>()
				//                 .Title("Select a workout")
				//                 .AddChoices(dataManager.Drivers));
                //     dataManager.RemoveDriver(selectedDriver);
                // } 
                // else if(command=="Rename Driver") {
                //     var table = new Table();

                //     table.AddColumn("Driver Name");

                //     foreach(var driver in dataManager.Drivers) {
                //         table.AddRow(driver.Name);
                //     }
                //     AnsiConsole.Write(table);
                // }
         } while(command!="back");
    }

}