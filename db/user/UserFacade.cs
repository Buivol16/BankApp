using BankApp.db.user;
using BankApp.assets;
using BankApp.exceptions;
using System.Text.Json;

namespace BankApp.db.io
{
    public class UserFacade : IDbHandler<User>
    {
        public void DeleteById(int id)
        {
            var user = FindById(id);
            var fm = new FileManager(FileConsts.UserFilePath, true);
            var arrays = FileManager.ReadAll<User>(FileConsts.UserFilePath);
            new FileManager(FileConsts.UserFilePath, false).Write("");

            foreach (var item in arrays)
            {
                if (item.Equals(user)) arrays.Remove(item);
            }

            foreach (var item in arrays)
            {
                fm.Write(item.ToJson() + " ,\n, ");
            }

        }

        public User[] FindAll()
        {
            try
            {
                var arrays = FileManager.ReadAll<User>(FileConsts.UserFilePath);
                return [.. arrays];
            }
            catch (Exception)
            {
                return [];
            }
        }

        public User FindById(int id)
        {
            var arrays = FileManager.ReadAll<User>(FileConsts.UserFilePath);
            if (arrays.Count != 0)
            {
                var found = arrays.Where(a => a.Id == id);
                if (found.Count() == 1) return found.First();
            }
            throw new ObjectNotFoundException("There are no users found");
        }

        public User FindByLogin(string login)
        {
            var arrays = FileManager.ReadAll<User>(FileConsts.UserFilePath);
            if (arrays.Count() != 0)
            {
                var found = arrays.Where(a => a.Login == login);
                if (found.Count() == 1) return found.First();
            }
            throw new ObjectNotFoundException("There are no users found");
        }

        public User FindByEmail(string email)
        {
            var arrays = FileManager.ReadAll<User>(FileConsts.UserFilePath);
            if (arrays.Count != 0)
            {
                var found = arrays.Where(a => a.Email == email);
                if (found.Count() == 1) return found.First();
            }
            throw new ObjectNotFoundException("There are no users found");
        }

        public User FindByNumber(string phoneNumber)
        {
            var arrays = FileManager.ReadAll<User>(FileConsts.UserFilePath);
            if (arrays.Count != 0)
            {
                var found = arrays.Where(a => a.PhoneNumber == phoneNumber);
                if (found.Count() == 1) return found.First();
            }
            throw new ObjectNotFoundException("There are no users found");
        }

        public int FindLastIndex()
        {
            var all = FindAll();
            if (all.Length > 0)
            {
                var id = all.Last().Id;
                if (id != null) return (int)id;
            }
            return -1;
        }

        public User Save(User entity)
        {
            var fm = new FileManager(FileConsts.UserFilePath, true);
            try
            {
                if (entity.Id != null && FindById((int)entity.Id) != null) throw new ObjectExistsException("This id is exists already");
                if (entity.Login != null && FindByLogin(entity.Login) != null) throw new ObjectExistsException("This login is exists already"); ;
                if (entity.Email != null && FindByEmail(entity.Email) != null) throw new ObjectExistsException("This email is exists already");
                if (entity.PhoneNumber != null && FindByNumber(entity.PhoneNumber) != null) throw new ObjectExistsException("This phone number is exists already");
                throw new ObjectNotFoundException("Entity id is null.");
            }
            catch (ObjectNotFoundException exceptions)
            {
                entity.Id = FindLastIndex() + 1;
                fm.Write(entity.ToJson() + " ,\n, ");
            }
            return entity;
        }

        public User Update(User entity)
        {
            var user = FindById((int)entity.Id);
            var fm = new FileManager(FileConsts.UserFilePath, true);
            var arrays = FileManager.ReadAll<User>(FileConsts.UserFilePath);
            new FileManager(FileConsts.UserFilePath, false).Write("");

            foreach (var item in arrays)
            {
                if (item.Equals(user)) arrays[arrays.FindIndex((obj) => item.Id == obj.Id)] = entity;
            }

            foreach (var item in arrays)
            {
                fm.Write(item.ToJson() + " ,\n, ");
            }

            return entity;
        }
    }
}