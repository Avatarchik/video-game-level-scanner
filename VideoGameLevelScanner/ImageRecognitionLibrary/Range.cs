using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageRecognitionLibrary
{
    public class Range<T>
    {
        public T Min;
        public T Max;
        public Range()
        {

        }
        public Range(T min, T max)
        {
            this.Min = min;
            this.Max = max;
        }
    }
}
