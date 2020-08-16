using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;
using Common.Tools;
using System.IO;
using System.Xml.Serialization;

public class SyncPositionEvent : BaseEvent {
    public override void OnEvent(EventData eventData)
    {
        string playerDataListString = (string)DictTool.GetDictValue<byte, object>(eventData.Parameters,(byte)ParameterCode.PlayerDataList);

        using (StringReader sr=new StringReader(playerDataListString))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PlayerData>));
            List<PlayerData> playerDataList = (List<PlayerData>)xmlSerializer.Deserialize(sr);

            PlayerController.Instance.OnSyncPlayerPosition(playerDataList);
        }
    }
}
