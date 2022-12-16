using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoData : Subject
{
    private string title;
    private string description;
    private string fileName;
    private YoutobeNotify _yotobeNotifier;
    private FacebookNotidy _facebookNotifier;

    public string getTitle()
    {
        return title;
    }
    public void setTitle(string title)
    {
        this.title = title;
        VideoDataChange();
    }
    public void VideoDataChange()
    {
        notifyObserver(this, null);
    }
}
