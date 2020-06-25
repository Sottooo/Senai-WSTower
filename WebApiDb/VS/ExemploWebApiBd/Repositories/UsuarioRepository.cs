using ExemploWebApiBd.Domains;
using ExemploWebApiBd.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploWebApiBd.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private string stringConexao = "Data Source=DEV14\\SQLEXPRESS; initial catalog=InLock_Games_Tarde; user Id=sa; pwd=sa@132";

        public void Atualizar(int id, UsuarioDomain usuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "update Usuarios set Email = @Email, Senha = @Senha  where IdUsuario = @Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    string verificar = "select  Usuarios.Email, Usuarios.Senha from Usuarios where IdUsuario = @Id";

                    using (SqlCommand verificarUser = new SqlCommand(verificar, con))
                    {
                        verificarUser.Parameters.AddWithValue("@Id", id);

                        con.Open();

                        SqlDataReader rdrVerificar = verificarUser.ExecuteReader();

                        if (rdrVerificar.Read())
                        {
                            UsuarioDomain userBackup = new UsuarioDomain();
                            userBackup.Email = rdrVerificar["Email"].ToString();
                            userBackup.Senha = rdrVerificar["Senha"].ToString();
                            userBackup.NomeUsuario = rdrVerificar["NomeUsuario"].ToString();

                            if (String.IsNullOrEmpty(usuario.Email))
                            {
                                cmd.Parameters.AddWithValue("@Email", userBackup.Email);
                                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                                cmd.Parameters.AddWithValue("@NomeUsuario", usuario.NomeUsuario);

                            }
                            else if (String.IsNullOrEmpty(usuario.Senha))
                            {
                                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                                cmd.Parameters.AddWithValue("@Senha", userBackup.Senha);
                                cmd.Parameters.AddWithValue("@NomeUsuario", usuario.NomeUsuario);
                            }

                            rdrVerificar.Close();

                            cmd.ExecuteNonQuery();
                        }

                    }

                }
            }
        }

        public void Cadastrar(UsuarioDomain usuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Usuarios VALUES(@Email, @Senha, @NomeUsuario";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("@NomeUsuario", usuario.NomeUsuario);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "delete from Usuarios where IdUsuario = @Id";
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Logar(UsuarioDomain usuaio)
        {
            throw new NotImplementedException();
        }
    }
}
