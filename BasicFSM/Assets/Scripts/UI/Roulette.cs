using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Roulette : MonoBehaviour
{

    private Transform content;
    public int rotateLoop = 5;
    public int regionNum = 8;
    private Dictionary<string,Transform> index2unitDict = new Dictionary<string, Transform>();
    private Transform currentLightUnit;
    private bool isRotate = false;
    private int index2 = 0;
    private void Start() {
        isRotate = false;
        content = GameObject.Find("Content").transform;
        for (int i = 0; i < 8; i++)
        {
            var index = i+1;
            Transform child = content.GetChild(i);
            index2unitDict.Add(index.ToString(),child);
        }
    }

    void startUpRoulette()
    {
        isRotate = true;
        transform.rotation = Quaternion.identity;
        Vector3 rotateVec = new Vector3(0,0,-1 * 360 * rotateLoop);
        transform.DORotate(rotateVec,4)
        .SetEase(Ease.InOutCubic)
        .OnComplete(endCallBack);
    }

    void endCallBack()
    {
        isRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotate){
            // 计算亮的地方
            computeLightUnit();
        }
    }

    public void computeLightUnit()
    {
        float rouletteAngle = Mathf.Floor(transform.eulerAngles.z);
        float sectorAngle = 360/regionNum;
        float normalizeAngle = rouletteAngle % 360;
        if(normalizeAngle<0){
            // 化成整数容易转换些
            normalizeAngle+=360;
        }

        float tempAngleCounter = 0;
        int index = 0;
        for (int i = 1; i <= regionNum; i++)
        {
            tempAngleCounter = i*sectorAngle;
            if(normalizeAngle<tempAngleCounter){
                index = i;
                break;
            }
        }

        if(index2!=index){
            index2 = index;
            print("normalizeAngle: "+normalizeAngle);
            print(index2);
        }

        if(currentLightUnit){
            currentLightUnit.GetChild(0).GetComponent<Image>().color = Color.green;
        }

        index2unitDict.TryGetValue((9-index).ToString(),out currentLightUnit);
        currentLightUnit.GetChild(0).GetComponent<Image>().color = Color.white;
    }

    public void ButtonClick(){
        startUpRoulette();
    }
}
