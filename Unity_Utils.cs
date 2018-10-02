using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using SimpleJSON;

// --------------- Data objects for result validation ---------------

/**
* The classes in this category are built to make sure that if you would
* maybe have a function that could return no result or a result, you get
* all the data in the same object.
*
* To use them, instead of returning a nullable type or a simple variable type as float, you can
* set these matching types instead. By default they are set to be "correct". So, you initialize them with their constructor
* with no parameters and if there is no result or an error, you can set "correct" to false and set an error message.
* If there is a result, you simply put it inside the "value" attribute.
* This makes it so you will still receive a coherent answer from your function.
*
* The word correct is used instead of valid to make it easier to distinguish between the words "valid" and "value".
*/
public class Vector4_Result {
	public bool correct = true;
	public Vector4 value = Vector4.zero;
	public string error = "";

	public Vector4_Result () {
	}
}

public class Vector3_Result {
	public bool correct = true;
	public Vector3 value = Vector3.zero;
	public string error = "";

	public Vector3_Result () {
	}
}

public class Vector2_Result {
	public bool correct = true;
	public Vector2 value = Vector2.zero;
	public string error = "";

	public Vector2_Result () {
	}
}

public class Quaternion_Result {
	public bool correct = true;
	public Quaternion value = Quaternion.identity;
	public string error = "";

	public Quaternion_Result () {
	}
}

public class Float_Result {
	public bool correct = true;
	public float value = 0F;
	public string error = "";

	public Float_Result () {
	}
}

public class Int_Result {
	public bool correct = true;
	public int value = 0;
	public string error = "";

	public Int_Result () {
	}
}

public class String_Result {
	public bool correct = true;
	public string value = "";
	public string error = "";

	public String_Result () {
	}
}

public class JSONNode_Result {
	public bool correct = true;
	public JSONNode value;
	public string error = "";

	public JSONNode_Result () {
	}
}

// --------------- Utils Static Classes ---------------

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
		if (target.x <= 0F) {
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
}

public static class IO_Utils {
	/**
	* Returns a String_Result for the WHOLE file submitted.
	* The "correct" attribute of the data returned maybe be false if the file exists or not.
	*
	* The path is absolute. We need to tell the full path from the folder one level above Assets.
	*/
	public static String_Result read_full_file (string path) {
		String_Result result = new String_Result();
        try {
    		StreamReader reader = new StreamReader(path, Encoding.Default);

    		using (reader) {
    			result.value = reader.ReadToEnd();
    			reader.Close();
    		}
    	} catch (Exception e) {
			result.correct = false;
			result.error = e.ToString();
		}
		return result;
    }

    /**
    * Returns a more "dynamic" decoding of a JSON file.
    * The return data may have an error depending on the file's existence and the file's JSON data structure.
    *
    * SimpleJSON on asset store (GitHub) is required to use this functionnality.
    *
    * The path is absolute. We need to tell the full path from the folder one level above Assets.
    */
    public static JSONNode_Result JSON_decode_file (string path) {
    	String_Result json_file_content = IO_Utils.read_full_file(path);
    	JSONNode_Result json_data = new JSONNode_Result();

    	if (json_file_content.correct) {
    		try {
    			json_data.value = JSON.Parse(json_file_content.value);
    		} catch (Exception e) {
    			json_data.correct = false;
    			json_data.error = e.ToString();
    		}
    	} else {
    		json_data.correct = false;
    	}

    	if (json_data.correct && json_data.value == "") {
    		json_data.correct = false;
    		json_data.error = "JSON file has either no data or has a structural problem.";
    	}

		return json_data;
    }
}

public static class Debug_Utils {
	/**
	* Draws a circle in 3D space with a determined radius and color.
	*
	* The quality of the circle definition is set to 15 sides since it is a debugging
	* function and doesn't really more precision.
	*/
	public static void draw_circle (Vector3 position, float radius, Color color) {
		float segments = 15F;
		for (float s = 0; s < segments; s++) {
			float angle_a = (360F * (s / segments)) * Mathf.Deg2Rad;
			float angle_b = (360F * ((s + 1) / segments)) * Mathf.Deg2Rad;

			Vector3 point_a = new Vector3(Mathf.Cos(angle_a) * radius, Mathf.Sin(angle_a) * radius, 0F);
			Vector3 point_b = new Vector3(Mathf.Cos(angle_b) * radius, Mathf.Sin(angle_b) * radius, 0F);

			Debug.DrawLine(position + point_a, position + point_b, color);
		}
	}

	/**
	* Draws a circle in 3D space from a 2D coordinate with a determined radius and color.
	*
	* The quality of the circle definition is set to 15 sides since it is a debugging
	* function and doesn't really more precision.
	*/
	public static void draw_circle (Vector2 position, float radius, Color color) {
		Vector3 new_pos = new Vector3(position.x, position.y, 0F);
		Debug_Utils.draw_circle(new_pos, radius, color);
	}

	/**
	* Draws a rectangle in 3D space with a determined width, height and color.
	* The position received is set at the center of the rectangle.
	*/
	public static void draw_rectangle (Vector3 position, float width, float height, Color color) {
		float top = position.y + (height / 2F);
		float bottom = position.y - (height / 2F);
		float left = position.x - (width / 2F);
		float right = position.x + (width / 2F);

		Vector3 tl = new Vector3(left, top, 0F);
		Vector3 tr = new Vector3(right, top, 0F);
		Vector3 br = new Vector3(right, bottom, 0F);
		Vector3 bl = new Vector3(left, bottom, 0F);

		Debug.DrawLine(tl, tr, color);
		Debug.DrawLine(tr, br, color);
		Debug.DrawLine(br, bl, color);
		Debug.DrawLine(bl, tl, color);
	}

	/**
	* Draws a rectangle in 3D space from a 2D coordinate with a determined width, height and color.
	* The position received is set at the center of the rectangle.
	*/
	public static void draw_rectangle (Vector2 position, float width, float height, Color color) {
		Vector3 new_pos = new Vector3(position.x, position.y);
		Debug_Utils.draw_rectangle(new_pos, width, height, color);
	}
}