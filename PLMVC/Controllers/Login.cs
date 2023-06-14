using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Login(ML.Usuario usuario, string password, string passwordconfirm)
        {
            // Crear una instancia del algoritmo de hash bcrypt
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
            // Obtener el hash resultante para la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            if (passwordconfirm != null)
            {
                //registrar
                usuario.Password = passwordHash;
                ML.Result result = BL.Usuario.AddUser(usuario);
                return View();
            }
            else
            {
                //login
                ML.Result result = BL.Usuario.UserGetByUserName(usuario.UserName);
                usuario = (ML.Usuario)result.Object;

                if (result.Correct == true)
                {
                    if (usuario.Password.SequenceEqual(passwordHash))
                    {
                        return RedirectToAction("GetAllHistorial", "Usuario", usuario);
                    }

                }
                else
                {
                    ViewBag.Mensaje = "Correo o contraseña incorrectos";
                }
            }
            return View("Modal");
        }
    }
}
