using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static string MidiFileName;
    public static AudioClip AudioClip;
    public static string songName;
    //public static string Username;
    public static string NextScene;
    public static int ComboScore;
    public static int TotalScore;
    public static int MissedNotes;
    public static int HitedNotes;
    public static int OkNotes;
    public static int GoodNotes;
    public static int GreatNotes;
    public static int ExcellentNotes;
    public static int finalScore;
    public static int[] topScores;
    public static SongsSO currentSongData;

    /*private static CustomPriorityQueue priorityQueue;

    private void Start()
    {
        finalScore = CalculateFinalScore();
        priorityQueue = new CustomPriorityQueue();
    }*/

    public static int CalculateFinalScore()
    {
        return TotalScore + (OkNotes * 15) + (GoodNotes * 30) + (GreatNotes * 50) + (ExcellentNotes * 75);
    }
   /* public static void SendScoreAndReturnToMenú()
    {
        priorityQueue.PriorityEnqueue(finalScore);
        topScores = priorityQueue.GetTop5Scores();
        if (currentSongData != null)
        {
            currentSongData.top5Scores = topScores;
        }
        SceneManager.LoadScene("MusicSelector");

    }*/

}
