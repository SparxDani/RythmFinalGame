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
    CustomQueue<Note> notes = new CustomQueue<Note>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    public int inputIndex = 0;
    public static int totalTimeStamps;

    public static void ResetTotalTimeStamps()
    {
        totalTimeStamps = 0;
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        timeStamps.Clear();  
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
                    string feedbackMessage = GetFeedbackMessage(Math.Abs(audioTime - timeStamp), marginOfError);
                    Hit();
                    FeedbackManager.Instance.ShowFeedback(feedbackMessage, ScoreManager.ComboScore);
                    print($"Hit on {inputIndex} note");
                    Note noteToDestroy = notes.Dequeue();
                    if (noteToDestroy != null)
                    {
                        Destroy(noteToDestroy.gameObject);
                    }
                    inputIndex++;
                }
                else
                {
                    FeedbackManager.Instance.ShowFeedback("Miss", ScoreManager.ComboScore);
                    double accuracy = 100 - (Math.Abs(audioTime - timeStamp) / marginOfError * 10);
                    print($"Hit inaccurate on {inputIndex} note with {accuracy:F2}% accurate");
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                FeedbackManager.Instance.ShowFeedback("Miss", ScoreManager.ComboScore);
                print($"Missed {inputIndex} note");
                Note noteToDestroy = notes.Dequeue();
                if (noteToDestroy != null)
                {
                    Destroy(noteToDestroy.gameObject);
                }
                inputIndex++;
            }
        }
    }

    private string GetFeedbackMessage(double difference, double marginOfError)
    {
        if (difference < marginOfError / 4)
        {
            return "Excellent";
        }
        else if (difference < marginOfError / 2)
        {
            return "Great";
        }
        else if (difference < (3 * marginOfError) / 4)
        {
            return "Good";
        }
        else
        {
            return "Ok";
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
