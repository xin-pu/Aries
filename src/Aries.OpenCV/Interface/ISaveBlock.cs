namespace Aries.OpenCV.Interface
{


    interface ISaveBlock
    {
        bool EnableSaveBlock { set; get; }
        void SaveBlock();
    }
}
