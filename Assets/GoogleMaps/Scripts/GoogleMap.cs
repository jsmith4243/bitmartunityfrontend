using UnityEngine;
using System.Collections;


using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using System.Net;

public class GoogleMap : MonoBehaviour
{

	//public string url2 = "http://localhost:3000/api/products";
	public string url2 = "http://104.199.124.21:3000/api/products";


	public enum MapType
	{
		RoadMap,
		Satellite,
		Terrain,
		Hybrid
	}
	public bool loadOnStart = true;
	public bool autoLocateCenter = true;
	public GoogleMapLocation centerLocation;
	public int zoom = 13;
	public MapType mapType;
	public int size = 512;
	public bool doubleResolution = false;
	public GoogleMapMarker[] markers;
	public GoogleMapPath[] paths;
	
	void Start() {


		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			//yield return new WaitForSeconds(1);
			//maxWait--;
		}


		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("////////////////GPS Timed out////////////////");
			//yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("////////////////Unable to determine device location////////////////");
			//yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			print("////////////////Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
		}

		//centerLocation.latitude = -122;
	
		//centerLocation.longitude = 37;


		// Stop service if there is no need to query location updates continuously
		//Input.location.Stop();





		Debug.Log("THREAD FIRED");



		if(loadOnStart) Refresh();	








	}


	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel(1); }

	}
	
	public void Refresh() {

		//if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel(0); }

		if(autoLocateCenter && (markers.Length == 0 && paths.Length == 0)) {
			Debug.LogError("Auto Center will only work if paths or markers are used.");	
		}
		StartCoroutine(_Refresh());
	}
	
	IEnumerator _Refresh ()
	{






		centerLocation = new GoogleMapLocation ();
		centerLocation.address = "";



		centerLocation.latitude = Input.location.lastData.latitude;
		centerLocation.longitude = Input.location.lastData.longitude;

		//centerLocation.latitude = 37.015244f;
		//centerLocation.longitude = -121.886125f;



		// Stop service if there is no need to query location updates continuously
		//Input.location.Stop();



		markers = new GoogleMapMarker[1];

		markers[0] = new GoogleMapMarker();

		markers [0].size = GoogleMapMarker.GoogleMapMarkerSize.Mid;
		markers[0].color = new GoogleMapColor();
		markers[0].color = GoogleMapColor.red;
		markers [0].label = "YourLocation";






		var text = new WebClient().DownloadString (url2);




		Debug.Log ("text is: \n" + text);



		var N = JSON.Parse(text);


		//Debug.Log ("RES>>>" + N[1]["_id"].Value);
		//Debug.Log ("Count: " + N.Count);


		Debug.Log (">>N.count (number of records parsed from json string) is: " + N.Count + "\n");


		markers [0].locations = new GoogleMapLocation[N.Count + 1];
		markers[0].locations[0] = new GoogleMapLocation();
		markers[0].locations[0].address = "";

		//markers[0].locations[0].latitude = 37.015244f;
		float lat = Input.location.lastData.latitude;
		markers [0].locations [0].latitude = lat;

		//markers [0].locations [0].longitude = -121.886125f;
		float longi = Input.location.lastData.longitude;
		markers [0].locations [0].longitude = longi;

		Debug.Log("Length of markers array is (should be 1): " + markers.Length );
		Debug.Log("Length of locations array is (should be 1 + number of products): " + markers[0].locations.Length );


		/*

		markers[1] = new GoogleMapMarker();
		markers [1].size = GoogleMapMarker.GoogleMapMarkerSize.Mid;
		markers[1].color = new GoogleMapColor();
		markers[1].color = GoogleMapColor.yellow;
		markers [1].label = "product";




		markers [1].locations = new GoogleMapLocation[1];
		markers[1].locations[0] = new GoogleMapLocation();
		markers[1].locations[0].address = "";

		markers[1].locations[0].latitude = 37.015244f;
		//float lat = Input.location.lastData.latitude;
		//markers [0].locations [0].latitude = lat;
		markers [1].locations [0].longitude = -121.886125f;
		//



		*/

		//String text = "";

		//StartCoroutine (GET ( text));
		//getJSON(ref text);

		//var text = new WebClient().DownloadString ("http://104.199.124.21:3000/api/products");
		//var text = new WebClient().DownloadString ("http://localhost:3000/api/products");

		//markers [1].locations = new GoogleMapLocation[N.Count];
		for (int i = 1; i < N.Count + 1; i++) {



			markers [0].locations [i] = new GoogleMapLocation ();
			markers [0].locations [i].address = "";

			//markers[0].locations[i].latitude = 37.015244f;

			Debug.Log ("i is: " + i + "\n");

			Debug.Log ("FLAGX");
				
			//Debug.Log ("N [i] [latitude] is: " + N [i] ["latitude"] + "\n");
			Debug.Log ("N [i] [latitude] is: " + 44 + "\n");

			//Debug.Log ("N [i] [longitude] is: " +  N [i] ["longitude"] + "\n");

			Debug.Log ("N [i] [latitude] is: " + 55 + "\n");

			//markers[0].locations[i].latitude = 37.015244f;
			string lat2String = N [i] ["latitude"];
			float lat2Float = Convert.ToSingle (lat2String);
			markers [0].locations [i].latitude = lat2Float;



			//markers [0].locations [i].longitude = -121.886125f;
			string longi2String = N [i] ["longitude"];
			float longi2Float = Convert.ToSingle (longi2String);
			markers [0].locations [i].longitude = longi2Float;

		}

		//Debug.Log ("LOCFLAG>>>>: " + markers [0].locations [3].latitude + "\n");



		//Debug.Log ("Lattest1: " + markers [1].locations [0].latitude);





		

			//N [i] ["altitude"];   





//*/


		var url = "http://maps.googleapis.com/maps/api/staticmap";
		var qs = "";
		if (!autoLocateCenter) {
			if (centerLocation.address != "")
				qs += "center=" + WWW.UnEscapeURL (centerLocation.address);
			else {
				qs += "center=" + WWW.UnEscapeURL (string.Format ("{0},{1}", centerLocation.latitude, centerLocation.longitude));
			}
		
			qs += "&zoom=" + zoom.ToString ();
		}
		qs += "&size=" + WWW.UnEscapeURL (string.Format ("{0}x{0}", size));
		qs += "&scale=" + (doubleResolution ? "2" : "1");
		qs += "&maptype=" + mapType.ToString ().ToLower ();
		var usingSensor = false;
#if UNITY_IPHONE
		usingSensor = Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running;
#endif
		qs += "&sensor=" + (usingSensor ? "true" : "false");



		
		foreach (var i in markers) {
			qs += "&markers=" + string.Format ("size:{0}|color:{1}|label:{2}", i.size.ToString ().ToLower (), i.color, i.label);
			foreach (var loc in i.locations) {
				if (loc.address != "") {
					qs += "|" + WWW.UnEscapeURL (loc.address);
					Debug.Log ("flag1");
				}
				else
					qs += "|" + WWW.UnEscapeURL (string.Format ("{0},{1}", loc.latitude, loc.longitude));
					Debug.Log("flag2");
				Debug.Log ("latitude is: " + loc.latitude);
				Debug.Log ("longitude is: " + loc.longitude);
				Debug.Log ("qs is: " + qs);
			}
		}
		
		foreach (var i in paths) {
			qs += "&path=" + string.Format ("weight:{0}|color:{1}", i.weight, i.color);
			if(i.fill) qs += "|fillcolor:" + i.fillColor;
			foreach (var loc in i.locations) {
				if (loc.address != "")
					qs += "|" + WWW.UnEscapeURL (loc.address);
				else
					qs += "|" + WWW.UnEscapeURL (string.Format ("{0},{1}", loc.latitude, loc.longitude));
			}
		}
		
		
		var req = new WWW (url + "?" + qs);
		Debug.Log (url + "?" + qs);
		yield return req;
		GetComponent<Renderer>().material.mainTexture = req.texture;


		yield return 3;
	}






	IEnumerator GET ( String json)
	{



		Debug.Log("THREAD FIREDX");

		//WWWForm form = new WWWForm();


		WWW www = new WWW(url2);





		//WWW www = new WWW (url);

		//Debug.Log (www.text);

		yield return www;

		Debug.Log ("Connecting to: " + url2);

		if(!string.IsNullOrEmpty(www.error)) {
			print( "Error downloading: " + www.error );
		} else {
			// show the highscores
			Debug.Log("response: " + www.text);
		}

		json = www.text;


		/*
		var N = JSON.Parse(www.text);

		Debug.Log ("RES>>>" + N[1]["_id"].Value);
		Debug.Log ("Count: " + N.Count);
		*/


		/*
		JSONClass j = (JSONClass)N.AsObject ["N"];
		foreach (string k in j.keys){
			Debug.Log (k);
			Debug.Log (j[k]);
		}

		*/


		
		/*
		markers [1].locations = new GoogleMapLocation[N.Count];
		for (int i = 0; i < N.Count; i++) {



			markers [1].locations [i] = new GoogleMapLocation ();
			markers [1].locations [i].address = "";

			//markers[0].locations[0].latitude = 37.015244f;
			string lat2String = N [i] ["latitude"];
			float lat2Float = Convert.ToSingle (lat2String);
			markers [1].locations [i].latitude = lat2Float;
			//markers [0].locations [0].longitude = -121.886125f;

			string longi2String = N [i] ["longitude"];
			float longi2Float = Convert.ToSingle (longi2String);
			markers [1].locations [i].longitude = longi2Float;



		}
		*/




	}

	String getJSON(ref String json)
	{

		Debug.Log("THREAD FIREDX");

		//WWWForm form = new WWWForm();


		WWW www = new WWW(url2);





		//WWW www = new WWW (url);

		//Debug.Log (www.text);

		//yield return www;

		Debug.Log ("Connecting to: " + url2);

		if(!string.IsNullOrEmpty(www.error)) {
			print( "Error downloading: " + www.error );
		} else {
			// show the highscores
			Debug.Log("response: " + www.text);
		}

		json = www.text;

		return json;


	}


	
}

public enum GoogleMapColor
{
	black,
	brown,
	green,
	purple,
	yellow,
	blue,
	gray,
	orange,
	red,
	white
}

[System.Serializable]
public class GoogleMapLocation
{
	public string address;
	public float latitude;
	public float longitude;
}

[System.Serializable]
public class GoogleMapMarker
{
	public enum GoogleMapMarkerSize
	{
		Tiny,
		Small,
		Mid
	}
	public GoogleMapMarkerSize size;
	public GoogleMapColor color;
	public string label;
	public GoogleMapLocation[] locations;
	
}

[System.Serializable]
public class GoogleMapPath
{
	public int weight = 5;
	public GoogleMapColor color;
	public bool fill = false;
	public GoogleMapColor fillColor;
	public GoogleMapLocation[] locations;	
}