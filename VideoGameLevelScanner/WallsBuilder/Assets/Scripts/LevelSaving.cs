using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Text;
using System.IO;
using System;

public class LevelSaving : MonoBehaviour {
    public LevelBuilder Level;
    public InputField LevelNameInput;
    public GameObject OverwriteWarningPanel;
    public GameObject NotSavedWarningPanel;
    public GameObject ScanningUI;
    public GameObject BuilderUI;
    private StringBuilder JsonBuilder;
    private DirectoryInfo LevelsDirectory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Video Game Level Scanner"));
    public void SaveFile(FileInfo file)
    {
        JsonBuilder = new StringBuilder();
        
        JsonBuilder.AppendLine("{");

        AppendName(JsonBuilder);
        AppendDate(JsonBuilder);
        AppendMatrix(JsonBuilder);
        AppendHeight(JsonBuilder);
        AppendWidth(JsonBuilder);
        AppendRooms(JsonBuilder);
        AppendDoors(JsonBuilder);

        JsonBuilder.AppendLine("}");

        var fileStream = file.AppendText();
        fileStream.Write(JsonBuilder.ToString());
        fileStream.Close();
    }
        
    public void TryToLeave()
    {
        if (Level.isSaved)
        {
            BuilderUI.SetActive(false);
            ScanningUI.SetActive(true);
        }
        else
        {
            NotSavedWarningPanel.SetActive(true);
        }

    }

    private void AppendDate(StringBuilder sb)
    {
        sb.Append("CreationDate: ");
        sb.AppendLine(DateTime.Now.ToString("o"));
    }

    private void AppendName(StringBuilder sb)
    {
        sb.Append("LevelName: ");
        sb.AppendLine(LevelNameInput.text);
    }

    private void AppendDoors(StringBuilder sb)
    {
        sb.AppendLine("Doors: [");
        //foreach (var passage in level.Doors)
        //{
        //    sb.Append("{from: ");
        //    AppendPoint(passage.From);
        //    sb.Append(", to: ");
        //    AppendPoint(passage.To);
        //    sb.Append("}");
        //    if (!passage.Equals(level.Doors.Last()))
        //        sb.AppendLine(",");
        //}
        sb.AppendLine();
        sb.AppendLine("]");
    }

    private void AppendRooms(StringBuilder sb)
    {
        sb.AppendLine("Rooms: [");
        foreach (var room in Level.Rooms)
        {
            sb.Append("{N:");
            sb.Append(room.N);
            sb.Append(", FloorColor: [");
            sb.Append(room.FloorMaterial.color.a);
            sb.Append(",");
            sb.Append(room.FloorMaterial.color.r);
            sb.Append(",");
            sb.Append(room.FloorMaterial.color.g);
            sb.Append(",");
            sb.Append(room.FloorMaterial.color.b);
            sb.Append("]}");
            if (!room.Equals(Level.Rooms.Last()))
                sb.AppendLine(",");
        }
        sb.AppendLine();
        sb.AppendLine("],");
    }

    private void AppendWidth(StringBuilder sb)
    {
        sb.Append("Width: ");
        sb.Append(Level.matrix.GetLength(1));
        sb.AppendLine(",");
    }

    private void AppendHeight(StringBuilder sb)
    {
        sb.Append("Height: ");
        sb.Append(Level.matrix.GetLength(0));
        sb.AppendLine(",");
    }

    private void AppendMatrix(StringBuilder sb)
    {
        sb.Append("Matrix: [");
        sb.Append(string.Join(",", Array.ConvertAll(Level.matrix.Cast<int>().ToArray(),integer => integer.ToString())));
        sb.AppendLine("],");
    }

    private void AppendPoint(System.Drawing.Point point)
    {
        JsonBuilder.Append("[");
        JsonBuilder.Append(point.X);
        JsonBuilder.Append(",");
        JsonBuilder.Append(point.Y);
        JsonBuilder.Append("]");
    }

    public void TrySaving(bool overwriting)
    {
        string fileName = PrepareFileName(LevelNameInput.text);
        var newLevelFileInfo = new FileInfo(Path.Combine(LevelsDirectory.FullName, fileName));
        if (!newLevelFileInfo.Exists || overwriting)
        {
            var file = newLevelFileInfo.Create();
            file.Close();
            SaveFile(newLevelFileInfo);
            Level.isSaved = true;
        }
        else
        {
            OverwriteWarningPanel.SetActive(true);
        }
    }

    private string PrepareFileName(string levelName)
    {
        string properFileName = levelName;
        foreach(var symbol in Path.GetInvalidFileNameChars())
            properFileName.Replace(char.ToString(symbol),string.Empty);
        return properFileName;

    }
    private void Awake()
    {
        CreateScannerDirectory();
    }

    private void CreateScannerDirectory()
    {
        if (!LevelsDirectory.Exists)
            LevelsDirectory.Create();
    }
}
