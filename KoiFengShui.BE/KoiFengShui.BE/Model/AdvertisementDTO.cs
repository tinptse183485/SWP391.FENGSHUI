namespace KoiFengShui.BE.Model
{
    public class AdvertisementDTO
    {

        public string? AdId { get; set; } = null!;
        public string Heading { get; set; } = null!;

        public string Image { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Rank { get; set; } = null!;
        public string? ElementId { get; set; }
        public string status { get; set; } = null!;

    }
}
