using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helpers : MonoBehaviour
{
    public static string getPrefabName(string name)
    {
        int howManyChars = name.IndexOf("#") + 1;
        return name.Substring(0, howManyChars);
    }
}
