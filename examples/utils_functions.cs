using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utils_functions : MonoBehaviour {
	void Start () {

		// Extract most structures of JSON without needing to make an Object class structure (somewhat dynamic)
		JSONNode_Result data_result = IO_Utils.JSON_decode_file("Assets/Unity_Utils/examples/example_json.txt");
		Debug.Log(data_result.correct); // Shows true if the file exists and is right
		Debug.Log(data_result.error); // Shows an empty string if no error

		Debug.Log(data_result.value["Random_Array"][0]); // Displays an int (12)
		Debug.Log(data_result.value["Random_Array"][1]); // Displays a string ("Apple")
		Debug.Log(data_result.value["Random_Array"][2]); // Displays a float (0.25F)
		Debug.Log(data_result.value["Random_Array"][3][2]); // Displays a sub array element (2)
	}
}

