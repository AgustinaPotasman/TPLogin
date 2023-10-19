using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PrimerTPconJS.Models;
using log.Models;
using PrimerProyecto.Models;
using PrimerTPconJS.Controllers;

namespace PrimerTPconJS.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Olvide()
    {
        return View();
    }

    public IActionResult VerificarContraseña(string Usuario, string Contraseña)
    {
        if (string.IsNullOrEmpty(Contraseña)  || string.IsNullOrEmpty(Usuario) )
        {
            ViewBag.Error = "Se deben completar todos los campos";
            return RedirectToAction("Account");
        }
        else
        {
            Login verificar = BD.login(Usuario, Contraseña);
        
            if (verificar != null)
            {
                if (Contraseña == verificar.Contraseña)
                {
                    return RedirectToAction("Bienvenida", "Home", new {id=verificar.id});
                }
            }
            else
            {
                ViewBag.Verificar = "El usuario y/o contraseña ingresada son incorrectos";
            }

            return View("Login");
        }
    }
      public IActionResult GuardarUsuario(Login nuevoUser, string Contraseña2)
    {
        if (nuevoUser.Contraseña!= Contraseña2 || string.IsNullOrEmpty(nuevoUser.Usuario) ||  string.IsNullOrEmpty(nuevoUser.Contraseña) || string.IsNullOrEmpty(nuevoUser.Nombre) || string.IsNullOrEmpty(nuevoUser.Email) ||string.IsNullOrEmpty(nuevoUser.Telefono) )
        {
            string alerta="No se compltaron todos los datos o las contraseñas ingresadas no coinciden";
            return RedirectToAction("Registro" , "Account", new {error=alerta});
        }
        else
        {
            BD.CrearCuenta(nuevoUser);
            return RedirectToAction("Login");
        }
    }
    public IActionResult OlvidarContraseña(string Usuario, string NuevaContraseña)
    {
        BD.CambiarContraseña(Usuario, NuevaContraseña);
        return RedirectToAction ("Login");
    }
    public IActionResult Registro(string error)
    {
        ViewBag.Error=error;
        return View();
    }

     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}