using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ReadFromFile();
    }

    private void ReadFromFile()
    {
        songMidiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }
    public void GetDataFromMidi()
    {
        var notes = songMidiFile.GetNotes();
        Melanchall.DryWetMidi.Interaction.Note[] array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        for (int i = 0; i < lanes.Length; i++)
        {
            lanes[i].SetTimeStamps(array);
            noteIndex++;
        }

        Invoke(nameof(StartSong), songDelayInSeconds);
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
