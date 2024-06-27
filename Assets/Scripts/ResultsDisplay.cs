using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsDisplay : MonoBehaviour
{
    public TextMeshProUGUI songNameText;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI missedNotesText;
    public TextMeshProUGUI hitedNotesText;
    public TextMeshProUGUI okNotesText;
    public TextMeshProUGUI goodNotesText;
    public TextMeshProUGUI greatNotesText;
    public TextMeshProUGUI excellentNotesText;
    public TextMeshProUGUI maxComboText;
    public TextMeshProUGUI finalScoreText;

    public int finalScore;
    void Start()
    {
        songNameText.text = GameData.songName;
        usernameText.text = "Username: " + GameData.Username + " Results:";
        totalScoreText.text = "Score: " + GameData.TotalScore.ToString();
        missedNotesText.text = "Failed notes: " + GameData.MissedNotes.ToString();
        hitedNotesText.text = "Successful notes: " + GameData.HitedNotes.ToString();
        okNotesText.text = "OK: " + GameData.OkNotes.ToString();
        goodNotesText.text = "GOOD: " + GameData.GoodNotes.ToString();
        greatNotesText.text = "GREAT: " + GameData.GreatNotes.ToString();
        excellentNotesText.text = "EXCELLENT: " + GameData.ExcellentNotes.ToString();
        maxComboText.text = "Max Combo: " + GameData.ComboScore.ToString();

        finalScore = CalculateFinalScore();
        finalScoreText.text = "FINAL SCORE: " + finalScore.ToString();
    }

    int CalculateFinalScore()
    {
        return GameData.TotalScore + (GameData.OkNotes * 15) + (GameData.GoodNotes * 30) + (GameData.GreatNotes * 50) + (GameData.ExcellentNotes * 75);
    }
}