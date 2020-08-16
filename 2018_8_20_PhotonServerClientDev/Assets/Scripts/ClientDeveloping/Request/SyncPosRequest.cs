using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;

public class SyncPosRequest : Request {

    public Vector3 position;

    protected override void Start()
    {
        base.Start();
    }

    public override void DefaultRequest()
    {
        Dictionary<byte, object> userPositionData = new Dictionary<byte, object>();
        //userPositionData.Add((byte)ParameterCode.Position, new Vector3Data() { X = position.x, Y=position.y,Z=position.z});
        userPositionData.Add((byte)ParameterCode.X,position.x);
        userPositionData.Add((byte)ParameterCode.Y, position.y);
        userPositionData.Add((byte)ParameterCode.Z, position.z);
        PhotonEngine.Peer.OpCustom((byte)OpCode, userPositionData, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
    }
}
