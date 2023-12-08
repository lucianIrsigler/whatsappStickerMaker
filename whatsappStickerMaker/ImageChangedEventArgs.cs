using System;

namespace whatsappStickerMaker
{
    public class ImageChangedEventArgs : EventArgs
    {

        //row and col of the image
        public int Row { get; }
        public int Col { get; }

        public ImageChangedEventArgs(int row,int col)
        {
            Row = row;
            Col = col;
        }
    }
}
