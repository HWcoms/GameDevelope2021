using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    [SerializeField] private bool showConsole;
    [SerializeField] private bool focused;

    string input;

    public static DebugCommand HEAL;
    public static DebugCommand KILL_ALL;
    public static DebugCommand HEAL_PLAYER;
    public static DebugCommand KILL_PLAYER;


    private DebugCommand[] commandList;
    [SerializeField] private string[] commandListID;

    public void OnToggleDebug()
    {
        showConsole = !showConsole;
    }

    public void OnReturn()
    {
        if(showConsole)
        {
            OnToggleDebug();
            HandleInput();
           
            input = "";
            focused = false;
        }
    }

    
    private void Awake()
    {
        //commands info
        HEAL = new DebugCommand("heal", "heal all enemys to max HP from the scene.", "heal", "heal");

        KILL_ALL = new DebugCommand("kill", "kill all enemys from the scene.", "kill", "kill");

        HEAL_PLAYER = new DebugCommand("heal player", "heal player to max HP.", "heal player", "heal_player");

        KILL_PLAYER = new DebugCommand("kill player", "kill player.", "kill player", "kill_player");

        commandList = new DebugCommand[]
        {
            HEAL,
            KILL_ALL,
            HEAL_PLAYER,
            KILL_PLAYER
        };

        getCommandListToTextArray();
    }

    void getCommandListToTextArray()
    {
        commandListID = new string[commandList.Length];

        for (int i = 0; i < commandList.Length; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            commandListID[i] = commandBase.commandId;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.BackQuote) && !showConsole)
        {
            OnToggleDebug();
        }
    }
    private void OnGUI()
    {
        Event e = Event.current;

        if (!showConsole) return;
        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);

        GUI.SetNextControlName("Console");
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);

        if (!GUI.GetNameOfFocusedControl().Equals("Console"))
            focused = false;
        else
            focused = true;

        if (!focused)
        {
            GUI.FocusControl("Console");
        }
        else if (focused)
        {
            if (e.keyCode == KeyCode.Return) {
                OnReturn();
            }
            else if (e.keyCode == KeyCode.Escape) {
                OnToggleDebug();
                input = "";
                focused = false;
            }
        }

    }

    private void HandleInput()
    {
        if(input == null) return;
        
        for(int i=0; i<commandList.Length; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if(input.Equals(commandBase.commandId))
            {

                if(commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).runCommand();
                }
            }
        }
    }

}
