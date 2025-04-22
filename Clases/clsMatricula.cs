using Examen3_ServiciosWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Examen3_ServiciosWeb.Clases
{
    public class clsMatricula
    {
        private DBExamenEntities dbExamen = new DBExamenEntities();

        public Matricula matricula = new Matricula();

        public string Insertar()
        {
            try
            {
                var estudiante = dbExamen.Estudiantes.FirstOrDefault(e => e.idEstudiante == matricula.idEstudiante);
                if (estudiante == null)
                {
                    return $"Error registrando la matricula, el estudiante con id {matricula.idEstudiante} no se encuentra registrado en la base de datos.";
                }
                else
                {
                    matricula.TotalMatricula = matricula.NumeroCreditos * matricula.ValorCredito;
                    dbExamen.Matriculas.Add(matricula);
                    dbExamen.SaveChanges();
                    return $"Se ha registrado la matricula con id {matricula.idMatricula} para el estudiante {matricula.idEstudiante} en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IQueryable ConsultarPorDocumentoYSemestre(string documento, string semestre)
        {
            return from m in dbExamen.Set<Matricula>()
                   join e in dbExamen.Set<Estudiante>()
                   on m.idEstudiante equals e.idEstudiante
                   where e.Documento == documento && m.SemestreMatricula == semestre
                   select new
                   {
                       nombre = e.NombreCompleto,
                       documento = e.Documento,
                       idMatricula = m.idMatricula,
                       numeroCreditos = m.NumeroCreditos,
                       valorCredito = m.ValorCredito,
                       totalMatricula = m.TotalMatricula,
                       fechaMatricula = m.FechaMatricula,
                       semestre = m.SemestreMatricula,
                       materiasMatriculadas = m.MateriasMatriculadas
                   };
        }

        public string Actualizar()
        {
            try
            {
                var matriculaExistente = dbExamen.Matriculas.FirstOrDefault(m => m.idMatricula == matricula.idMatricula);
                if (matriculaExistente == null)
                {
                    return $"Error, la matricula con id {matricula.idMatricula} no se encuentra registrada en la base de datos.";
                }
                else
                {
                    matricula.TotalMatricula = matricula.NumeroCreditos * matricula.ValorCredito;
                    dbExamen.Matriculas.AddOrUpdate(matricula);
                    dbExamen.SaveChanges();
                    return $"Se ha actualizado la matricula con id {matricula.idMatricula} para el estudiante {matricula.idEstudiante} en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string Eliminar(int idMatricula)
        {
            try
            {
                var matriculaExistente = dbExamen.Matriculas.FirstOrDefault(m => m.idMatricula == idMatricula);
                if (matriculaExistente == null)
                {
                    return $"Error, la matricula con id {matricula.idMatricula} no se encuentra registrada en la base de datos.";
                }
                else
                {
                    dbExamen.Matriculas.Remove(matriculaExistente);
                    dbExamen.SaveChanges();
                    return $"Se ha eliminado la matricula con id {matriculaExistente.idMatricula} para el estudiante {matriculaExistente.idEstudiante} en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}