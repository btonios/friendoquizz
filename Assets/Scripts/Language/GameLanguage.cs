using System;
using UnityEngine;
using System.Collections.Generic;


public class GameLanguage : MonoBehaviour
{
	public static GameLanguage Instance;

	public static Dictionary<String, String> Fields;

	[SerializeField] string defaultLang = "english";
	[SerializeField] public string[] Langs;


	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}

		LoadLanguage();
	}


	void LoadLanguage()
	{
		if (Fields == null)
			Fields = new Dictionary<string, string> ();
		
		Fields.Clear ();

		string lang = PlayerPrefs.GetString ("language", defaultLang);
        

		string allTexts = (Resources.Load (@"Languages/" + lang) as TextAsset).text; //without (.txt)

		string[] lines = allTexts.Split (new string[] {"\n"}, StringSplitOptions.None);

        foreach(string line in lines)
        {
            string[] splits = line.Split(new string[] {"="}, StringSplitOptions.None);
            string key = "";
            string value ="";
            if(splits != null || splits[0] != null || splits[1] != null)
            {
                key = splits[0];
                value = splits[1];
            }
            
            Fields.Add(key, value);
        }
	}

	public static string GetTraduction (string key)
	{
		if (!Fields.ContainsKey (key)) {
			Debug.LogError ("There is no key with name: [" + key + "] in your text files");
			return null;
		}

		return Fields [key];
	}


}
