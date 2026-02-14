# CS690-FitnessTracker
An app for tracking fitness progress

## Scenario - A Day in the Life of Ryan

### Morning
Ryan wakes up feeling a mix of determination and frustration. Last night, Ryan decided it was time to get back into shape. The running shoes are out, a new workout playlist is queued, and a granola bar replaces the usual sugary cereal.

But as Ryan ties the laces, a thought creeps in: “How will I know if I’m making progress?” It’seasy to start strong, but staying consistent has always been the hard part.

### Late Morning
Ryan manages a quick run before heading to work. Itwasn’ta long run—just a couple of laps around the block—but it feels good to get moving.

At work, a colleague mentions their gym routine and howthey’vebeen tracking their progress. “I use an app to log my workouts, but honestly, it’s too complicated. I end up skipping it half the time,” they say. Ryan nods, thinking about how simplicityis key.

### Lunch
Over lunch, Ryan starts brainstorming how to track progress. A notebook seems old-fashioned, and most fitness apps seem too cluttered with features.

Ryan jots some notes on a sticky pad:
* “Log workouts.”
* “Track weight or reps.”
* “See progress over time.”

But the sticky note gets lost in the shuffle of other papers. By the time lunch is over, Ryan has forgotten about it.

### Afternoon
Ryan gets a notification from a fitness tracker app installed months ago but never used.It’san overwhelming reminder about "reaching step goals," “hydration tracking,” and a dozen other things that don’t feel relevant.

Ryan dismisses it and mutters, “I just want something simple to track my workouts.”

### Evening
Back home, Ryan thinks about the day’s run and wonders how to track progress in a way that doesn’t feel like a chore. Should it just be a log of runs? Maybe tracking the time or distance could help show improvement.

Ryan flips through a pile of old notes but doesn’t find any inspiration.It’s clear something needs to change, but what? For now, the plan is to just keep running and figure it out later.

## Business Requirements
We create a software that tracks fitness progress in an easy manner.

## Use Cases, including Functional and Non-Functional Requirements

Use Cases are given below, and each includes related Functional (FRs) and Non-Functional Requirements (NFRs).  Note that there are several FRs and NFRs that are common among multiple Use-Cases.  This is because there are certain menus and prompts that must exist in order for multiple features to be delivered.  For instance, an Admin menu must exist before any Users or Workout types can be added or removed.

### Collect workout data
#### UC1: Collect movement data
Actor: User
Flow:
1. User opens app
2. User chooses User menu
3. User signs-in
4. User chooses Log Workout
5. User chooses Movement workout
6. User chooses workout subtype (Run, Walk, Bike, etc)
7. User inputs distance units
8. User inputs distance quantity
9. User inputs time units
10. User inputs time quantity
11. System stores this data

