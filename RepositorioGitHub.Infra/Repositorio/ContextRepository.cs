using RepositorioGitHub.Dominio;
using RepositorioGitHub.Infra.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGitHub.Infra.Repositorio
{
    public class ContextRepository : IContextRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ContextData"].ToString();
        public bool ExistsByCheckAlready(Favorite favorite)
        {
            return false;
        }

        public List<Favorite> GetAll()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT * FROM Favoritos");

            var comand = connection.CreateCommand();
            comand.CommandText = sql.ToString();
            comand.CommandType = System.Data.CommandType.Text;

            var reader = comand.ExecuteReader();

            var lista = new List<Favorite>();
            while (reader.Read())
            {
                var favorito = new Favorite();
                favorito.Id = Convert.ToInt64(reader["Id"].ToString());
                favorito.Description = reader["Description"].ToString();
                favorito.Language = reader["Language"].ToString();
                favorito.Name = reader["Name"].ToString();
                favorito.Owner = reader["Owner"].ToString();
                favorito.UpdateLast = Convert.ToDateTime(reader["UpdateLast"]);

                lista.Add(favorito);
            }

            return lista;
        }

        public bool Insert(Favorite favorite)
        {
           
            var connection = new SqlConnection(connectionString);
            connection.Open();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("INSERT INTO Favoritos (Description, Language, UpdateLast, Owner, Name) Values('{0}', '{1}', '{2}', '{3}', '{4}')", favorite.Description, favorite.Language, favorite.UpdateLast, favorite.Owner, favorite.Name);

            var comand = connection.CreateCommand();
            comand.CommandText = sql.ToString();
            comand.CommandType = System.Data.CommandType.Text;
           

            if (comand.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
