using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using Directory = System.IO.Directory;
using File = System.IO.File;

public class StepParser : MonoBehaviour
{

    public TextAsset SongFile;
    public string Path;

    void Start()
    {
        var files = Directory.GetFiles(Path, "*.ssc");
        if (files.Length == 0)
        {
            return;
        }

        foreach(var file in files)
        {
            var stepmania = File.ReadAllText(files[0]).Trim();
            var metadata = new Regex("#.*?;", RegexOptions.Singleline).Match(stepmania);

            var isSongValid = true;
            var songItem = new SongInfo(Path);
            while (metadata.Success)
            {
                // get the key value pairs
                var datum = metadata.Value;
                var key = datum.Substring(0, datum.IndexOf(":")).Trim('#').Trim(':');
                var value = datum.Substring(datum.IndexOf(":")).Trim(':').Trim(';');

                switch (key.ToUpper())
                {
                    case "TITLE":
                        songItem.Title = value;
                        break;
                    case "SUBTITLE":
                        songItem.Subtitle = value;
                        break;
                    case "ARTIST":
                        songItem.Artist = value;
                        break;
                    case "TITLETRANSLIT":
                        songItem.TitleTranslit = value;
                        break;
                    case "SUBTITLETRANSLIT":
                        songItem.SubtitleTranslit = value;
                        break;
                    case "ARTISTTRANSLIT":
                        songItem.ArtistTranslit = value;
                        break;
                    case "GENRE":
                        songItem.Genre = value;
                        break;
                    case "CREDIT":
                        songItem.Credit = value;
                        break;
                    case "BANNER":
                        songItem.Banner = value;
                        break;
                    case "BACKGROUND":
                        songItem.Background = value;
                        break;
                    case "LYRICSPATH":
                        songItem.LyricsPath = value;
                        break;
                    case "CDTITLE":
                        songItem.CDTitle = value;
                        break;
                    case "MUSIC":
                        isSongValid = value.EndsWith(".ogg") || value.EndsWith(".wav");
                        songItem.Music = value;
                        break;
                    case "OFFSET":
                        songItem.Offset = value;
                        break;
                    case "SAMPLESTART":
                        songItem.SampleStart = value;
                        break;
                    case "SAMPLELENGTH":
                        songItem.SampleLength = value;
                        break;
                    case "DISPLAYBPM":
                        songItem.DisplayBPM = value;
                        break;
                    case "BPMS":
                        songItem.BPMs = value;
                        break;
                    case "STOPS":
                        songItem.Stops = value;
                        break;
                    case "BGCHANGES":
                        songItem.BGChanges = value;
                        break;
                    case "KEYSOUNDS":
                        songItem.KeySounds = value;
                        break;
                    case "ATTACKS":
                        songItem.Attacks = value;
                        break;
                    case "NOTES":
                        songItem.Notes.Add(value);
                        break;
                    default:
                        break;
                }
                metadata = metadata.NextMatch();
            }
            float time = 0.0f;
            string bpmStr = songItem.BPMs.Split('=')[1].TrimEnd('0', '.');
            float bpm = float.Parse(bpmStr);
            float offset = float.Parse(songItem.Offset, System.Globalization.CultureInfo.InvariantCulture);

            foreach(string pack in songItem.Notes)
            {
                List<string> notesInBeat = new List<string>();
                string[] lines = pack.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    bool isLastLine = i == lines.Length - 1;
                    if (line.Equals(",") || isLastLine)
                    {
                        float timeStep = 4 * 60.0f / bpm / notesInBeat.Count;
                        foreach (string note in notesInBeat)
                        {
                            songItem.NotesByTime.Enqueue(new KeyValuePair<float, string>(time - offset, note));
                            time += timeStep;
                        }
                        notesInBeat.Clear();
                    }
                    else if (!string.IsNullOrEmpty(line))
                    {
                        notesInBeat.Add(line);
                    }
                }
            }
        }
    }
}
