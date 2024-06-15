using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Nueva Canci�n", menuName = "Musica/Datos de M�sica")]
public class SongsSO : ScriptableObject
{
    public string musicTitle;
    public string musicArtist;
    public string dificult;
    public string musicDuration;
    public Image musicImage;
    public AudioClip audioClip;
}
