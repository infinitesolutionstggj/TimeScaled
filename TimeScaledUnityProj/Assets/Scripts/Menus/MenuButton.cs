using UnityEngine;
using System.Collections;
using XboxCtrlrInput;


public class MenuButton : MonoBehaviour
{
    //public bool selected;
    public bool exitButton;
    //public string levelName;
    public Object Level = null;
    public XboxButton buttonPress;
    public XboxButton altButtonPress;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (exitButton && (XCI.GetButtonDown(buttonPress) || XCI.GetButtonDown(altButtonPress)))
            Application.Quit();
        else if (XCI.GetButtonDown(buttonPress) || XCI.GetButtonDown(altButtonPress))
            if (Level != null) Application.LoadLevel(Level.name);
	}
}
