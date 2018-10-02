using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class result_objects : MonoBehaviour {
	void Start () {
		// This is an example of the Result objects.
		// What is applied here is also applicable for the other types mde the same way (not only for Vector3).
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

