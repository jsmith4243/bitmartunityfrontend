using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
//using UnityEditor;

public class SubmitButtonEdit : MonoBehaviour {

	//public string url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
	//public string url = "http://requestb.in/r4x6eyr4";


	//public string url = "http://localhost:3000/api/products/edit";
	public string url = "http://104.199.124.21:3000/api/products/edit";

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



		GameObject inputFieldGo = GameObject.Find("InputField");
		InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
		inputFieldCo.text = GlobalInfo.Instance.productname;

		inputFieldGo = GameObject.Find("InputField (1)");
		inputFieldCo = inputFieldGo.GetComponent<InputField>();
		//price
		//inputFieldCo.text = GlobalInfo.Instance.

		inputFieldGo = GameObject.Find("InputField (2)");
		inputFieldCo = inputFieldGo.GetComponent<InputField>();
		//phone
		//inputFieldCo.text = GlobalInfo.Instance.sellerphone;

		inputFieldGo = GameObject.Find("InputField (3)");
		inputFieldCo = inputFieldGo.GetComponent<InputField>();
		inputFieldCo.text = GlobalInfo.Instance.email;

		inputFieldGo = GameObject.Find("Dropdown");
		Dropdown inputFieldCoDropdown = inputFieldGo.GetComponent<Dropdown>();
		if (GlobalInfo.Instance.condition == "new") {
			inputFieldCoDropdown.value = 0;
		}
		else if (GlobalInfo.Instance.condition == "good") {
			inputFieldCoDropdown.value = 1;
		}
		else if (GlobalInfo.Instance.condition == "fair") {
			inputFieldCoDropdown.value = 2;
		}


		inputFieldGo = GameObject.Find("Dropdown (1)");
		inputFieldCoDropdown = inputFieldGo.GetComponent<Dropdown>();
		if (GlobalInfo.Instance.takingCash == "true") {

			inputFieldCoDropdown.value = 0;
		}
		else if (GlobalInfo.Instance.takingCredit == "true")
		{

			inputFieldCoDropdown.value = 1;

		}

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel(1); }

	}

	private void SubmitForm()
	{
		Debug.Log ("button clicked");


		Debug.Log ("id isx: " + GlobalInfo.Instance.id); 
		var id = GlobalInfo.Instance.id;


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

		Debug.Log ("id iss: " + GlobalInfo.Instance.id);

		form.AddField("productidEdit", GlobalInfo.Instance.id);

		form.AddField("latitudeEdit", Input.location.lastData.latitude.ToString());
		form.AddField("longitudeEdit", Input.location.lastData.longitude.ToString());
		form.AddField("altitudeEdit", Input.location.lastData.altitude.ToString());

		form.AddField("userid", "3");

		if (GlobalInfo.Instance.takingCash == "true") {
			form.AddField ("cashEdit", "true");
			form.AddField ("creditEdit", "false");
		}
		else if (GlobalInfo.Instance.takingCredit == "true") {
			form.AddField ("cashEdit", "false");
			form.AddField ("creditEdit", "true");
		}




		form.AddField("conditionEdit", condition2);

		form.AddField("countryEdit", "31xx");
		form.AddField("usernameEdit", GlobalInfo.Instance.username);
		form.AddField("emailEdit", selleremail);
		form.AddField("sellerphoneEdit", sellerphone);
		form.AddField("othercomments", "3");
		form.AddField("productnameEdit", productname);
		form.AddField ("nameEdit", "3");
		form.AddField("productpriceEdit", productprice);



		WWW www = new WWW(url, form);





		//WWW www = new WWW (url);

		//Debug.Log (www.text);

		yield return www;



		if(!string.IsNullOrEmpty(www.error)) {
			print( "Error downloading: " + www.error );
		} else {
			// show the highscores
			//Debug.Log("response: " + www.text);
		}


	}
}
