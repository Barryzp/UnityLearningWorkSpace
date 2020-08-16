using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;
using Common.Tools;
using System.Xml.Serialization;
using System.IO;

public class SyncPlayerRequest : Request
{

    protected override void Start()
    {
        base.Start();
    }

    public override void DefaultRequest()
    {
        PhotonEngine.Peer.OpCustom((byte)OpCode, null, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        string xmlString =(string) DictTool.GetDictValue<byte, object>(operationResponse.Parameters, (byte)ParameterCode.UsernameList);

        #region 反序列化
        using (StringReader sr=new StringReader(xmlString))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            List<string> usernameList =(List<string>) xmlSerializer.Deserialize(sr);
            PlayerController.Instance.OnSyncPlayerResponse(usernameList);
        }
        #endregion
    }
}
