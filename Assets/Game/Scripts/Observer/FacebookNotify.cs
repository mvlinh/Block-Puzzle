using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookNotidy : Observer
{
    public FacebookNotidy(Subject sub)
    {
        this.sub = sub;
        this.sub.attachObserver(this);
    }
    public override void notify(Subject subject, Object obj)
    {
        if (subject is VideoData videoData)
        {
            Debug.Log("Facebook: " + videoData.getTitle());

        }
    }
}
