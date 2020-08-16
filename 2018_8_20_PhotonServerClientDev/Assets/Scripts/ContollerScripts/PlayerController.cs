using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Common.Tools;

public class PlayerController : MonoBehaviour
{

    public string username;
    public bool isLoacalPlayer;
    public float speed;
    public GameObject player;
    public GameObject playerPrefab;

    private float moveOffset = 0.1f;
    private SyncPosRequest syncPosRq;
    private SyncPlayerRequest syncPlayerRq;
    private Vector3 lastPosition = Vector3.zero;
    private Dictionary<string, GameObject> gameObjectDict = new Dictionary<string, GameObject>();

    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (isLoacalPlayer)
        {
            syncPosRq = GetComponent<SyncPosRequest>();
            syncPlayerRq = GetComponent<SyncPlayerRequest>();
            syncPlayerRq.DefaultRequest();

            player.GetComponent<Renderer>().material.color = Color.green;
            InvokeRepeating("SyncPosition", 1, 0.05f);
        }
    }

    void Update()
    {
        if (isLoacalPlayer)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float forward = Input.GetAxis("Vertical");

            player.transform.Translate(new Vector3(horizontal, 0, forward) * Time.deltaTime * speed);
        }
    }

    void OnDestroy()
    {
        instance = null;
    }

    void SyncPosition()
    {
        if (Vector3.Distance(player.transform.position, lastPosition) > moveOffset)
        {
            lastPosition = player.transform.position;
            syncPosRq.position = player.transform.position;
            syncPosRq.DefaultRequest();
        }
    }

    public void OnSyncPlayerResponse(List<string> usernameList)
    {
        foreach (var item in usernameList)
        {
            OnNewPlayerEvent(item);
        }
    }

    public void OnNewPlayerEvent(string name)
    {
        var ob = Instantiate(playerPrefab);
        gameObjectDict.Add(name, ob);
    }

    public void OnSyncPlayerPosition(List<PlayerData> playerDatas)
    {
        foreach (var item in playerDatas)
        {
            var go = DictTool.GetDictValue<string, GameObject>(gameObjectDict, item.Username);

            if(go!=null)
            {
                go.transform.position = new Vector3(item.Position.X, item.Position.Y, item.Position.Z);
            }

        }
    }

}
