namespace foodsale_api.Interfaces
{
    public interface IExcelHelper<T> where T : class
    {
        public List<T> ReadExcel(string targetPath);
    }
}
