using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Historial
    {
        public static ML.Result Operacion(ML.Historial historial)
        {

            ML.Result result = new ML.Result();
            int resultado = 0;
            string num;
            string resultado2 = "";
            num = historial.Numero.ToString(); 

            if (num.Length >= 2)
            {
                for (int i = 0; i < num.Length; i++)
                {
                    resultado = resultado + (int)Char.GetNumericValue(num[i]);
                }
                resultado2 = resultado.ToString();
                num = resultado.ToString();

                if (resultado2.Length == 2)
                {
                    resultado = 0;
                    for (int i = 0; i < num.Length; i++)
                    {
                        resultado = resultado + (int)Char.GetNumericValue(num[i]);
                    }
                    resultado2 = resultado.ToString();
                    num = resultado.ToString();
                    if (resultado2.Length == 2)
                    {
                        resultado = 0;
                        for (int i = 0; i < num.Length; i++)
                        {
                            resultado = resultado + (int)Char.GetNumericValue(num[i]);
                        }
                        resultado2 = resultado.ToString();
                        num = resultado.ToString();
                    }
                }
                
                result.resultado = resultado2;
            }
            else
            {
                result.resultado = num.ToString();
            }
            result.Correct = true;
            return result;
        }
        public static ML.Result AddRegistroHistorial(ML.Historial historial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EcoronaExamen68Context context = new DL.EcoronaExamen68Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddRegistroHistorial  {historial.Numero},{historial.Resultado},'{historial.FechaHora}',{historial.IdUsuario}");

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
    }
}
