using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;
using Common.Tools;

public class NewPlayerEvent : BaseEvent {

    public override void OnEvent(EventData eventData)
    {
        string username = (string)DictTool.GetDictValue<byte, object>(eventData.Parameters,(byte)ParameterCode.Username);
        PlayerController.Instance.OnNewPlayerEvent(username);
    }
}
