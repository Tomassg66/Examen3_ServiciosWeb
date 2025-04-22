using Examen3_ServiciosWeb.Clases;
using Examen3_ServiciosWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Examen3_ServiciosWeb.Controllers
{
    [RoutePrefix("api/Matriculas")]
    public class MatriculasController : ApiController
    {
        [HttpGet]
        [Route("ConsultarPorDocumentoYSemestre")]
        public IQueryable ConsultarPorDocumentoYSemestre(string documento, string semestre)
        {
            clsMatricula matricula = new clsMatricula();
            return matricula.ConsultarPorDocumentoYSemestre(documento, semestre);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Matricula matricula)
        {
            clsMatricula Matricula = new clsMatricula();
            Matricula.matricula = matricula;
            return Matricula.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Matricula matricula)
        {
            clsMatricula Matricula = new clsMatricula();
            Matricula.matricula = matricula;
            return Matricula.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int idMatricula)
        {
            clsMatricula matricula = new clsMatricula();
            return matricula.Eliminar(idMatricula);
        }
    }
}