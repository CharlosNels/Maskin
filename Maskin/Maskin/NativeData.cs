using System;
using System.Collections.Generic;
using System.Text;

namespace Maskin
{
    public class NativeData
    {
        public enum MaskinBorderStyle
        {
            MaskinStyle,
            NormalStyle
        }
        public enum PlayState
        {
            Playing,
            Pause
        }
        public enum LoopState
        {
            SingleCycle,
            ListCycle,
            RandomPlay
        }
        public enum GridenStyles
        {
            Normal,
            Center
        }
    }
}
