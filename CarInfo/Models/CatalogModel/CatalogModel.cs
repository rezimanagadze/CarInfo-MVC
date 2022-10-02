namespace CarInfo.Models.CatalogModel
{
    public class CatalogModel
    {
        public Guid Id { get; set; }
        public string Mark { get; set; }
        public string Country { get; set; }
        public IFormFile Logo { get; set; }
    }
}
