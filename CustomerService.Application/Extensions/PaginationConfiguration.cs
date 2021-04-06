namespace CustomerService.Application.Extensions
{
    public class PaginationConfiguration
    {
        public const int ItemsPerPage = 20;

        public static (int skip, int take) GetPaginationConfig(int page)
        {
            var skip = page == 1 ? 0 : (page * ItemsPerPage) - ItemsPerPage;

            return (skip, ItemsPerPage);
        }
    }
}