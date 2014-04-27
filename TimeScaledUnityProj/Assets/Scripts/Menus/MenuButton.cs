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

    // special
    public bool special = false;
    public string specialComponentName;
    public string specialFunction;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (exitButton && (XCI.GetButtonDown(buttonPress, 1) || XCI.GetButtonDown(altButtonPress, 1)))
            Application.Quit();
        else if (XCI.GetButtonDown(buttonPress, 1) || XCI.GetButtonDown(altButtonPress, 1))
        {
            if (special)
                GameObject.Find("Menu").SendMessage(specialFunction);

            if (Level != null) Application.LoadLevel(Level.name);
        }
	}
}
