using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public float songDelayInSeconds;
    public int inputDelayMiliseconds;
    public double marginOfError;
    public Lane[] lanes;

    public string fileLocation;
    public float noteTime;
    public float noteSpawnY;
    public float noteTapY;
    [SerializeField] int noteIndex;

    public float noteDespawnY { get { return noteTapY - (noteSpawnY - noteTapY); } }

    public static MidiFile songMidiFile;

    void Start()
    {
        Instance = this;
        fileLocation = GameData.MidiFileName;
        ReadFromFile();

    }

    private void ReadFromFile()
    {
        songMidiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);

        //songMidiFile = MidiFile.Read(path);
        GetDataFromMidi();
    }
        //string path = Application.dataPath + "/MidiFiles/" + fileLocation;
        //Debug.Log("Attempting to read file from path: " + path);

        /*if (System.IO.File.Exists(path))
        {
            songMidiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);

            songMidiFile = MidiFile.Read(path);
            GetDataFromMidi();
        }
        else
        {
            Debug.LogError($"File {fileLocation} not found at path: " + path);
        }
    }*/

    public void GetDataFromMidi()
    {
        var notes = songMidiFile.GetNotes();
        Melanchall.DryWetMidi.Interaction.Note[] array = notes.ToArray();

        for (int i = 0; i < lanes.Length; i++)
        {
            lanes[i].SetTimeStamps(array);
            noteIndex++;
        }

        StartSong();
    }

    public void StartSong()
    {
        audioSource.Play();
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
}
