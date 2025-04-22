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
                // Buscar estudiante con usuario y clave exactos
                var estudiante = dbExamen.Estudiantes
                    .FirstOrDefault(e => e.Usuario == login.Usuario && e.Clave == login.Clave);

                if (estudiante == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario o clave incorrectos";
                    return new List<LoginRespuesta> { loginRespuesta }.AsQueryable();
                }

                // Generar token si el usuario es válido
                string token = TokenGenerator.GenerateTokenJwt(estudiante.Usuario);

                return new List<LoginRespuesta>
                {
                    new LoginRespuesta
                    {
                        Usuario = estudiante.Usuario,
                        Autenticado = true,
                        Perfil = "Estudiante", 
                        PaginaInicio = "paginaEstudiante.html",
                        Token = token,
                        Mensaje = ""
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