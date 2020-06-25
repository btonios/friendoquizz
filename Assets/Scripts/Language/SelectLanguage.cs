using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLanguage : MonoBehaviour
{
	int index;
    public RawImage settingFlag;

    public void Start()
    {
        GlobalVariables.SetLanguage(PlayerPrefs.GetString("language"));
        SetSettingsFlag(GlobalVariables.LANGUAGE);
    }

    public void Select(string language)
    {
        PlayerPrefs.SetString ("language", language);
        GlobalVariables.LANGUAGE = language;
        ApplyLanguageChanges();
        GameObject.Find("MenuManager").GetComponent<MenuManager>().ToggleLanguages();
    }
    
	void ApplyLanguageChanges ()
	{
		SceneManager.LoadScene(0);
	}

    public void SetSettingsFlag(string language)
    {
        settingFlag.texture = (Texture2D) Resources.Load("Flags/flag_"+language);
    }
}
