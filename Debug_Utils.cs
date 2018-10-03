using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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