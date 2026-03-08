using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Models;

namespace TestTask.Services
{
    internal class IncomingService
    {
        private readonly DataBaseService _db;

        public IncomingService(DataBaseService db)
        {
            _db = db;
        }

        public void Add(int productId, int amount)
        {
            const string sql = @"
            INSERT INTO incoming (productId, amount, date)
            VALUES (@productId, @amount, GETDATE())";

            _db.Execute(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@amount", amount);
            });
        }

        public List<Incoming> GetAll()
        {
            const string sql = "SELECT id, productId, amount, date FROM incoming ORDER BY id";

            return _db.QueryList(sql, r => new Incoming
            {
                Id = r.GetInt32(0),
                ProductId = r.GetInt32(1),
                Amount = r.GetInt32(2),
                Date = r.GetDateTime(3)
            });
        }
        public void Delete(int id)
        {
            const string sql = "DELETE FROM incoming WHERE id = @id";
            _db.Execute(sql, cmd => cmd.Parameters.AddWithValue("@id", id));
        }

        public void Update(int id, int productId, int amount, DateTime date)
        {
            const string sql = @"
        UPDATE incoming
        SET productId = @productId,
            amount = @amount,
            date = @date
        WHERE id = @id";

            _db.Execute(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@date", date);
            });
        }
        public List<Incoming> GetByPeriod(DateTime from, DateTime to)
        {
            const string sql = @"
        SELECT id, productId, amount, date
        FROM incoming
        WHERE date BETWEEN @from AND @to
        ORDER BY date";

            return _db.QueryList(sql, r => new Incoming
            {
                Id = r.GetInt32(0),
                ProductId = r.GetInt32(1),
                Amount = r.GetInt32(2),
                Date = r.GetDateTime(3)
            },
            cmd =>
            {
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
            });
        }
    }
}