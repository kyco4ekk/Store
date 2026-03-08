using System.Text;
using TestTask.Models;

namespace TestTask.Services
{
    internal class ProductService
    {
        private readonly DataBaseService _db;

        public ProductService(DataBaseService db)
        {
            _db = db;
        }

        public Product Add(string name)
        {
            const string checkSql = "SELECT id, name FROM product WHERE LOWER(name) = LOWER(@name)";
            var existing = _db.QuerySingle(checkSql,
                r => new Product { Id = r.GetInt32(0), Name = r.GetString(1) },
                cmd => cmd.Parameters.AddWithValue("@name", name));

            if (existing != null)
                return existing;

            const string insertSql = "INSERT INTO product (name) OUTPUT INSERTED.id VALUES (@name)";
            int id = _db.QuerySingle(insertSql,
                r => r.GetInt32(0),
                cmd => cmd.Parameters.AddWithValue("@name", name));

            return new Product { Id = id, Name = name };
        }

        public List<Product> GetAll()
        {
            const string sql = "SELECT id, name FROM product ORDER BY id";
            return _db.QueryList(sql, r => new Product
            {
                Id = r.GetInt32(0),
                Name = r.GetString(1)
            });
        }

        public void Delete(int id)
        {
            const string sql = "DELETE FROM product WHERE id = @id";
            _db.Execute(sql, cmd => cmd.Parameters.AddWithValue("@id", id));
        }
        public void Update(int id, string newName)
        {
            const string checkSql = "SELECT id FROM product WHERE LOWER(name) = LOWER(@name) AND id <> @id";
            var exists = _db.QuerySingle(checkSql,
                r => r.GetInt32(0),
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@name", newName);
                    cmd.Parameters.AddWithValue("@id", id);
                });

            if (exists != 0)
                throw new Exception("Товар с таким названием уже существует!");

            const string sql = "UPDATE product SET name = @name WHERE id = @id";
            _db.Execute(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@id", id);
            });
        }

    }
}