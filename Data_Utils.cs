using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data_Utils {
	/**
    * This function shuffles a list of any type and changes its value directly.
    *
    * Source : https://answers.unity.com/questions/486626/how-can-i-shuffle-alist.html
    */
    public static void Shuffle<T> (this IList<T> list) {
        for (int i = 0; i < list.Count; i++) {
             T temp = list[i];
             int random_index = UnityEngine.Random.Range(i, list.Count);
             list[i] = list[random_index];
             list[random_index] = temp;
        }
    }
}