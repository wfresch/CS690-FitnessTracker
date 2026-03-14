namespace FitnessTracker.Tests;

using FitnessTracker;

public class FileSaverTests
{
    FileSaver fileSaver;
    string testFileName;

    public FileSaverTests() {
        testFileName = "test-doc.txt";
        File.Delete(testFileName);
        fileSaver = new FileSaver(testFileName);
    }

    [Fact]
    public void Test_FileSaver_Append()
    {
        fileSaver.AppendLine("Hello, World!");
        var contentFromFile = File.ReadAllText(testFileName);
        Assert.Equal("Hello, World!"+Environment.NewLine,contentFromFile);
    }

    [Fact]
    public void Test_FileSaver_AppendLine()
    {
        var sampleGUID = Guid.NewGuid();
        User sampleUser = new("TestUser", sampleGUID);
        DateTime sampleDate = DateTime.Now;
        WorkoutSubtype sampleSubtype = new(sampleGUID, "sampleMovement", WorkoutType.Movement);
        Workout sampleWorkout = new(sampleGUID, sampleUser, sampleDate, sampleSubtype);

        fileSaver.AppendLine(sampleWorkout.Storage());
        var contentFromFile = File.ReadAllText(testFileName);
        Assert.Equal($"{sampleGUID},{sampleGUID},{sampleDate},{sampleGUID}{Environment.NewLine}" ,contentFromFile);
    }
}