using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Canci�n", menuName = "Musica/Datos de M�sica")]
public class SongsSO : ScriptableObject
{
    public string title;
    public AudioClip audioClip;
}
