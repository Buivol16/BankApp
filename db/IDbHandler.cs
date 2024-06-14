namespace BankApp.db{
    public interface IDbHandler<T>{
        public T FindById(int id);
        public T[] FindAll();
        public void DeleteById(int id);
        public T Update(T entity);
        public T Save(T entity);
        public int FindLastIndex();
    }
}