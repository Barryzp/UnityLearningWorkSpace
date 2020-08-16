using Common;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEvent : MonoBehaviour {
    public EventCode EventCd;
    public abstract void OnEvent(EventData eventData);

    protected virtual void Start()
    {
        PhotonEngine.Instance.AddEvent(this);
    }

    private void OnDestroy()
    {
        PhotonEngine.Instance.RemoveEvent(this);
    }

}
