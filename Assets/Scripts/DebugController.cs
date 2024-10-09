using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DebugController : MonoBehaviour
{
    private bool showConsole;
    private bool showHelp;
    private string input;

    //TODO : LISTE DE COMMANDES DEBUGS

    private List<object> commandList;

    private void Awake()
    {
        //TODO : Initialiser les commandes debugs
        /*Exemple
        DEBUG_COMMAND_NAME = new DebugCommand("command_id", "Command description, "command_format", () =>
        {
            Du code
        });*/
        

        commandList = new List<object>
        {
            //TODO : Ajouter les commandes prévues à la liste
        };
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            showConsole = !showConsole;
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (!showConsole) return;

            HandleInput();
            input = "";

        }
    }

    Vector2 scroll;

    private void OnGUI()
    {
        if (!showConsole) return;


        float y = 0f;


        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            Rect viewport = new Rect(0,0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for(int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();
            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input); 
    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');

        for(int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if(input.Contains(commandBase.commandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }

                if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }

                if (commandList[i] as DebugCommand<string> != null)
                {
                    (commandList[i] as DebugCommand<string>).Invoke(properties[1]);
                }
            }
        }
    }
}
