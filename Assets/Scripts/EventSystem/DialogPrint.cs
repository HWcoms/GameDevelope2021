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
	
	private DialogLoader dialogLoader;
	
	private List<DialogLoader.Dialog> dialogL;
	
	[SerializeField] private TextMeshProUGUI DialogTextBox;

	[SerializeField] private bool isPlayingDialog = false;

    // Start is called before the first frame update
    void Start()
    {
		dialogLoader = GameObject.Find("GoogleSheetSpreadLoader").GetComponent<DialogLoader>();
		dialogL = dialogLoader.dialogList;

		DialogTextBox = TransformDeepChildExtension.FindDeepChild(this.transform, "DialogContentText").GetComponent<TextMeshProUGUI>();

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
		if(!isPlayingDialog)
        {
			isPlayingDialog = true;
			PrintDialog();
		}
			
	}
	
	void GetDialogFromId(int Id) 
	{
		setToDialogInfo(dialogL[Id]);
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
		foreach(char letter in sentence.ToCharArray())
		{
			DialogTextBox.text += letter;
			yield return null;
		}
		isPlayingDialog = false;
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