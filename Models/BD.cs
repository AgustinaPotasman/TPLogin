using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using log.Models;

namespace PrimerProyecto.Models
{
    public static class BD
    {
        private static string _connectionString = @"Server=localhost;DataBase=Usuario;Trusted_Connection=True;";
        
       public static Login login(string Usuario, string Contraseña)
    {
        Login Ingresado = new Login();
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Usuario WHERE Usuario= @Usuarioo and Contraseña= @Contraseñaa";
            Ingresado = db.QueryFirstOrDefault<Login>(SQL, new { Usuarioo = Usuario , Contraseñaa = Contraseña });
        }
        return Ingresado;
    }
      public static Login ListarPorId(int id)
    {
        Login Ingresado = new Login();  
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Usuario WHERE id= @pid";
            Ingresado = db.QueryFirstOrDefault<Login>(SQL, new { pid= id });
        }
        return Ingresado;
    }
    public static void CrearCuenta(Login user)
{
    string SQL = "INSERT INTO Usuario(Usuario, Contraseña, Nombre, Email, Telefono)";
    SQL += " VALUES (@Usuarioo, @Contraseñaa, @Nombree, @Emaill, @Telefonoo)";

    using (SqlConnection db = new SqlConnection(_connectionString))
    {
        db.Execute(SQL, new
        {
            Usuarioo = user.Usuario,
            Contraseñaa = user.Contraseña,
            Nombree = user.Nombre,
            Emaill = user.Email,
            Telefonoo = user.Telefono
        });
    }
    }
    public static void CambiarContraseña(string Usuario, string NuevaContraseña)
{
    string SQL = "UPDATE Usuario SET Contraseña = @NuevaContraseñaa where Usuario = @NuevoUsuarioo";

    using (SqlConnection db = new SqlConnection(_connectionString))
    {
        db.Execute(SQL, new
        {
            NuevaContraseñaa = NuevaContraseña,
            NuevoUsuarioo = Usuario
        });
    }
    }
    }
    }