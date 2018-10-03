using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using SimpleJSON;

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