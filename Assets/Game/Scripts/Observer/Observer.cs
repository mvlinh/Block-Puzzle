using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer
{
    protected Subject sub;
    public abstract void notify(Subject subject, Object obj);
}
