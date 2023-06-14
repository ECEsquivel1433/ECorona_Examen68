using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BL
{
    public class Usuario
    {
        public static ML.Result UserGetByUserName(string UserName)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EcoronaExamen68Context context = new DL.EcoronaExamen68Context())
                {
                    var queryEF = context.Usuarios.FromSqlRaw($"UserGetByUserName '{UserName}'").AsEnumerable().FirstOrDefault();
                    if (queryEF != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = queryEF.IdUsuario;
                        usuario.UserName = queryEF.UserName;
                        usuario.Password = queryEF.Password;


                        result.Object = usuario;

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al insertar el usuario" + ex;
            }
            return result;
        }
        public static ML.Result AddUser(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EcoronaExamen68Context context = new DL.EcoronaExamen68Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddUser  '{usuario.UserName}', @Password", new SqlParameter("@Password", usuario.Password));

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result UsuarioHistorialGetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EcoronaExamen68Context context = new DL.EcoronaExamen68Context())
                {
                    var queryEF = context.Historials.FromSqlRaw($"UsuarioHistorialGetAll {usuario.IdUsuario}").ToList();
                    result.Objects = new List<object>();
                    if (queryEF != null)
                    {
                        foreach (var obj in queryEF)
                        {
                            ML.Historial historial = new ML.Historial();
                            historial.IdHistorial = obj.IdHistorial;
                            historial.Numero = obj.Numero;
                            historial.Resultado = obj.Resultado;
                            historial.FechaHora = obj.FechaHora;
                            historial.IdUsuario = obj.IdUsuario;
                            result.Objects.Add(historial);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al insertar el historial" + ex;
            }
            return result;
        }
    }
}