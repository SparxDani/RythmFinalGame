using Melanchall.DryWetMidi.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    Queue<Note> notes = new Queue<Note>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    public int inputIndex = 0;
    public static int totalTimeStamps;

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Melanchall.DryWetMidi.Interaction.Note note = array[i];
            if (note.NoteName == noteRestriction)
            {
                MetricTimeSpan metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.songMidiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                totalTimeStamps++;
            }
        }
        ScoreManager.UpdateTotalNotes();
    }

    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                GameObject noteObj = Instantiate(notePrefab, transform);
                Note noteComponent = noteObj.GetComponent<Note>();
                noteComponent.assignedTime = (float)timeStamps[spawnIndex];
                notes.Enqueue(noteComponent);
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayMiliseconds / 1000.0);

            if (Input.GetKeyDown(input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit();
                    print($"Hit on {inputIndex} note");
                    Destroy(notes.Dequeue().gameObject);
                    inputIndex++;
                }
                else
                {
                    double accuracy = 100 - (Math.Abs(audioTime - timeStamp) / marginOfError * 10);
                    print($"Hit inaccurate on {inputIndex} note with {accuracy:F2}% accurate");
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                print($"Missed {inputIndex} note");
                notes.Dequeue(); 
                inputIndex++;
            }
        }
    }

    private void Hit()
    {
        ScoreManager.Hit();
    }

    private void Miss()
    {
        ScoreManager.Miss();
    }
}
