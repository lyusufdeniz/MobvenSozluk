namespace MobvenSozluk.Repository.Services
{
    public interface ISearchingService<T> where T : class
    {
        public IEnumerable<T> Search(IEnumerable<T> items,string query);
    }
}
