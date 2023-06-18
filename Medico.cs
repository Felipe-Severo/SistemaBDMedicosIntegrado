using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Drawing;
using SistemaBDCompleto.BancoDados;

namespace SistemaBDCompleto
{
    internal class Medico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CRM { get; set; }
        public string Especialidade { get; set; }
        public string Senha { get; set; }

        public Medico()
        {
            Console.WriteLine($"Informe o nome do médico:");
            Nome = Console.ReadLine();


            Console.WriteLine($"Informe o CRM:");
            CRM = Console.ReadLine();


            Console.WriteLine($"Informe a especialidade:");
            Especialidade = Console.ReadLine();

            Console.WriteLine($"Informe a senha:");
            Senha = Console.ReadLine();


        }

        public Medico(long id)
        {
            using (var conn = new SqlConnection(DBinfo.DBConnection))
            {

                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "select ID, NOME, CRM,ESPECIALIDADE FROM MEDICO WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                        Nome = reader.GetString(1);
                        CRM = reader.GetString(2);
                        Especialidade = reader.GetString(3);
                        Senha = reader.GetString(4);

                    }
                }
            }
        }
        public Medico(int id, string nome, string crm, string especialidade, string senha)
        {
            Id = id;
            Nome = nome;
            CRM = crm;
            Especialidade = especialidade;
            Senha = senha;

        }

        public bool IsValid()
        {
            return Id > 0;
        }
        public void Save()
        {
            using (var conn = new SqlConnection(DBinfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO MEDICO(NOME, CRM, ESPECIALIDADE, SENHA) VALUES( @NOME, @CRM, @ESPECIALIDADE, @SENHA)";
                cmd.Parameters.AddWithValue("@NOME", Nome);
                cmd.Parameters.AddWithValue("@CRM", CRM);
                cmd.Parameters.AddWithValue("@ESPECIALIDADE", Especialidade);
                cmd.Parameters.AddWithValue("SENHA", Senha);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update()
        {
            using (var conn = new SqlConnection(DBinfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE MEDICO SET NOME = @NOME, CRM = @CRM, ESPECIALIDADE = @ESPECIALIDADE, SENHA = @SENHA WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.Parameters.AddWithValue("@NOME", Nome);
                cmd.Parameters.AddWithValue("@CRM", CRM);
                cmd.Parameters.AddWithValue("@ESPECIALIDADE", Especialidade);
                cmd.Parameters.AddWithValue("SENHA", Senha);


                cmd.ExecuteNonQuery();
            }
        }

        public void Delete()
        {
            using (var conn = new SqlConnection(DBinfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM MEDICO WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", Id);

                cmd.ExecuteNonQuery();
            }
        }
        public static List<Medico> GetAll()
        {
            var result = new List<Medico>();

            using (var conn = new SqlConnection(DBinfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "SELECT ID, NOME, CRM, ESPECIALIDADE, SENHA FROM MEDICO";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Medico medico = new Medico(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));

                        result.Add(medico);
                    }
                }
            }

            return result;
        }
    }
}
