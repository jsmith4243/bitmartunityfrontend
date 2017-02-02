using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
//using UnityEditor;

public class SubmitButton : MonoBehaviour {

	//public string url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
	//public string url = "http://requestb.in/r4x6eyr4";

	//public string url = "http://localhost:3000/api/products";
	public string url = "http://104.199.124.21:3000/api/products";

	// Use this for initialization
	void Start () {

		var button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(delegate { SubmitForm(); });

		if (!Input.location.isEnabledByUser) {

			Debug.Log ("GPS NOT ENABLED");

			//EditorUtility.DisplayDialog ("title", "GPS NOT ENABLED", "okk");

		} else {

			Debug.Log ("GPS IS ENABLED");

			//EditorUtility.DisplayDialog ("title", "GPS  ENABLED", "okk");
		}

		Input.location.Start();

		Input.compass.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel(1); }

	}

	private void SubmitForm()
	{
		Debug.Log ("button clicked");

		GameObject inputFieldGo = GameObject.Find("InputField");
		InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
		var productname = inputFieldCo.text;
		Debug.Log("productname: " + productname);

		inputFieldGo = GameObject.Find("InputField (1)");
		inputFieldCo = inputFieldGo.GetComponent<InputField>();
		var productprice = inputFieldCo.text;
		Debug.Log("productprice: " + productprice);

		inputFieldGo = GameObject.Find("InputField (2)");
		inputFieldCo = inputFieldGo.GetComponent<InputField>();
		var sellerphone = inputFieldCo.text;
		Debug.Log("sellerphone: " + sellerphone);

		inputFieldGo = GameObject.Find("InputField (3)");
		inputFieldCo = inputFieldGo.GetComponent<InputField>();
		var selleremail = inputFieldCo.text;
		Debug.Log("selleremail: " + selleremail);

		inputFieldGo = GameObject.Find("Dropdown");
		Dropdown inputFieldCoDropdown = inputFieldGo.GetComponent<Dropdown>();
		var condition = inputFieldCoDropdown.value;
		Debug.Log("condition: " + condition);

		inputFieldGo = GameObject.Find("Dropdown (1)");
		inputFieldCoDropdown = inputFieldGo.GetComponent<Dropdown>();
		var paymenttype = inputFieldCoDropdown.value;
		Debug.Log("paymenttype: " + paymenttype);



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





		StartCoroutine(Post(productname, productprice, sellerphone, selleremail, condition, paymenttype));

	}

	IEnumerator Post (string productname, string productprice, string sellerphone, string selleremail, int condition, int paymenttype) {

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}


		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("////////////////GPS Timed out////////////////");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("////////////////Unable to determine device location////////////////");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			print("////////////////Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
		}

		// Stop service if there is no need to query location updates continuously
		//Input.location.Stop();


		string condition2 = "null";
		string paymenttype2;

		if (condition == 0) {

			condition2 = "new";

		} else if (condition == 1) {
			condition2 = "good";

		} else if (condition == 2) {

			condition2 = "fair";
		}


		if (paymenttype == 0) {

			paymenttype2 = "cash";

		} else if (paymenttype == 1) {
			paymenttype2 = "credit";

		} else if (paymenttype == 2) {

			paymenttype2 = "other";
		}





		Debug.Log("THREAD FIRED");

		WWWForm form = new WWWForm();

		//string altitude = Input.location.lastData.altitude.ToString();

		form.AddField("latitude", Input.location.lastData.latitude.ToString());
		form.AddField("longitude", Input.location.lastData.longitude.ToString());
		form.AddField("altitude", Input.location.lastData.altitude.ToString());

		form.AddField("userid", "3");
		form.AddField ("username", GlobalInfo.Instance.username);
		form.AddField ("sellername", GlobalInfo.Instance.sellername);

		if (paymenttype == 0) {
			form.AddField ("cash", "true");
			form.AddField ("credit", "false");
		}
		else if (paymenttype == 1) {
			form.AddField ("cash", "false");
			form.AddField ("credit", "true");
		}

		
		form.AddField("condition", condition2);

		form.AddField("country", "3");
		form.AddField("name", "3");
		form.AddField("selleremail", selleremail);
		form.AddField("sellerphone", sellerphone);
		form.AddField("othercomments", "3");
		form.AddField("productname", productname);
		form.AddField("productprice", productprice);



		WWW www = new WWW(url, form);





		//WWW www = new WWW (url);

		//Debug.Log (www.text);

		yield return www;



		if(!string.IsNullOrEmpty(www.error)) {
			print( "Error downloading: " + www.error );
		} else {
			// show the highscores
			Debug.Log("response: " + www.text);
		}


	}
}
