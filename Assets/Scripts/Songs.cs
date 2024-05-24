using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Songs : MonoBehaviour
{
    public string Title { get; private set; }

    public Songs(string title)
    {
        Title = title;
    }
}
