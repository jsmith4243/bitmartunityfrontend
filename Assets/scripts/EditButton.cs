using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EditButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

		var button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(delegate { Delete(); });
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private void Delete()
	{
		Debug.Log ("button clicked");

		var data = transform.parent.GetComponent<DeleteButtonScript>();

		Debug.Log ("id is xxxc: " + data.id);






		GlobalInfo.Instance.id = data.id;
		GlobalInfo.Instance.productname = data.productname;
		GlobalInfo.Instance.sellername = data.sellername;
		GlobalInfo.Instance.takingCash = data.takingCash;
		GlobalInfo.Instance.takingCredit = data.takingCredit;
		GlobalInfo.Instance.condition = data.condition;
		GlobalInfo.Instance.email = data.email;
		GlobalInfo.Instance.latitude = data.latitude;
		GlobalInfo.Instance.longitude = data.longitude;
		GlobalInfo.Instance.altitude = data.altitude;

		Application.LoadLevel(5);



	}




}
