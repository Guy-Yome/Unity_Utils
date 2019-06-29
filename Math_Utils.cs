using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Math_Utils {
	/**
	* Gets the angle in Deg and converts it in Rad and returns the normalized Vector2 associated with it
	*/
	public static Vector2 angle_direction (float angle) {
		return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
	}

	/**
	* Returns the angle in Rad of the line made from point a to point b.
	*
	* Source : https://stackoverflow.com/questions/9970281/java-calculating-the-angle-between-two-points-in-degrees
	*/
	public static float two_points_angle (Vector2 a, Vector2 b) {
		Vector2 target = b - a;
		float angle = Mathf.Atan(target.y / target.x) * Mathf.Rad2Deg;
		if (target.x < 0F) {
		    angle += 180F;
		}
		return angle * Mathf.Deg2Rad;
	}

	/**
	* Converts a square input (Ps3 controller for example) to a linear circle input
	*/
	public static Vector2 square_to_circle (Vector2 point) {
		Vector2 new_point = new Vector2(point.x, point.y);
		if (Vector2.Distance(Vector2.zero, new_point) > 0F) {
			float x_c_1 = new_point [0];
			float y_c_1 = new_point [1];

			float x_c_2 = Mathf.Pow(x_c_1, 2);
			float y_c_2 = Mathf.Pow(y_c_1, 2);

			float x_circle = x_c_1 * (Mathf.Sqrt(x_c_2 + y_c_2 - (x_c_2 * y_c_2)) / Mathf.Sqrt(x_c_2 + y_c_2));

			float y_circle = y_c_1 * (Mathf.Sqrt(x_c_2 + y_c_2 - (x_c_2 * y_c_2)) / Mathf.Sqrt(x_c_2 + y_c_2));

			return new Vector2(x_circle, y_circle);
		} else {
			return new Vector2(0F, 0F);
		}
	}

	/**
	* Clamps any vector2 input (Ps3 controller for example) to a vector2 of magnitude 1 max
	*/
	public static Vector2 clamp_to_unit_vector (Vector2 point) {
		return Vector2.ClampMagnitude(point, 1F);
	}
}