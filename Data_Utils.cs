using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data_Utils {
	/**
    * This function shuffles a list of any type and changes its value directly.
    * You can use it by simply doing your_list.shuffle();
    * Source : https://answers.unity.com/questions/486626/how-can-i-shuffle-alist.html
    */
    public static void shuffle<T> (this IList<T> list) {
        for (int i = 0; i < list.Count; i++) {
             T temp = list[i];
             int random_index = UnityEngine.Random.Range(i, list.Count);
             list[i] = list[random_index];
             list[random_index] = temp;
        }
    }

    /**
    * This function shuffles a list of any type and returns a new list without changing the original.
    * You can use it by simply doing Data_Utils.shuffle_list(your_original_list);
    */
    public static List<T> shuffle_list<T> (List<T> list) {
        List<T> result = Data_Utils.copy_list(list);
        for (int i = 0; i < list.Count; i++) {
             T temp = result[i];
             int random_index = UnityEngine.Random.Range(i, result.Count);
             result[i] = result[random_index];
             result[random_index] = temp;
        }
        return result;
    }

    /**
    * This funciton copies a list of any type and returns it in a new variable
    */
    public static List<T> copy_list<T> (List<T> list) {
        List<T> result = new List<T>();
        for (int i = 0; i < list.Count; i++) {
             result.Add(list[i]);
        }
        return result;
    }
}