using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConnecting : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            Request();
        }
	}

    void Request()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        string clientValue = "The data from client.";
        data.Add(1, clientValue);

        //为true代表数据就一定能传输过去，false就不会保证
        PhotonEngine.Peer.OpCustom(1,data,true);
    }
}
