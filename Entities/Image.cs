namespace Modsenfy.Entities
{
    public class Image
    {
        public int ImageId { get; set; }

        public string ImageFilename { get; set; }

        public int ImageTypeId { get; set; }

        public ImageType ImageType { get; set; }
    }
}
