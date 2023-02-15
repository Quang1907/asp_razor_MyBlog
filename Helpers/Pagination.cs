namespace ASP_RAZOR_5.Helpers
{
    public class Pagination
    {
        public int currentPage { get; set; }
        public int countPage { get; set; }
        public Func<int?, string> generateUrl { get; set; }
    }
}
