using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;
using Directory = System.IO.Directory;

public class StepParser : MonoBehaviour
{

    public TextAsset SongFile;

    void Start()
    {
        var stepmania = SongFile.text;
        var metadata = new Regex("#.*?;", RegexOptions.Singleline).Match(stepmania);

        var files = Directory.GetFiles(song, "*.sm");

        return;
        while (metadata.Success)
        {
            metadata = metadata.NextMatch();
        }
    }



}
