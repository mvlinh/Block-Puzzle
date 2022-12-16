using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    public List<Observer> observer = new List<Observer>();

    public void attachObserver(Observer obj)
    {
        observer.Add(obj);
    }

    public void deleteObserver(Observer obj)
    {
        if(observer.Contains(obj))
            observer.Remove(obj);
    }

    public void notifyObserver(Subject sub, Object arg)
    {
        observer.ForEach((obj) => obj.notify(sub, arg));
    }
}
