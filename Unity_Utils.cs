using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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