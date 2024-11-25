using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class DocenteDto
    {
        static string SP_Registrar_Docente = "usp_Docente_insert";
        static string SP_Listar_Docente = "usp_Docente_List";

        public async Task<ResultadoTransaccionE<string>> Registrar_Docente(DocenteE objDocente)
        {
            ResultadoTransaccionE<string> resultado = new ResultadoTransaccionE<string>();
            using (SqlConnection cnn = new SqlConnection(ConexionDto.Cnx))
            {
                cnn.Open();
                SqlTransaction transaction = cnn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand(SP_Registrar_Docente, cnn, transaction))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombres", objDocente.Nombres);
                        cmd.Parameters.AddWithValue("@apellidoPaterno", objDocente.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@apellidoMaterno", objDocente.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@nroDocumento", objDocente.NroDocumento);
                        cmd.Parameters.AddWithValue("@fechaNacimiento", objDocente.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@direccion", objDocente.Direccion);
                        cmd.Parameters.AddWithValue("@email", objDocente.Email);
                        cmd.Parameters.AddWithValue("@celular", objDocente.Celular);
                        await cmd.ExecuteNonQueryAsync();

                        resultado.IdRegistro = 0;
                        resultado.Mensaje = "Se ha registrado correctamente el Docente: " + objDocente.Nombres + " "+ objDocente.ApellidoPaterno + " "+ objDocente.ApellidoMaterno;
                        transaction.Commit();
                        transaction.Dispose();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        resultado.IdRegistro = -1;
                        resultado.Mensaje = ex.Message;
                    }
                }
            }
            return resultado;
        }

        public async Task<ResultadoTransaccionE<DocenteE>> Listar_All(string buscar)
        {
            ResultadoTransaccionE<DocenteE> resultado = new ResultadoTransaccionE<DocenteE>();
            using (SqlConnection cnn = new SqlConnection(ConexionDto.Cnx))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(SP_Listar_Docente, cnn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@buscar", buscar);
                        List<DocenteE> Lista = new List<DocenteE>();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                DocenteE objDocente = new DocenteE();
                                objDocente.IdDocente = Convert.ToInt32(reader["IdDocente"].ToString());
                                objDocente.Nombres = reader["Nombres"].ToString();
                                objDocente.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                                objDocente.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                                objDocente.NroDocumento = reader["NroDocumento"].ToString();
                                objDocente.FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"].ToString());
                                objDocente.Direccion = reader["Direccion"].ToString();
                                objDocente.Email = reader["Email"].ToString();
                                objDocente.Celular = reader["Celular"].ToString();
                                Lista.Add(objDocente);
                            }
                        }
                        resultado.IdRegistro = 0;
                        resultado.Mensaje = "Ok";
                        resultado.DataList = Lista;
                    }
                    catch (Exception ex)
                    {
                        resultado.IdRegistro = -1;
                        resultado.Mensaje = ex.Message;
                        resultado.DataList = new List<DocenteE>();
                    }
                }
            }
            return resultado;
        }
    }
}
