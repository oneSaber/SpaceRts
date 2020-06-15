using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class ShipInfo : MonoBehaviour
{
    public string UUID;
    public string ShipName;
    public float Speed;
    public float RotateSpeed;
    //public float ViewAnagle;
    public float ViewDistance;
    public float HP;
    public float Attack;
    public float AttackDistance;
    public string BeLong;

    private List<string> EnemyList;
    private List<string> FriendList;

    public void InitInfomation(string shipname,string belong,NavMeshAgent agent)
    {
        UUID = Guid.NewGuid().ToString();
        ShipName = shipname;
        Speed = agent.speed;
        RotateSpeed = agent.angularSpeed;
        ViewDistance = 100.0f;
        HP = 100.0f;
        Attack = 15.0f;
        AttackDistance = 50.0f;
        BeLong = belong;
        EnemyList = new List<string>();
        FriendList = new List<string>();
    }
    public void AddEnemy(string belong)
    {
        if (!EnemyList.Contains(belong))
        {
            EnemyList.Add(belong);
        }
        if (FriendList.Contains(belong))
        {
            FriendList.Remove(belong);
        }
    }
    public void AddFriend(string belong)
    {
        if (!FriendList.Contains(belong))
        {
            FriendList.Add(belong);
        }
        if (EnemyList.Contains(belong))
        {
            EnemyList.Remove(belong);
        }
    }
    public void RemoveBelong(string belong, string list)
    {
        if(list == "Firend")
        {
            FriendList.Remove(belong);
        }
        if(list == "Enemy")
        {
            EnemyList.Remove(belong);
        }
    }
    public bool IsEnemy(string belong)
    {
        if (EnemyList.Contains(belong))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
