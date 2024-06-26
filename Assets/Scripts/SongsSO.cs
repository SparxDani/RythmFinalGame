using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Nueva Canci�n", menuName = "Musica/Datos de M�sica")]
public class SongsSO : ScriptableObject
{
    public string musicTitle;
    public string musicArtist;
    public string musicGame;
    public string dificult;
    public string musicDuration;
    public int musicDurationInSeconds;
    public string midiNameFile;
    public Sprite musicImage;
    public AudioClip audioClip;
    public int[] top5Scores;

}
