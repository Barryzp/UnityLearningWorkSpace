using MyType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RegisterController : MonoBehaviour {

    public InputField usernameIf;
    public InputField passwordIf;
    public Text hintMessage;

    private static RegisterController instance;
    public static RegisterController Instance
    {
        get
        {
            return instance;
        }
    }
    private RegisterRequest registerRequest;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void OnRegisterButtonClick()
    {
        hintMessage.text = "";
        registerRequest.Username = usernameIf.text;
        registerRequest.Password = passwordIf.text;
        registerRequest.DefaultRequest();
    }

    public void OnBackButtonClick()
    {
        PanelSetInstance.Instance.OpenPanel(Panels.logginMenu);
    }

    public void RegisterUiSwtich(ReturnCode returnCode)
    {
        switch(returnCode)
        {
            case ReturnCode.AccountExist:
                hintMessage.text = "Account has been existed.";
                break;
            case ReturnCode.RegisterSuccess:
                hintMessage.text = "Register account successfully.";
                break;
        }
    }
}
