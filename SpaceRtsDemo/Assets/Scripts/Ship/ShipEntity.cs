using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipEntity : MonoBehaviour
{
    bool beControl;
    ShipEngineRoom engineRoom;
    void Start()
    {
        beControl = false;
        engineRoom = gameObject.AddComponent<ShipEngineRoom>();
    }

    void Update()
    {
        
    }

    // 设置航行目标
    public void setTarget(Vector3 target)
    {
        if (target.y != gameObject.transform.position.y)
            target.y = gameObject.transform.position.y;
        engineRoom.SetEndPosition(target);
    }
    public void ChooseControl(bool control)
    {
        beControl = control;
        if (beControl)
        {
            Debug.Log(gameObject.transform.name + "被控制");
        }
        else
        {
            Debug.Log(gameObject.transform.name + "取消控制");
        }
    }
}
