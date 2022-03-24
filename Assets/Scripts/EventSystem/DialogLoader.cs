using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLoader : MonoBehaviour
{
    [SerializeField] private string sheetData;

    public List<string> availableLanguages = new List<string>();

    [System.Serializable]
    public class Lang
    {
        public string lang;
        public string Text, Button;
    }

    [System.Serializable]
    public class Dialog
    {
        public int Id;
        public string IdString, DialogType, Navigation;
        public bool EndDialog;

        public List<Lang> langInfo;
    }

    public List<Dialog> dialogList;

    public static DialogLoader Singleton;
    #region singleton
    private void Awake()
    {
        if (null == Singleton)
        {
            Singleton = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSheet());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetInfoList (string data)
    {
        string[] row = data.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;
        string[,] InfoArray = new string[rowSize, columnSize];

        for(int i=0; i<rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columnSize; j++) InfoArray[i, j] = column[j];
        }
        
        int langCount = GetLanguageCount(InfoArray, columnSize);

        dialogList = new List<Dialog>();
        for (int i = 1; i < columnSize; i++)
        {
            Dialog tempdialog = new Dialog();

            tempdialog.Id = int.Parse(InfoArray[i, 0]);
            tempdialog.IdString = InfoArray[i, 1];

            List<Lang> langList = new List<Lang>();
            tempdialog.langInfo = langList;
            //get dialogs from available languages
            for (int j = 0; j < langCount; j++)
            {
                Lang tempLang = new Lang();
                tempLang.lang = availableLanguages[j];
                tempLang.Text = InfoArray[i, 5 + (j * 2)];
                tempLang.Button = InfoArray[i, 5 + (j * 2) + 1];

                tempdialog.langInfo.Add(tempLang);
            }

            tempdialog.Navigation = InfoArray[i, 3];
            tempdialog.EndDialog = bool.Parse(InfoArray[i, 4]);

            dialogList.Add(tempdialog);
        }
    }

    IEnumerator LoadSheet()
    {
        while(this.GetComponent<GoogleSheetManager>().data == null || this.GetComponent<GoogleSheetManager>().data == "")
        {
            yield return new WaitForSeconds(0.5f);
            //print("Loading Dialog");
        }

        sheetData = this.GetComponent<GoogleSheetManager>().data;
        //print(sheetData);
        SetInfoList(sheetData);
    }

    int GetLanguageCount(string[,] dataArray, int ColumnSize)
    {
        int languageCount = 0;

        for(int i=0; i< ColumnSize; i++)
        {
            if(dataArray[0,i].Contains("TEXT"))
            {
                //print(dataArray[0,i]);
                int index = dataArray[0, i].IndexOf("_");

                availableLanguages.Add(dataArray[0, i].Remove(0, index + 1));

                languageCount++;
            }
        }

        return languageCount;
    }
}
