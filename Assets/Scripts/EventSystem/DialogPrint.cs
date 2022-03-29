/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogPrint : MonoBehaviour
{
	public string dialogType;
	public string dialogText;
	public string buttonText;
	public string Navigation; // can be string or number
	public bool endDialog;
	public int camInfo;
	
	private DialogLoader;
	
	private List<DialogLoader.Dialog> dialogL;
	
	TextMeshProUGUI DialogTextBox;

    // Start is called before the first frame update
    void Start()
    {
		DialogLoader = GameObject.Find("DialogSystem").GetComponent<DialogLoader>();
		dialogL = DialogLoader.dialogList;
		
		StartCoroutine(LoadDialogList());
    }

	IEnumerator LoadDialogList()
    {
        while(dialogLoader.dialogList == null)
        {
            yield return new WaitForSeconds(0.5f);
            //print("Loading DialogList");
        }

        dialogL = dialogLoader.dialogList;
        //print(dialogL);
    }
	
    // Update is called once per frame
    void Update()
    {
        
    }
	
	void GetDialogFromId(int Id) 
	{
		setToDialogInfo(dialogL[i]);
	}
	
	void GetDialogFromIdString(string IdString) 
	{
	
	}
	
	void NextDialog()
	{
		ClearDialog();
		
		if(endDialog) return;
	}
	
	void PrintDialog()
	{
		//DialogTextBox.text = dialogText;
		
		StopAllCoroutines();
		StartCoroutine(TypeSentence(dialogText));
	}
	
	IEnumerator TypeSentence (string sentence)
	{
		ClearDialog();
		foreach(char letter int sentence.ToCharArray)
		{
			DialogTextBox.text += letter;
			yield return null;
		}
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
		
		this.dialogText = dialog.Lang[lang].Text;
		this.buttonText = dialog.Lang[lang].Button;
	}
	
}
*/
