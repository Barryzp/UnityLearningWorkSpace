using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ExitGames.Client.Photon;

public class LogginRequest : Request
{
    private string username;
    public string Username
    {
        get
        { return username; }
        set
        { username = value; }
    }

    private string password;
    public string Password
    {
        get
        { return password; }
        set
        { password = value; }
    }

    public override void DefaultRequest()
    {
        Dictionary<byte, object> userInputData = new Dictionary<byte, object>();
        userInputData.Add((byte)ParameterCode.Username, username);
        userInputData.Add((byte)ParameterCode.Password, password);

        PhotonEngine.Peer.OpCustom((byte)OpCode,userInputData, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        ReturnCode returnCode = (ReturnCode)operationResponse.ReturnCode;

        if (returnCode==ReturnCode.Success)
        {
            PhotonEngine.Instance.username = username;
        }

        LoginController.Instance.LogginUiSwitch(returnCode);
    }

}
