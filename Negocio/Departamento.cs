using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }
        public string Descripcion { get; set; }

        //Variable para llenar la lista
        public List<object> Departamentos { get; set; }

        public static Negocio.Result GetAllEF()
        {
            Negocio.Result result = new Negocio.Result();

            try
            {
                using (AccesoDatos.GGarciaEstructuraEntities context = new AccesoDatos.GGarciaEstructuraEntities())
                {

                    var departamentos = context.DepartamentoGetAll().ToList();

                    result.Objects = new List<object>();

                    if (departamentos != null)
                    {
                        foreach (var objDepartamento in departamentos)
                        {

                            //Instancia de la Clase
                            Negocio.Departamento departamento = new Negocio.Departamento();
                            departamento.DepartamentoID = objDepartamento.DepartamentoID;
                            departamento.Descripcion = objDepartamento.Descripcion;


                           

                            result.Objects.Add(departamento);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

    }
}
