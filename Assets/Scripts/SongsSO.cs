using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Canción", menuName = "Musica/Datos de Música")]
public class SongsSO : ScriptableObject
{
    public string title;
    public AudioClip audioClip;
}
