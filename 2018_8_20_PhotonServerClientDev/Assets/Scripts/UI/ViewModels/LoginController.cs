using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Common;
using MyType;

public class LoginController : MonoBehaviour {

    public InputField usernameIf;
    public InputField passwordIf;
    public Text hintMessage;

    private LogginRequest logginRequest;

    private static LoginController instance;
    public static LoginController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        logginRequest = GetComponent<LogginRequest>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        instance = null;
    }

    public void OnLogginButtonClick()
    {
        hintMessage.text = "";
        logginRequest.Username = usernameIf.text;
        logginRequest.Password = passwordIf.text;
        logginRequest.DefaultRequest();

    }

    public void OnRegisterButtonClick()
    {
        PanelSetInstance.Instance.OpenPanel(Panels.registerMenu);
    }

    public void LogginUiSwitch(ReturnCode returnCode)
    {
        switch(returnCode)
        {
            case ReturnCode.Success:
                hintMessage.text = "Correct account and password,Logging...";
                SceneManager.LoadScene("Main");
                break;
            case ReturnCode.Failed:
                hintMessage.text = "Incorrect account or password,Failed...";
                break;
            default:
                hintMessage.text = "either success not failed,some error arose.";
                break;
        }
    }
}
