using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogPrint : MonoBehaviour
{
	[SerializeField] private bool isDialogLoaded = false;
	public float dialogSpeed = 0.05f;
	public int current_language = 0;

	public string dialogType;
	public string dialogText;
	public string buttonText;
	public string Navigation; // can be string or number
	public bool endDialog;
	public int camInfo;
	
	private DialogLoader dialogLoader;
	
	private List<DialogLoader.Dialog> dialogL;
	
	private TextMeshProUGUI DialogTextBox;

	[SerializeField] private bool isDialogReady;


    // Start is called before the first frame update
    void Start()
    {
		HideUI();

		isDialogLoaded = false;

		dialogLoader = GameObject.Find("GoogleSheetSpreadLoader").GetComponent<DialogLoader>();
		//dialogL = dialogLoader.dialogList;

		DialogTextBox = TransformDeepChildExtension.FindDeepChild(this.transform, "DialogContentText").GetComponent<TextMeshProUGUI>();
		
		StartCoroutine(LoadDialogList());

		isDialogReady = true;
	}

	IEnumerator LoadDialogList()
    {
        while(dialogLoader.dialogList.Count == 0)
        {
            yield return new WaitForSeconds(0.5f);
            //print("Loading DialogList");
        }

        dialogL = dialogLoader.dialogList;
		isDialogLoaded = true;
		//print(dialogL);

		GetDialogFromId(0);
	}
	
    // Update is called once per frame
    void Update()
    {
		/*
		if (!isDialogLoaded) return;

		if (Input.GetKeyDown("r"))
			NextDialog();

		if (isDialogReady)
        {
			isDialogReady = false;
			//GetDialogFromIdString("ด๋ป็1");
			PrintDialog();
		}
		*/
	}

	public void ViewUI()
    {
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(true);
		}
	}

	public void HideUI()
	{
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(false);
		}
	}

	bool GetDialogFromId(int Id) 
	{
		return GetDialogFromId(Id, current_language);
	}

	bool GetDialogFromId(int Id, int lang)
	{
		if(dialogL.Count <= Id) //count 1 -> Id 0, count 2 -> Id 0,1	count 0 -> x
			return false;
        else
			setToDialogInfo(dialogL[Id], lang);

		return true;
	}


	bool GetDialogFromIdString(string IdString) 
	{
		return GetDialogFromIdString(IdString, current_language);
	}

	bool GetDialogFromIdString(string IdString, int lang)
    {
		foreach (DialogLoader.Dialog dialog in dialogL)
		{
			if (IdString.Equals("")) break;

			if (dialog.IdString.Equals(IdString))
			{
				print("found dialog info success");

				setToDialogInfo(dialog, lang);

				return true;
			}
		}
		print("can't find dialog info");

		return false;
	}


	public bool NextDialog()	//false : endDialog, true: continue
	{

		ClearDialog();
		
		if(endDialog)
        {
			HideUI();

			return false;
        }


		int navigation_n;
		if(int.TryParse(Navigation, out navigation_n)) //navigaiton is number
        {
			print("number " + navigation_n);
			GetDialogFromId(navigation_n);
		}
		else    //navigation is Note string
		{
			GetDialogFromIdString(Navigation);
		}
		print(Navigation);

		isDialogReady = true;

		PrintDialog();

		return true;
	}

	public void PrintDialog(int index)
	{
		if(!GetDialogFromId(index, current_language))
		{
			dialogText = "Error, cannot find dialog text";
		}

		PrintDialog();
	}

	public void PrintDialog(string note)
	{
		if (!GetDialogFromIdString(note, current_language))
        {
			dialogText = "Error, cannot find dialog text";
		}

		PrintDialog();
	}

	void PrintDialog()
	{
		//DialogTextBox.text = dialogText;
		
		StopAllCoroutines();
		StartCoroutine(TypeSentence(dialogText));
	}
	
	IEnumerator TypeSentence (string sentence)
	{
		sentence = sentence.Replace("\\n", "\n");

		ClearDialog();
		yield return new WaitForSeconds(0f);

		foreach (char letter in sentence.ToCharArray())
		{
			DialogTextBox.text += letter;
			yield return new WaitForSeconds(dialogSpeed);
		}
		//isDialogReady = false;
	}
	
	
	void ClearDialog()
	{
		DialogTextBox.text = "";
	}
	
	void setToDialogInfo(string dialogType, string Navigation, bool endDialog, int camInfo, string dialogText, string buttonText)
	{
		this.dialogType = dialogType;
		this.Navigation = Navigation;
		this.endDialog = endDialog;
		this.camInfo = camInfo;
		
		this.dialogText = dialogText;
		this.buttonText = buttonText;
	}
	
	void setToDialogInfo(DialogLoader.Dialog dialog, int lang = 0)	//lang 0 : kor
	{
		this.dialogType = dialog.DialogType;
		this.Navigation = dialog.Navigation;
		this.endDialog = dialog.EndDialog;
		this.camInfo = dialog.camInfo;
		
		this.dialogText = dialog.langInfo[lang].Text;
		this.buttonText = dialog.langInfo[lang].Button;
	}

	public bool getDialogLoaded()
    {
		return isDialogLoaded;
    }

}

public static class TransformDeepChildExtension
{
	//Breadth-first search
	public static Transform FindDeepChild(this Transform aParent, string aName)
	{
		Queue<Transform> queue = new Queue<Transform>();
		queue.Enqueue(aParent);
		while (queue.Count > 0)
		{
			var c = queue.Dequeue();
			if (c.name == aName)
				return c;
			foreach (Transform t in c)
				queue.Enqueue(t);
		}
		return null;
	}
}