using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTag : MonoBehaviour
{
    public enum Tags
    {
        None = 0, Wall, Enemy, Player
    }

    public Tags tag;
}
