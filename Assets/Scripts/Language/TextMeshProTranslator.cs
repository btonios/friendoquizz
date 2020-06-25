using UnityEngine;
using TMPro;

[RequireComponent (typeof(TMP_Text))]
public class TextMeshProTranslator : MonoBehaviour
{
	[SerializeField] 
	string key;

	void Start ()
	{
		GetComponent <TMP_Text> ().text = GameLanguage.GetTraduction (key);
	}
}
