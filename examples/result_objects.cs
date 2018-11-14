using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class result_objects : MonoBehaviour {
	void Start () {

		// -------------------- TResult<T> class example --------------------

		// Stock a Vector3 in the result value
		TResult<Vector3> generic_vector3_result = new TResult<Vector3>();
		generic_vector3_result.value = new Vector3(1F, 2F, 3F);
		Debug.Log(generic_vector3_result.value); // Will display (1.0, 2.0, 3.0)

		// Stock a more complex variable (Dictionary<int, string>) in the result value
		Dictionary<int, string> a_dictionary = new Dictionary<int, string>();
		a_dictionary.Add(25, "A string");
		a_dictionary.Add(658, "B string");

		TResult<Dictionary<int, string>> generic_dictionary_result = new TResult<Dictionary<int, string>>();
		generic_dictionary_result.value = a_dictionary;
		Debug.Log(generic_dictionary_result.value[25]); // Will display "A string"
		Debug.Log(generic_dictionary_result.value[658]); // Will display "B string"

		// Stock a string in the result value
		TResult<string> generic_string_result = new TResult<string>();
		generic_string_result.value = "Another string";
		Debug.Log(generic_string_result.value); // Will display "Another string"

		// -------------------- Vector3_Result (Appliable to more specific similar classes) class example --------------------

		// This is an example of the Result objects.
		// What is applied here is also applicable for the other types made the same way (not only for Vector3).
		Vector3_Result position_of_target_correct = get_vector3_result(false);
		Vector3_Result position_of_target_error   = get_vector3_result(true);

		// Will pass through this if and execute the code
		if (position_of_target_correct.correct) {
			// Do something with position_of_target_correct.value  ...
		}

		if (position_of_target_error.correct) {
			// Will not reach this part in this scenario
		} else {
			// Execute some alternate scenario or display the error with position_of_target_error.error
		}
	}

	Vector3_Result get_vector3_result (bool has_error) {
		Vector3_Result result = new Vector3_Result();
		if (has_error) {
			result.correct = false;
			result.error = "Example: No object is at this position.";
		} else {
			result.value = new Vector3(10F, 15F, 2.5F);
		}
		return result;
	}
}

