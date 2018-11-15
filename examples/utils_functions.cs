using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utils_functions : MonoBehaviour {
	void Start () {

		// ----------------- Demonstrates the use of JSON_decode_file -----------------

		// Extract most structures of JSON without needing to make an Object class structure (somewhat dynamic)
		JSONNode_Result data_result = IO_Utils.JSON_decode_file("Assets/Unity_Utils/examples/example_json.txt");
		Debug.Log(data_result.correct); // Shows true if the file exists and is right
		Debug.Log(data_result.error); // Shows an empty string if no error

		Debug.Log(data_result.value["Random_Array"][0]); // Displays an int (12)
		Debug.Log(data_result.value["Random_Array"][1]); // Displays a string ("Apple")
		Debug.Log(data_result.value["Random_Array"][2]); // Displays a float (0.25F)
		Debug.Log(data_result.value["Random_Array"][3][2]); // Displays a sub array element (2)

		// ----------------- List shuffling -----------------

		List<int> int_list = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
		List<string> string_list = new List<string>() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};

		// Directly shuffle the list and change its value
		int_list.shuffle();

		// Shuffle a list and get its value as a return without affecting the original list
		List<string> new_string_list = Data_Utils.shuffle_list(string_list);

		// ----------------- Demonstrates the use of SMatrix -----------------

		// How to create a matrix with a format but no direct values (All 0 for now)
		SMatrix a_matrix = new SMatrix(5, 5);

		// How to directly create a matrix with values
		a_matrix = new SMatrix(new float[5,5] {
			{2, 1, 5, -1, 6},
			{1, 4, 0, 3, 7},
			{1, 0, 0, -7, 9},
			{3, 2, 1, 0, 8},
			{1, 1, 0, 3, 4}
		});

		SMatrix b_matrix = new SMatrix(new float[5,5] {
			{5, 3, 4, 2, -1},
			{2, 2, 3, 5, 5},
			{8, 10, 2, 36, 2},
			{7, 1, 1, 1, 3},
			{0, 3, 4, 8, 6}
		});

		// Set a value of the SMatrix
		b_matrix.set(2, 2, 5F);

		// Shows simple operations for Matrices
		SMatrix add_matrix = a_matrix + b_matrix;
		SMatrix sub_matrix = a_matrix - b_matrix;
		SMatrix mult_matrix = a_matrix * b_matrix;

		// Multiply or divide the matrix with scalars. Note that for the division, the scalar comes after.
		SMatrix scalar_after_matrix = a_matrix * 5F;
		SMatrix scalar_before_matrix = 5F * a_matrix;

		SMatrix scalar_divide_matrix = a_matrix / 5F;

		// Join the matrices into one
		SMatrix vert_matrix = SMatrix.join_vertical(a_matrix, b_matrix);
		SMatrix hor_matrix = SMatrix.join_horizontal(a_matrix, b_matrix);

		// Directly transpose the matrix
		a_matrix.transpose();
		// Transpose a matrix without changing its values, but instead get it as a new matrix
		SMatrix transposed_a_matrix = SMatrix.transpose(a_matrix);

		// Directly normalize the matrix
		a_matrix.normalize();
		// Normalize a matrix without changing its values, but instead get it as a new matrix
		SMatrix normalized_a_matrix = SMatrix.normalize(a_matrix);

		// Get the determinant of the matrix if any
		float determinant = a_matrix.det();

		// Log the matrix in the console
		a_matrix.log();

		// Get the string representing the matrix and log it in the console
		string read = a_matrix.read();
		Debug.Log(read);
	}
}

