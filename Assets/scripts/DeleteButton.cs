﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
//using UnityEditor;

public class DeleteButton : MonoBehaviour
{

	//public string url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
	//public string url = "http://requestb.in/r4x6eyr4";

	//public string url = "http://localhost:3000/api/products/delete";
	public string url = "http://104.199.124.21:3000/api/products/delete";


	// Use this for initialization
	void Start ()
	{

		var button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(delegate { Delete(); });
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


	private void Delete()
	{
		Debug.Log ("button clicked");


		//Application.LoadLevel(1); 


		/*

		GameObject inputFieldGo = GameObject.Find("InputField");
		InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
		var username = inputFieldCo.text;
		Debug.Log("username: " + username);

		inputFieldGo = GameObject.Find("InputField (1)");
		inputFieldCo = inputFieldGo.GetComponent<InputField>();
		var password = inputFieldCo.text;
		Debug.Log("password: " + password);
		*/


		/*
		WWWForm form = new WWWForm();
		form.AddField("facebookID", "3");
		//Hashtable header = new Hashtable();
		//Dictionary header = new Dictionary();
		//Dictionary header = new Dictionary<string,string>();

		//header.Add("Content-Type", "application/x-www-form-urlencoded");

		//header = form.headers;

		//WWW www = new WWW (url);
		//WWW www = new WWW(url, form.data, header);
		//WWW www = new WWW(url, form.data);
		WWW www = new WWW(url, form);

		//StartCoroutine ("AddComponentMenu");
		StartCoroutine (AddComponentMenu(www));
		*/



		var data = transform.parent.GetComponent<DeleteButtonScript>();

		Debug.Log ("id is: " + data.id);




	
		StartCoroutine(Post(data.id));

	}

	IEnumerator Post (string id) 
	{





		Debug.Log("THREAD FIRED");

		WWWForm form = new WWWForm();

		//string altitude = Input.location.lastData.altitude.ToString();

		form.AddField ("operation", "deleteProduct");
		form.AddField("id", id);






		WWW www = new WWW(url, form);





		//WWW www = new WWW (url);

		//Debug.Log (www.text);

		yield return www;



		if(!string.IsNullOrEmpty(www.error)) {
			print( "Error downloading: " + www.error );
		} else {
			// show the highscores
			Debug.Log("response: " + www.text);

			if (www.text == "{status:false}")
			{



			}

		}

		Application.LoadLevel(3);


	}




}
