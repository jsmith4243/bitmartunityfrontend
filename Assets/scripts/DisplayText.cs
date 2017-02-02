using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;

public class DisplayText : MonoBehaviour {

	public RectTransform myPanel;
	public GameObject myTextPrefab;
	public GameObject variableForPrefab;

	public GameObject variableForPrefab2;

	//public Text variableForPrefab;
	public GameObject pref1;
	//public Text pref1;

	public GameObject pref2;

	public string string1 = "bicycle ";
	public string string2 = "200";
	public string string3 = "unicycle";
	public string string4 = "3000";


	public string url = "http://localhost:3000/api/products";
	//public string url = "http://104.199.124.21:3000/api/products";




	// Use this for initialization
	void Start () {











		Debug.Log("FLAG11");
		// UnityScript
		//variableForPrefab = Resources.Load("prefabs/prefab1", GameObject) as GameObject;




		variableForPrefab = (GameObject)Resources.Load("Text (1)", typeof(GameObject));

		variableForPrefab2 = (GameObject)Resources.Load ("CanvasEditButton", typeof(GameObject));


		//variableForPrefab = (Text)Resources.Load("Panel", typeof(Text));

		if (variableForPrefab == null) {

			Debug.Log ("It's null");

		}


		/*

		pref1 = (GameObject) Instantiate (variableForPrefab) as GameObject;
		//pref1 = (Text) Instantiate (variableForPrefab) as Text;
	

		pref1.transform.parent = transform;

		Text text1;
		text1 = pref1.GetComponent<Text> ();
		text1.text = "Score : ";
		*/

		StartCoroutine (GET (variableForPrefab));

	

		//GetComponent<Text>();

		//Text myText = pref1.AddComponent<Text> ();	
		//myText.text = "XXXX";

		//pref1.guiText = "df";

		/*
		if (myText == null) {

			Debug.Log ("myText is null");
		} else {
			Debug.Log ("myText not null");
		}
		*/


		//Text[] texts;
		//texts = pref1.GetComponentsInChildren<Text>();
		//texts[0].text = "test1x";




		Debug.Log ("FLAG2");

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel(1); }
	
	}




	IEnumerator GET (GameObject variableForPrefab) {



		Debug.Log("THREAD FIREDX");

		//WWWForm form = new WWWForm();


		WWW www = new WWW(url);





		//WWW www = new WWW (url);

		//Debug.Log (www.text);

		yield return www;

		Debug.Log ("Connecting to: " + url);

		if(!string.IsNullOrEmpty(www.error)) {
			print( "Error downloading: " + www.error );
		} else {
			// show the highscores
			Debug.Log("response: " + www.text);
		}


		var N = JSON.Parse(www.text);

		Debug.Log ("RES>>>" + N[1]["_id"].Value);
		Debug.Log ("Count: " + N.Count);



		/*
		JSONClass j = (JSONClass)N.AsObject ["N"];
		foreach (string k in j.keys){
			Debug.Log (k);
			Debug.Log (j[k]);
		}

		*/

		for (int i = 0; i < N.Count; i++) {

			pref1 = (GameObject) Instantiate (variableForPrefab) as GameObject;
			//pref1 = (Text) Instantiate (variableForPrefab) as Text;


			pref1.transform.parent = transform;

			Text text1;
			text1 = pref1.GetComponent<Text> ();
			text1.text = "PRODUCT SOLD: \n id: " + N [i] ["_id"] + "\n Product Name: " + N [i] ["productname"]  + "\n Product Price: " + N [i] ["productprice"] +  "\n Seller User Name: " + N [i] ["username"] + "\n Taking Cash:" + N [i] ["takingCash"] + "\n Taking Credit:" + N [i] ["takingCredit"] + "\n Condition: " + N [i] ["condition"] + "\n Seller Email: " + N [i] ["selleremail"] + "\n Seller Phone Number: " + N [i] ["phone"] +  "\n Latitude: " + N [i] ["latitude"] + "\n Longitude: " + N [i] ["longitude"] + "\n Altitude: " + N [i] ["altitude"];   

			if (GlobalInfo.Instance.username.Equals(N[i]["username"]))
			{
					

				pref2 = (GameObject)Instantiate (variableForPrefab2) as GameObject;



				var data = pref2.GetComponent<DeleteButtonScript>();

				Debug.Log ("XXXXXX668");

				Debug.Log("id is: " + N[i]["_id"].Value);

				//data.id = "44";

				data.id = N [i] ["_id"].Value;
				data.productname = N [i] ["productname"].Value;
				data.productprice = N [i] ["productprice"].Value;
				data.phone = N [i] ["phone"].Value;
				data.username = N [i] ["username"].Value;
				data.takingCash = N [i] ["takingCash"];
				data.takingCredit = N [i] ["takingCredit"];
				data.condition = N [i] ["condition"];
				data.country = N [i] ["country"];
				data.email = N [i] ["selleremail"];
				data.latitude = N [i] ["latitude"];
				data.longitude = N [i] ["longitude"];
				data.altitude = N [i] ["altitude"];

				data.sellername = N [i] ["sellername"].Value;
				




				pref2.transform.parent = transform;

			}





		}



	}




}
