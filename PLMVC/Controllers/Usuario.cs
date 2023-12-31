﻿using Microsoft.AspNetCore.Mvc;

namespace PLMVC.Controllers
{
    public class Usuario : Controller
    {
        public IActionResult GetAllHistorial(ML.Usuario usuario)
        {
            ML.Result resultoperaciones = BL.Usuario.UsuarioHistorialGetAll(usuario);
            ML.Historial historial = new ML.Historial();
            historial.Historiales = resultoperaciones.Objects;
            historial.IdUsuario = usuario.IdUsuario;
            historial.Resultado = usuario.Resultado;
            return View(historial);
        }

        public IActionResult Operacion(ML.Historial historial)
        {
            ML.Result resultoperaciones = BL.Historial.Operacion(historial);
            historial.Resultado = int.Parse(resultoperaciones.resultado);
            historial.FechaHora = DateTime.Now.ToString();

            ML.Result resultregistro = BL.Historial.AddRegistroHistorial(historial);

            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = historial.IdUsuario;
            ML.Result resultoperaciones1 = BL.Usuario.UsuarioHistorialGetAll(usuario);
            //ML.Historial historial1 = new ML.Historial();
            //historial1.Historiales = resultoperaciones1.Objects;
            usuario.Resultado= historial.Resultado;
            return RedirectToAction("GetAllHistorial", "Usuario", usuario);
        }

        public IActionResult DeleteHistorial(ML.Historial historial)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = historial.IdUsuario;
            ML.Result result = BL.Usuario.DeleteHistorialUsuario(usuario);

            return RedirectToAction("GetAllHistorial", "Usuario", usuario);
        }


    }
}
