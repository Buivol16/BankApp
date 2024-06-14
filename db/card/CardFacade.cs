using BankApp.db.card;
using BankApp.assets;
using BankApp.exceptions;
using System.Text.Json;
using BankApp.db.user;

namespace BankApp.db.io
{
    public class CardFacade : IDbHandler<Card>
    {
        public void DeleteById(int id)
        {
            var Card = FindByCardNumber(id);
            var fm = new FileManager(FileConsts.CardFilePath, true);
            var arrays = FileManager.ReadAll<Card>(FileConsts.CardFilePath);
            new FileManager(FileConsts.CardFilePath, false).Write("");

            foreach (var item in arrays)
            {
                if (item.Equals(Card)) arrays.Remove(item);
            }

            foreach (var item in arrays)
            {
                fm.Write(item.ToJson() + " ,\n, ");
            }

        }

        public Card[] FindAll()
        {
            try
            {
                var arrays = FileManager.ReadAll<Card>(FileConsts.CardFilePath);
                return [.. arrays];
            }
            catch (JsonException exception)
            {
                return [];
            }
        }

        public Card FindByCardNumber(int CardNumber)
        {
            var arrays = FileManager.ReadAll<Card>(FileConsts.CardFilePath);
            if (arrays.Count != 0)
            {
                var found = arrays.Where(a => a.CardNumber == CardNumber);
                if (found.Count() == 1) return found.First();
            }
            throw new ObjectNotFoundException("There are no Cards found");
        }

        public Card FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Card FindByOwner(User owner)
        {
            var arrays = FileManager.ReadAll<Card>(FileConsts.CardFilePath);
            if (arrays.Count() != 0)
            {
                var found = arrays.Where(a => a.Owner.Equals(owner));
                if (found.Count() == 1) return found.First();
            }
            throw new ObjectNotFoundException("There are no Cards found");
        }

        public int FindLastIndex()
        {
            var all = FindAll();
            if (all.Length > 0)
            {
                var id = all.Last().CardNumber;
                if (id != null) return id;
            }
            return -1;
        }

        public Card Save(Card entity)
        {
            var fm = new FileManager(FileConsts.CardFilePath, true);
            try
            {
                if (entity.CardNumber != null && FindByCardNumber(entity.CardNumber) != null) throw new ObjectExistsException("This card number is exists already");
                if (entity.Owner != null && FindByOwner(entity.Owner) != null) throw new ObjectExistsException("This owner have card already"); ;
                throw new ObjectNotFoundException("Entity card number is null.");
            }
            catch (ObjectNotFoundException exceptions)
            {
                fm.Write(entity.ToJson() + " ,\n, ");
            }
            return entity;
        }

        public Card Update(Card entity)
        {
            var Card = FindByCardNumber(entity.CardNumber);
            var fm = new FileManager(FileConsts.CardFilePath, true);
            var arrays = FileManager.ReadAll<Card>(FileConsts.CardFilePath);
            new FileManager(FileConsts.CardFilePath, false).Write("");

            arrays[arrays.FindIndex((obj) => entity.CardNumber == obj.CardNumber)] = entity;

            foreach (var item in arrays)
            {
                fm.Write(item.ToJson() + " ,\n, ");
            }

            return entity;
        }
    }
}