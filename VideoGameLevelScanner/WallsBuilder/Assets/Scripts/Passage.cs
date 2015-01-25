using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

public class Passage {
    public Point From {get; set;}
    public Point To {get; set;}
    public Passage(Point from, Point to)
    {
        From = from;
        To = to;
    }
    public Passage(KeyValuePair<int,int> from, KeyValuePair<int,int> to)
    {
        From = new Point (from.Key, from.Value);
        To = new Point (to.Key, to.Value );
    }
}
