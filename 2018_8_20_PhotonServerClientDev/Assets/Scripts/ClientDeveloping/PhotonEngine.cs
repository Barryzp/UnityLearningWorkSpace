using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common;
using Common.Tools;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{
    public string username;

    public static PhotonEngine Instance;

    private static PhotonPeer peer;
    public static PhotonPeer Peer
    {
        get
        {
            return peer;
        }
    }

    private Dictionary<OperationCode, Request> requestSet=new Dictionary<OperationCode, Request>();
    private Dictionary<EventCode, BaseEvent> eventSet = new Dictionary<EventCode, BaseEvent>();

    //解释代码：因为在游戏中，不管哪个场景，我们只需要一份PhotonEngine，当另外也有场景也挂载了相同的脚本，就把这个对象销毁，我们只需要一份
    //因为这是Static的，Instance是类的
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Use this for initialization
    void Start()
    {
        //通过Listener接收服务端的响应
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "MyGame1");
    }

    // Update is called once per frame
    void Update()
    {
        //如果客户端的状态是连接上
        //需要一直进行连接
        peer.Service();
    }

    void OnDestroy()
    {
        if (peer != null && peer.PeerState == PeerStateValue.Connected)
        {
            peer.Disconnect();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    //服务器端向客户端发消息时就会响应
    public void OnEvent(EventData eventData)
    {
        //这种方法称之为分发
        EventCode eventCode = (EventCode)eventData.Code;
        BaseEvent e = DictTool.GetDictValue<EventCode, BaseEvent>(eventSet, eventCode);
        e.OnEvent(eventData);

        #region 测试
        /*switch(eventData.Code)
        {
            case 1:
                object eventValue;
                eventData.Parameters.TryGetValue(2, out eventValue);
                Debug.Log("Receive a event,its value is: "+eventValue.ToString());
                break;
            default:
                break;
        }*/
        #endregion
    }

    //当客户端向服务器发起请求，服务器给客户端发出响应，然后这个方法就会被调用
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        OperationCode opCode = (OperationCode)operationResponse.OperationCode;

        Request request = null;
        bool isGetRq = requestSet.TryGetValue(opCode, out request);
        if(isGetRq)
        {
            request.OnOperationResponse(operationResponse);
        }
        else
        {
            Debug.Log("there is no matching value about opCode.");
        }

        #region 测试
        //根据不同的操作类型响应不同的事件
        /*switch(operationResponse.OperationCode)
        {
            case 1:
                Debug.Log("Receive a response form server.");

                //获取从服务端发送过来的数据
                Dictionary<byte, object> data = operationResponse.Parameters;
                object valueServer;
                data.TryGetValue(1, out valueServer);
                Debug.Log(valueServer.ToString());
                break;
            default:
                break;
        }*/
        #endregion
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log(statusCode);
    }

    public void AddRequest(Request request)
    {
        if(requestSet==null)
        {
            requestSet = new Dictionary<OperationCode, Request>();
        }
        requestSet.Add(request.OpCode, request);
    }

    public void RemoveRequest(Request request)
    {
        if(requestSet==null)
        {
            Debug.Log("RequestSet is Null ,Please add First.");
        }

        if(requestSet.Count==0)
        {
            Debug.Log("There are no more element to be removed.");
        }

        requestSet.Remove(request.OpCode);
    }

    public void AddEvent(BaseEvent e)
    {
        if(eventSet==null)
        {
            eventSet = new Dictionary<EventCode, BaseEvent>();
        }
        eventSet.Add(e.EventCd, e);
    }

    public void RemoveEvent(BaseEvent e)
    {
        if (eventSet == null)
        {
            Debug.Log("EventSet is Null ,Please add First.");
        }

        if (eventSet.Count == 0)
        {
            Debug.Log("There are no more element to be removed.");
        }
        eventSet.Remove(e.EventCd);
    }
}
