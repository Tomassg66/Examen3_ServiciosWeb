using Examen3_ServiciosWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Examen3_ServiciosWeb.Models.libLogin;

namespace Examen3_ServiciosWeb.Clases
{
    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }

        public DBExamenEntities dbExamen = new DBExamenEntities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }

        public IQueryable<LoginRespuesta> Ingresar()
        {
            try
            {
                // Verificar si el usuario existe en la base de datos
                var estudiante = dbExamen.Estudiantes
                    .FirstOrDefault(e => e.Usuario == login.Usuario);

                if (estudiante == null)
                {
                    // Usuario no encontrado
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario incorrecto";
                    return new List<LoginRespuesta> { loginRespuesta }.AsQueryable();
                }

                // Verificar si la contraseña es correcta
                if (estudiante.Clave != login.Clave)
                {
                    // Contraseña incorrecta
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Contraseña incorrecta";
                    return new List<LoginRespuesta> { loginRespuesta }.AsQueryable();
                }

                // Verificar si el usuario tiene matrícula
                bool tieneMatricula = dbExamen.Matriculas
                    .Any(m => m.idEstudiante == estudiante.idEstudiante);

                if (!tieneMatricula)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "El estudiante no tiene matrículas registradas";
                    return new List<LoginRespuesta> { loginRespuesta }.AsQueryable();
                }

                // Generar token si el usuario es válido
                string token = TokenGenerator.GenerateTokenJwt(estudiante.Usuario);

                // Respuesta exitosa
                return new List<LoginRespuesta>
                {
                    new LoginRespuesta
                    {
                        Usuario = estudiante.Usuario,
                        Autenticado = true,
                        Perfil = "Estudiante",
                        PaginaInicio = "paginaEstudiante.html",
                        Token = token
                    }
                }.AsQueryable();
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return new List<LoginRespuesta> { loginRespuesta }.AsQueryable();
            }
        }
    }
}