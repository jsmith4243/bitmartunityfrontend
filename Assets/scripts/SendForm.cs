using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SendForm : MonoBehaviour {

	// Use this for initialization
	void Start () {

		var input = gameObject.GetComponent<InputField> ();
		var se = new InputField.SubmitEvent ();
		se.AddListener (SubmitName);
		input.onEndEdit = se;


	
	}

	private void SubmitName(string arg0)
	{
		Debug.Log (arg0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
