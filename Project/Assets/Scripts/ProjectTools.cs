using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que contiene funciones estaticas utiles
/// </summary>
public static class ProjectTools
{
    /// <summary>
    /// Randomiza el orden
    /// </summary>
    /// <param name="list"></param>
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
