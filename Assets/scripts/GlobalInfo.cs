using UnityEngine;
using System.Collections;

public class GlobalInfo : MonoBehaviour {

	public static GlobalInfo Instance;

	public string username;
	public string productprice;
	public string id;
	public string productname;
	public string sellername;
	public string takingCash;
	public string takingCredit;
	public string condition;
	public string email;
	public string phone;
	public string latitude;
	public string longitude;
	public string altitude;




	void Awake ()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}
}
