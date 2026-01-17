using System.Numerics;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json; 
class Program
{
    /******
    FIND FILES
    *******/
    static IEnumerable<string> FindFiles(string folderName)
    {
        List<string> salesFiles = new List<string>();

        var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

        foreach (var file in foundFiles)
        {
            var extension = Path.GetExtension(file);
            if (extension == ".json")
            {
                salesFiles.Add(file);
            }
        }

        return salesFiles;
    }

    /******
    CALCULATE SALES TOTALS
    *******/
    static double CalculateSalesTotal(IEnumerable<string> salesFiles)
    {
        double salesTotal = 0;
        
        // Loop over each file path in salesFiles
        foreach (var file in salesFiles)
        {      
            // Read the contents of the file
            string salesJson = File.ReadAllText(file);
        
            // Parse the contents as JSON
            SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        
            // Add the amount found in the Total field to the salesTotal variable
            salesTotal += data?.Total ?? 0;
        }
        
        return salesTotal;
    }

    static void GenerateReport(double salesTotal, Dictionary<string, double> fileTotals, string filepath)
    {
        string report = "";

        report += "Sales Summary" + Environment.NewLine;
        report += "------------------------------" + Environment.NewLine;
        report += $"Total Sales:{salesTotal:C}" + Environment.NewLine; 
        report += "Details:" + Environment.NewLine;
        report += "------------------------------" + Environment.NewLine;

        foreach(var file in fileTotals)
        {
            report += $"Filename: {Path.GetFileName(file.Key)}: {file.Value:C}" + Environment.NewLine;
        }

        File.WriteAllText(filepath, report); 
    }
    static Dictionary<string, double> CalculateFileTotals(IEnumerable<string> files)
    {
        var myDict = new Dictionary<string, double>();

        foreach (var file in files)
        {
            string salesJson = File.ReadAllText(file);

            SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

            myDict.Add(file, data?.Total ?? 0);
        }
            return myDict;

    }
    record SalesData (double Total);
    static void Main(String[] args)
    {

        // Set up directories
        var currentDirectory = Directory.GetCurrentDirectory();
        var storesDirectory = Path.Combine(currentDirectory, "stores");
        var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");

        Directory.CreateDirectory(salesTotalDir);   

        // Find the files\
        var salesFiles = FindFiles(storesDirectory);

        // Calculate the total
        var salesTotal = CalculateSalesTotal(salesFiles);

        // Calculate file totals
        var fileTotals = CalculateFileTotals(salesFiles);

        // Write to file
        File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

        // Generate Report
        GenerateReport(salesTotal, fileTotals, Path.Combine(salesTotalDir, "report.txt"));
    }
}

