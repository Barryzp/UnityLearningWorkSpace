using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;

public class RegisterRequest : Request {

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

        PhotonEngine.Peer.OpCustom((byte)OpCode, userInputData, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        ReturnCode returnCode = (ReturnCode)operationResponse.ReturnCode;
        RegisterController.Instance.RegisterUiSwtich(returnCode);
    }
}
