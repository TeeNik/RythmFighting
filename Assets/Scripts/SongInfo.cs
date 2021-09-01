using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongInfo : MonoBehaviour
{
    public string Path;             // path to this song directory
    public string Title;            // title of the song
    public string Subtitle;         // subtitle of the song
    public string Artist;           // artists of the song
    public string TitleTranslit;    // transliteration of song title
    public string SubtitleTranslit; // transliteration of song subtitle
    public string ArtistTranslit;   // transliteration of song artist name
    public string Genre;            // genere
    public string Credit;           // credits
    public string Banner;           // song banner (filepath)
    public string Background;       // song background (filepath)
    public string LyricsPath;       // lyrics path
    public string CDTitle;          // title of CD
    public string Music;            // the actual song (filepath)
    public string Offset;           // offset
    public string SampleStart;      // starting point for sample
    public string SampleLength;     // duration of sample
    public string Selectable;       // is selectable
    public string DisplayBPM;       // BPM to be displayed
    public string BPMs;             // list of bpms (time=bpm, ...) list of pairs
    public string Stops;            // list of stops (time=duration, ...) list of pairs
    public string BGChanges;        // list of bg changes (??? - some other pairings) 
    public string KeySounds;        // keysounds (idk what these are)
    public string Attacks;          // attacks (idk what this means)
    public List<string> Notes;      // Notes of the song (per difficulty)

    public Queue<KeyValuePair<float, string>> NotesByTime = new Queue<KeyValuePair<float, string>>();

    public SongInfo(string path)
    {
        Path = path;
        Notes = new List<string>();
    }
}
