using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

/**
*	SMatrix stands for SimpleMatrix.
*	This class allows the simple use of matrices and their operations.
*
*	Examples are in examples/utils_functions.cs in the "Demonstrates the use of SMatrix"
*/
public class SMatrix {
	float[,] data;
	int rows = 1;
	int cols = 1;

	/**
	* Initialize the matrix with data in it already
	*/
	public SMatrix (float[,] _data) {
		data = _data;
		rows = data.GetLength(0);
		cols = data.GetLength(1);
	}

	/**
	* Initialize an empty matrix of specified format
	*/
	public SMatrix (int _rows, int _cols) {
		rows = _rows;
		cols = _cols;
		data = new float[rows, cols];
	}

	/**
	* Sets the value of a float to an element of the matrix at specified row and col
	*/
	public void set (int row, int col, float value) {
		data[row, col] = value;
	}

	/**
	* Transposes the matrix and changes its values directly
	*/
	public void transpose () {
		// Reversed rows and cols because of transposition
		float[,] result = new float[cols, rows];
		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < cols; x++) {
				result[x, y] = data[y, x];
			}
		}
		int temp_rows = rows;
		rows = cols;
		cols = temp_rows;
		data = result;
	}

	/**
	* Normalizes the matrix values and changes its values directly
	*/
	public void normalize () {
		float[,] result = new float[rows, cols];
		if (rows == cols && rows > 1 && cols > 1) {
			result = (this / this.det()).data;
		} else {
			if (cols == 1) {
				float square_sum = 0;
				for (int y = 0; y < rows; y++) {
					square_sum += Mathf.Pow(data[y, 0], 2);
				}
				square_sum = Mathf.Sqrt(square_sum);
				result = (this / square_sum).data;
			} else {
				for (int x = 0; x < cols; x++) {
					float square_sum = 0;
					for (int y = 0; y < rows; y++) {
						square_sum += Mathf.Pow(data[y, x], 2);
					}
					square_sum = Mathf.Sqrt(square_sum);

					for (int y = 0; y < rows; y++) {
						result[y, x] = (data[y, x] / square_sum);
					}
				}
			}
		}
		data = result;
	}

	/**
	* Finds and returns the determinant of this matrix if any. Else it returns an exception
	*/
	public float det () {
		float det = 0;
		if (rows == cols) {
			return sub_det(this);
		} else {
			throw new InvalidOperationException("Cannot get the determinant of a m * n SMatrix.");
		}
	}

	/**
	* Used by the det function to recursively get the sub determinants of the matrix
	*/
	private float sub_det (SMatrix sub) {
		float det = 0;
		if (sub.rows == sub.cols) {
			int size = sub.rows;
			if (size > 2) {
				// We need to find the det again
				int mult = 1;
				for (int i = 0; i < size; i++) {
					float dominant_value = sub.data[0, i];
					SMatrix low_sub = new SMatrix(new float[(size - 1), (size - 1)]);

					// Construct dependant sub low level SMatrix
					int added_amount = 0;
					for (int y = 0; y < size; y++) {
						for (int x = 0; x < size; x++) {
							if (y != 0 && x != i) {
								// We can construct with these
								int y_index = (int) Mathf.Floor((float) added_amount / (float) (size - 1));
								int x_index = added_amount % (size - 1);
								low_sub.data[y_index, x_index] = sub.data[y, x];
								added_amount++;
							}
						}
					}

					det += mult * dominant_value * sub_det(low_sub);
					mult *= -1;
				}
				return det;
			} else {
				// We are in a 2x2
				return (sub.data[0,0] * sub.data[1,1]) - (sub.data[0,1] * sub.data[1,0]);
			}
		} else {
			throw new InvalidOperationException("Cannot get the determinant of a m * n SMatrix.");
		}
	}

	/**
	* Displays a message in the console of the content of the matrix.
	*/
	public void log () {
		string result = "";
		for (int row = 0; row < rows; row++) {
			string row_string = "";
			for (int col = 0; col < cols; col++) {
				row_string += " | " + data[row, col] + " | ";
			}
			result += row_string + "\r\n";
		}
		Debug.Log(result);
	}

	/**
	*	Returns a string representing the content of the matrix
	*/
	public string read () {
		string result = "";
		for (int row = 0; row < rows; row++) {
			string row_string = "";
			for (int col = 0; col < cols; col++) {
				row_string += " | " + data[row, col] + " | ";
			}
			result += row_string + "\n\r";
		}
		return result;
	}

	/**
	* Joins two matrices of the vertical axis. Throws an exception if the formats are not compatible.
	*/
	public static SMatrix join_vertical (SMatrix a, SMatrix b) {
		if (a.cols == b.cols) {
			float[,] result = new float[a.rows + b.rows, a.cols];

			for (int r = 0; r < a.rows; r++) {
				for (int c = 0; c < a.cols; c++) {
					result[r, c] = a.data[r, c];
				}
			}

			for (int r = 0; r < b.rows; r++) {
				for (int c = 0; c < b.cols; c++) {
					result[r + a.rows, c] = b.data[r, c];
				}
			}

			return new SMatrix(result);
		} else {
			throw new InvalidOperationException("Cannot join SMatrices vertically of different column amounts.");
		}
	}

	/**
	* Joins two matrices of the horizontal axis. Throws an exception if the formats are not compatible.
	*/
	public static SMatrix join_horizontal (SMatrix a, SMatrix b) {
		if (a.rows == b.rows) {
			float[,] result = new float[a.rows, a.cols + b.cols];

			for (int r = 0; r < a.rows; r++) {
				for (int c = 0; c < a.cols; c++) {
					result[r, c] = a.data[r, c];
				}
			}

			for (int r = 0; r < b.rows; r++) {
				for (int c = 0; c < b.cols; c++) {
					result[r, c + a.cols] = b.data[r, c];
				}
			}

			return new SMatrix(result);
		} else {
			throw new InvalidOperationException("Cannot join SMatrices horizontally of different row amounts.");
		}
	}

	/**
	* Static version of the transpose function. This time, it returns a full NEW SMatrix as a result.
	*/
	public static SMatrix transpose (SMatrix a) {
		// Do not call the object's version because it changes the objects value
		// Reversed rows and cols because of transposition
		float[,] result = new float[a.cols, a.rows];
		for (int y = 0; y < a.rows; y++) {
			for (int x = 0; x < a.cols; x++) {
				result[x, y] = a.data[y, x];
			}
		}
		return new SMatrix(result);
	}

	/**
	* Static version of the normalize function. This time, it returns a full NEW SMatrix as a result.
	*/
	public static SMatrix normalize (SMatrix a) {
		// Do not call the object's version because it changes the objects value
		float[,] result = new float[a.rows, a.cols];
		if (a.rows == a.cols && a.rows > 1 && a.cols > 1) {
			result = (a / a.det()).data;
		} else {
			if (a.cols == 1) {
				float square_sum = 0;
				for (int y = 0; y < a.rows; y++) {
					square_sum += Mathf.Pow(a.data[y, 0], 2);
				}
				square_sum = Mathf.Sqrt(square_sum);
				result = (a / square_sum).data;
			} else {
				for (int x = 0; x < a.cols; x++) {
					float square_sum = 0;
					for (int y = 0; y < a.rows; y++) {
						square_sum += Mathf.Pow(a.data[y, x], 2);
					}
					square_sum = Mathf.Sqrt(square_sum);

					for (int y = 0; y < a.rows; y++) {
						result[y, x] = (a.data[y, x] / square_sum);
					}
				}
			}
		}
		return new SMatrix(result);
	}

	/**
	* Allows SMatrices "multiplication" as an operation
	* Throws an exception if formats do not match
	*/
	public static SMatrix operator * (SMatrix a, SMatrix b) {
		if (a.cols == b.rows) {
			// Pass through every row and every column
			SMatrix result = new SMatrix(a.rows, b.cols);

			for (int row = 0; row < a.rows; row++) {
				for (int col = 0; col < b.cols; col++) {
					// For each element in row a multiply with col b
					float total = 0;
					for (int row_b = 0; row_b < b.rows; row_b++) {
						total += a.data[row, row_b] * b.data[row_b, col];
					}
					result.set(row, col, total);
				}
			}

			return result;
		} else {
			throw new InvalidOperationException("Cannot perform multiplication on SMatrices of different sizes.");
		}
	}

	/**
	* Allows SMatrix "multiplication" with a salar as an operation
	*/
	public static SMatrix operator * (SMatrix a, float b) {
		SMatrix result = new SMatrix(a.rows, a.cols);

		for (int row = 0; row < a.rows; row++) {
			for (int col = 0; col < a.cols; col++) {
				result.set(row, col, (a.data[row, col] * b));
			}
		}

		return result;
	}

	/**
	* Allows SMatrix "multiplication" with a salar as an operation, but the scalar is first
	*/
	public static SMatrix operator * (float b, SMatrix a) {
		SMatrix result = new SMatrix(a.rows, a.cols);

		for (int row = 0; row < a.rows; row++) {
			for (int col = 0; col < a.cols; col++) {
				result.set(row, col, (a.data[row, col] * b));
			}
		}

		return result;
	}

	/**
	* Allows SMatrix "division" by a salar as an operation
	*/
	public static SMatrix operator / (SMatrix a, float b) {
		return (a * (1 / b));
	}

	/**
	* Allows SMatrices "addition" as an operation
	* Throws an exception if the formats do not match
	*/
	public static SMatrix operator + (SMatrix a, SMatrix b) {
		if (a.cols == b.cols && a.rows == b.rows) {
			// Pass through every row and every column
			SMatrix result = new SMatrix(a.rows, b.cols);

			for (int row = 0; row < a.rows; row++) {
				for (int col = 0; col < a.cols; col++) {
					result.set(row, col, (a.data[row, col] + b.data[row, col]));
				}
			}

			return result;
		} else {
			throw new InvalidOperationException("Cannot perform addition on SMatrices of different sizes.");
		}
	}

	/**
	* Allows SMatrices "subtraction" as an operation
	* Throws an exception if the formats do not match
	*/
	public static SMatrix operator - (SMatrix a, SMatrix b) {
		if (a.cols == b.cols && a.rows == b.rows) {
			// Pass through every row and every column
			SMatrix result = new SMatrix(a.rows, b.cols);

			for (int row = 0; row < a.rows; row++) {
				for (int col = 0; col < a.cols; col++) {
					result.set(row, col, (a.data[row, col] - b.data[row, col]));
				}
			}

			return result;
		} else {
			throw new InvalidOperationException("Cannot perform addition on SMatrices of different sizes.");
		}
	}
}
