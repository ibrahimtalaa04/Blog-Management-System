namespace BlogManagementSystem.ViewModels
{
    public class BlogQueryParamters
    {
        const int _maxSize = 100;
        int _size = 20;

        public int CurPage { get; set; } = 1;

        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = Math.Min(_maxSize, value);
            }
        }

        public string SearchTearm { get; set; } = string.Empty;
    }
}
