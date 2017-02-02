using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	public bool isBuy = false;
	public bool isSell = false;
	public bool isQuit = false;
	public bool isMap = false;

	// Use this for initialization
	void Start () {

		//GetComponent<Renderer> ().material.color = Color.yellow;
	
	}
	
	// Update is called once per frame
	void Update () {

	

		if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel(0); }

	

	}

	void OnMouseEnter(){
		GetComponent<Renderer> ().material.color = Color.yellow;
	}

	void OnMouseExit() {
		
		GetComponent<Renderer> ().material.color = Color.white;

	}

	void OnMouseDown() {

		GetComponent<Renderer> ().material.color = Color.red;

	}

	void OnMouseUp(){
		if(isBuy)
		{
			Application.LoadLevel(3);
		}
		if (isSell)
		{
			Application.LoadLevel(2);
		}
		if (isMap) 
		{

			Application.LoadLevel(4);

		}
		if (isQuit) 
		{
			Application.Quit();
		}
	} 

}
