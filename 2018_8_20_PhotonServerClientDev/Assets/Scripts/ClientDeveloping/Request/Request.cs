using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common;

public abstract class Request : MonoBehaviour {
    public OperationCode OpCode;
    public abstract void DefaultRequest();
    public abstract void OnOperationResponse(OperationResponse operationResponse);

    protected virtual void Start()
    {
        PhotonEngine.Instance.AddRequest(this);
    }

    private void OnDestroy()
    {
        PhotonEngine.Instance.RemoveRequest(this);
    }
}