[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR2: For User mode, provide a menu to choose User](https://github.com/wfresch/CS690-FitnessTracker/issues/2)\
[NFR1: If no users exist, provide a message asking user to first create a User in Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/3)\
[FR3: When User mode is chosen, provide a menu to Log Workout or View Stats](https://github.com/wfresch/CS690-FitnessTracker/issues/4)\
[FR4: When Log Workout is chosen, provide a menu to select type of Workout: Movement or Weightlifting](https://github.com/wfresch/CS690-FitnessTracker/issues/5)\
[FR5: When Movement workout type is chosen, provide a menu to select a subtype: Run, Walk, Bike, and more](https://github.com/wfresch/CS690-FitnessTracker/issues/6)\
[FR6: For any Movement type workout, prompt user for the following information](https://github.com/wfresch/CS690-FitnessTracker/issues/7):
* Distance units (Miles or Kilometers)
* Distance quantity (numeric decimal, greater than zero, less than 1000)
* Time units (Minutes or Hours)
* Time quantity (numeric decimal, greater than zero, less than 1000)

[FR7: The system shall store Movement Workout data in a persistent way](https://github.com/wfresch/CS690-FitnessTracker/issues/8)

#### UC2: Collect weightlifting data
Actor: User
Flow:
1. User opens app
2. User chooses User menu
3. User signs-in
4. User chooses Log Workout
5. User chooses Weightlifting workout
6. User chooses workout subtype (Back Squat, Front Squat, Bench Press, etc)
7. User inputs weight units
8. User inputs weight quantity
9. User inputs number of sets
10. User inputs number of reps
11. System stores this data

[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR2: For User mode, provide a menu to choose User](https://github.com/wfresch/CS690-FitnessTracker/issues/2)\
[NFR1: If no users exist, provide a message asking user to first create a User in Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/3)\
[FR3: When User mode is chosen, provide a menu to Log Workout or View Stats](https://github.com/wfresch/CS690-FitnessTracker/issues/4)\
[FR4: When Log Workout is chosen, provide a menu to select type of Workout: Movement or Weightlifting](https://github.com/wfresch/CS690-FitnessTracker/issues/5)\
[FR8: When Weightlifting workout type is chosen, provide a menu to select a subtype](https://github.com/wfresch/CS690-FitnessTracker/issues/9):
* Back Squat
* Front Squat
* Dead Lift
* Bench Press

[FR9: For any Weightlifting type workout, prompt user for the following information](https://github.com/wfresch/CS690-FitnessTracker/issues/10):
* Weight units (Pounds or Kilograms)
* Weight quantity (numeric decimal, greater than zero, less than 1000)
* Number of Sets
* Number of Reps

[FR10: The system shall store Weightlifting Workout data in a persistent way](https://github.com/wfresch/CS690-FitnessTracker/issues/11)

### Show workout data
#### UC11: Show workout data
Actor: User
Flow:
1. User opens app
2. User chooses User menu
3. User signs-in
4. User chooses View Workouts
5. User chooses a timeframe
6. System lists workouts from the given timeframe
   
[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR2: For User mode, provide a menu to choose User](https://github.com/wfresch/CS690-FitnessTracker/issues/2)\
[NFR1: If no users exist, provide a message asking user to first create a User in Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/3)\
[FR3: When User mode is chosen, provide a menu to Log Workout or View Stats](https://github.com/wfresch/CS690-FitnessTracker/issues/4)\
[FR32: When View Workouts is chosen, prompt user to specify timeframe; options are](https://github.com/wfresch/CS690-FitnessTracker/issues/12):
* Last 7 Days
* Last 30 Days
* Last 365 Days
* Custom
  
[FR33: When Custom timeframe is chosen, prompt user for start-date and stop-date](https://github.com/wfresch/CS690-FitnessTracker/issues/13)\
[NFR13: When Custom timeframe is chosen, dates must be validated)](https://github.com/wfresch/CS690-FitnessTracker/issues/14)\
[FR34: For the chosen timeframe, display a list of workouts. Each workout should include](https://github.com/wfresch/CS690-FitnessTracker/issues/15):
* Date
* Workout Category
* Workout Type
* Workout Details

###	Manage Users
#### UC3: Add a User
Actor: Admin
Flow:
1. User opens app
2. User chooses Admin menu
3. Admin chooses Manage Users
4. Admin chooses Add User
5. Admin inputs a User's name
6. System stores this data
   
[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR11: For Admin mode, provide a menu to Manage Users or Manage Workout Types](https://github.com/wfresch/CS690-FitnessTracker/issues/16)\
[FR12: When Manage Users is chosen, provide a menu to Add a User or Remove a User](https://github.com/wfresch/CS690-FitnessTracker/issues/17)\
[FR13: When Add a User is chosen, prompt for the User's name](https://github.com/wfresch/CS690-FitnessTracker/issues/18)\
[FR14: The system shall store User data in a persistent way](https://github.com/wfresch/CS690-FitnessTracker/issues/19)\
[NFR2: When adding a user with a duplicate name, prompt Admin to use a different name or cancel.](https://github.com/wfresch/CS690-FitnessTracker/issues/20)

#### UC4: Remove a User
Actor: Admin
Flow:
1. User opens app
2. User chooses Admin menu
3. Admin chooses Manage Users
4. Admin chooses Remove User
5. Admin chooses a User's name
6. Admin is prompted with a confirmation message, and confirms
7. System removes this data

[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR11: For Admin mode, provide a menu to Manage Users or Manage Workout Types](https://github.com/wfresch/CS690-FitnessTracker/issues/16)\
[FR12: When Manage Users is chosen, provide a menu to Add a User or Remove a User](https://github.com/wfresch/CS690-FitnessTracker/issues/17)\
[NFR3: If no users exist and Remove a User is chosen, display a message stating that no users exist.](https://github.com/wfresch/CS690-FitnessTracker/issues/21)\
[FR15: When Remove a User is chosen, provide a menu of existing users](https://github.com/wfresch/CS690-FitnessTracker/issues/22)\
[NFR4: Before removing a user, prompt Admin with a confirmation message (Are you sure?)](https://github.com/wfresch/CS690-FitnessTracker/issues/23)\
[FR16: Upon confirmation, the system shall remove User and associated workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/24)

### Manage Workouts > Manage Movement Workout Types
#### UC5: Add movement workout
Actor: Admin
Flow:
1. User opens app
2. User chooses Admin menu
3. Admin chooses Manage Movement Workouts
4. Admin chooses Add Movement Workout
5. Admin types Movement name
6. System stores this data

[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR11: For Admin mode, provide a menu to Manage Users or Manage Workout Types](https://github.com/wfresch/CS690-FitnessTracker/issues/16)\
[FR17: When Manage Workouts is chosen, provide a menu to Manage Movement Workouts or Manage Weightlifting Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/27)\
[FR18: When Manage Movement Workouts is chosen, provide a menu to Add, Rename, or Remove Movement Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/28)\
[FR19: When Add Movement Workout is chosen, prompt Admin for name of Movement](https://github.com/wfresch/CS690-FitnessTracker/issues/29)\
[NFR5: When adding or renaming a Movement Workout with a duplicate name, prompt Admin to use a different name or cancel.](https://github.com/wfresch/CS690-FitnessTracker/issues/30)\
[FR20: The system shall store Movement Workout data in a persistent way](https://github.com/wfresch/CS690-FitnessTracker/issues/29)\
[NFR6: A default set of Movement Workouts exist and cannot be modified or removed.  These workouts include](https://github.com/wfresch/CS690-FitnessTracker/issues/32):
* Running
* Walking
* Biking

#### UC6: Rename movement workout
Actor: Admin
Flow:
1. User opens app
2. User chooses Admin menu
3. Admin chooses Manage Movement Workouts
4. Admin chooses Rename Movement Workout
5. Admin chooses Movement name
6. Admin types new name for chosen Movement
7. System stores this data
   
[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR11: For Admin mode, provide a menu to Manage Users or Manage Workout Types](https://github.com/wfresch/CS690-FitnessTracker/issues/16)\
[FR17: When Manage Workouts is chosen, provide a menu to Manage Movement Workouts or Manage Weightlifting Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/27)\
[FR18: When Manage Movement Workouts is chosen, provide a menu to Add, Rename, or Remove Movement Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/28)\
[FR20: The system shall store Movement Workout data in a persistent way](https://github.com/wfresch/CS690-FitnessTracker/issues/29)\
[NFR6: A default set of Movement Workouts exist and cannot be modified or removed.  These workouts include](https://github.com/wfresch/CS690-FitnessTracker/issues/32):
* Running
* Walking
* Biking

[NFR7: If no custom Movement workout types exist and Remove Movement Workout is chosen, display a message.](https://github.com/wfresch/CS690-FitnessTracker/issues/31)\
[FR21: Once a Movement Workout is chosen for rename, prompt user for new Movement name.](https://github.com/wfresch/CS690-FitnessTracker/issues/32)\
[NFR5: When adding or renaming a Movement Workout with a duplicate name, prompt Admin to use a different name or cancel.](https://github.com/wfresch/CS690-FitnessTracker/issues/28)\
[NFR8: Before renaming or removing a Movement workout, prompt Admin with a confirmation message (Are you sure?)](https://github.com/wfresch/CS690-FitnessTracker/issues/33)\
[FR16: Upon confirmation, the system shall rename the Movement workout type and modify all associated workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/24)

#### UC7: Remove movement workout
Actor: Admin
Flow:
1. User opens app
2. User chooses Admin menu
3. Admin chooses Manage Movement Workouts
4. Admin chooses Remove Movement Workout
5. Admin chooses Movement name
6. Admin is prompted with a confirmation message, and confirms
7. System removes this data

[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR11: For Admin mode, provide a menu to Manage Users or Manage Workout Types](https://github.com/wfresch/CS690-FitnessTracker/issues/16)\
[FR17: When Manage Workouts is chosen, provide a menu to Manage Movement Workouts or Manage Weightlifting Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/27)\
[FR18: When Manage Movement Workouts is chosen, provide a menu to Add, Rename, or Remove Movement Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/28)\
[FR22: When Remove Movement Workout is chosen, provide a menu for existing Movement types](https://github.com/wfresch/CS690-FitnessTracker/issues/35)\
[NFR6: A default set of Movement Workouts exist and cannot be modified or removed.  These workouts include](https://github.com/wfresch/CS690-FitnessTracker/issues/32):
* Running
* Walking
* Biking

[NFR7: If no custom Movement workout types exist and Remove Movement Workout is chosen, display a message.](https://github.com/wfresch/CS690-FitnessTracker/issues/31)\
[NFR8: Before renaming or removing a Movement workout, prompt Admin with a confirmation message (Are you sure?)](https://github.com/wfresch/CS690-FitnessTracker/issues/33)\
[FR23: Upon confirmation, the system shall remove the Movement workout type and remove all associated workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/36)

### Manage Workouts > Manage Weightlifting Workout Types
#### UC8: Add weightlifting workout
Actor: Admin
Flow:
1. User opens app
2. User chooses Admin menu
3. Admin chooses Manage Weightlifting Workouts
4. Admin chooses Add Weightlifting Workout
5. Admin types Weightlifting exercise name
6. System stores this data
   
[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR11: For Admin mode, provide a menu to Manage Users or Manage Workout Types](https://github.com/wfresch/CS690-FitnessTracker/issues/16)\
[FR17: When Manage Workouts is chosen, provide a menu to Manage Movement Workouts or Manage Weightlifting Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/27)\
[FR24: When Manage Weightlifting Workout Types is chosen, provide a menu to Add, Rename, or Remove Weightlifting Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/37)\
[FR25: When Add Weightlifting Workout is chosen, prompt Admin for name of Weightlifting exercise](https://github.com/wfresch/CS690-FitnessTracker/issues/38)\
[NFR9: When adding or renaming a Weightlifting Workout with a duplicate name, prompt Admin to use a different name or cancel.](https://github.com/wfresch/CS690-FitnessTracker/issues/39)\
[FR26: The system shall store Weightlifting Workout data in a persistent way](https://github.com/wfresch/CS690-FitnessTracker/issues/40)\
[NFR10: A default set of Weightlifting Workouts exist and cannot be modified or removed. These Movement workouts include](https://github.com/wfresch/CS690-FitnessTracker/issues/41):
* Back Squats
* Front Squats
* Dead Lift
* Bench Press

#### UC9: Rename weightlifting workout
Actor: Admin
Flow:
1. User opens app
2. User chooses Admin menu
3. Admin chooses Manage Weightlifting Workouts
4. Admin chooses Rename Weightlifting Workout
5. Admin chooses Weightlifting name
6. Admin types new name for chosen Weightlifting exercise
7. System stores this data

[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR11: For Admin mode, provide a menu to Manage Users or Manage Workout Types](https://github.com/wfresch/CS690-FitnessTracker/issues/16)\
[FR17: When Manage Workouts is chosen, provide a menu to Manage Movement Workouts or Manage Weightlifting Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/27)\
[FR24: When Manage Weightlifting Workout Types is chosen, provide a menu to Add, Rename, or Remove Weightlifting Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/37)\
[FR27: When Rename Weightlifting Workout is chosen, provide a menu for existing Weightlifting types](https://github.com/wfresch/CS690-FitnessTracker/issues/42)\
[NFR10: A default set of Weightlifting Workouts exist and cannot be modified or removed. These Movement workouts include](https://github.com/wfresch/CS690-FitnessTracker/issues/41):
* Back Squats
* Front Squats
* Dead Lift
* Bench Press

[NFR11: If no custom Weightlifting workout types exist and Rename or Remove Weightlifting Workout is chosen, display a message.](https://github.com/wfresch/CS690-FitnessTracker/issues/43)\
[FR28: Once a Weightlifting Workout is chosen for rename, prompt user for new Weightlifting name.](https://github.com/wfresch/CS690-FitnessTracker/issues/44)\
[NFR9: When adding or renaming a Weightlifting Workout with a duplicate name, prompt Admin to use a different name or cancel.](https://github.com/wfresch/CS690-FitnessTracker/issues/39)\
[NFR12: Before renaming or removing a Weightlifting workout, prompt Admin with a confirmation message (Are you sure?)](https://github.com/wfresch/CS690-FitnessTracker/issues/45)\
[FR29: Upon confirmation, the system shall rename the Weightlifting workout type and modify all associated workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/46)

#### UC10: Remove weightlifting workout
Actor: Admin
Flow:
1. User opens app
2. User chooses Admin menu
3. Admin chooses Manage Weightlifting Workouts
4. Admin chooses Remove Weightlifting Workout
5. Admin chooses Weightlifting name
6. Admin is prompted with a confirmation message, and confirms
7. System removes this data

[FR1: Provide a Main menu allowing users to choose User or Admin mode](https://github.com/wfresch/CS690-FitnessTracker/issues/1)\
[FR11: For Admin mode, provide a menu to Manage Users or Manage Workout Types](https://github.com/wfresch/CS690-FitnessTracker/issues/16)\
[FR17: When Manage Workouts is chosen, provide a menu to Manage Movement Workouts or Manage Weightlifting Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/27)\
[FR24: When Manage Weightlifting Workout Types is chosen, provide a menu to Add, Rename, or Remove Weightlifting Workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/37)\
[FR30: When Remove Weightlifting Workout is chosen, provide a menu for existing Weightlifting types](https://github.com/wfresch/CS690-FitnessTracker/issues/47)\
[NFR10: A default set of Weightlifting Workouts exist and cannot be modified or removed. These Movement workouts include](https://github.com/wfresch/CS690-FitnessTracker/issues/41):
* Back Squats
* Front Squats
* Dead Lift
* Bench Press

[NFR11: If no custom Weightlifting workout types exist and Rename or Remove Weightlifting Workout is chosen, display a message.](https://github.com/wfresch/CS690-FitnessTracker/issues/43)\
[NFR12: Before renaming or removing a Weightlifting workout, prompt Admin with a confirmation message (Are you sure?)](https://github.com/wfresch/CS690-FitnessTracker/issues/45)\
[FR31: Upon confirmation, the system shall remove the Movement workout type and remove all associated workouts](https://github.com/wfresch/CS690-FitnessTracker/issues/48)



