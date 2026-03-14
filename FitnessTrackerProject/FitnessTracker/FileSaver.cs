namespace FitnessTracker;

using System.IO;

public class FileSaver {
    string fileName;

    public FileSaver(string fileName) {
        this.fileName = fileName;
        if(!File.Exists(this.fileName)) {
            File.Create(this.fileName).Close();
        }
    }

    public void AppendLine(string line) {
        File.AppendAllText(this.fileName, line + Environment.NewLine);
    }

    // This method is primarily used to validate lines inserted by unit tests
    public string GetLineDetails(string searchText)
    {
        var lineDetails = File.ReadLines(fileName).FirstOrDefault(x => x.StartsWith(searchText));
        
        return lineDetails ?? "";
    }

}