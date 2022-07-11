namespace BornToMove;

using System.Data.SqlClient;
using System;
using System.Data;

public class Db
{
    private string ConnectionString;

    public Db(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public bool Create(string name, string description, int sweatRate)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            var query = "INSERT INTO moves (name, description, sweatRate) VALUES (@n, @d, @s)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@n", SqlDbType.VarChar).Value = name;
                command.Parameters.Add("@d", SqlDbType.VarChar).Value = description;
                command.Parameters.Add("@s", SqlDbType.Int).Value = sweatRate;
                
                var affected = command.ExecuteNonQuery();

                return affected > 0;
            }
        }
    }

    public List<Move> GetMoves(int? id)
    {
        var result = new List<Move>();
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            var query = "SELECT * FROM moves";
            if (id != null)
            {
                query += " WHERE id = @id";
            }
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (id != null)
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                }
                using (SqlDataReader rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var move = new Move()
                        {
                            Id = Convert.ToInt16(rdr["id"]),
                            Name = Convert.ToString(rdr["name"]),
                            Description = Convert.ToString(rdr["description"]),
                            SweatRate = Convert.ToInt16(rdr["sweatRate"])
                        };
                        
                        result.Add(move);
                    }
                    
                    connection.Close();
                }
            }
        }

        return result;
    }
}