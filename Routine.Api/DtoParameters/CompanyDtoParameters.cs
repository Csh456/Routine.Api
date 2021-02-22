namespace Routine.Api.DtoParameters
{
    public class CompanyDtoParameters
    {
        private const int MaxPageSize = 20;
        public string CompanyName { get; set; }

        public string SearchTerm { get; set; }

        public int PageNum { get; set; } = 1;

        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = MaxPageSize > value ? value : MaxPageSize;
        }


    }
}
