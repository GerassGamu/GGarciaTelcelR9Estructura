using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Puesto
    {
        public int PuestoID { get; set; }
        public string Descripcion { get; set; }
      
        //Variable para llenar la lista
        public List<object> Puestos { get; set; }


        public static Negocio.Result GetAllEF()
        {
            Negocio.Result result = new Negocio.Result();

            try
            {
                using (AccesoDatos.GGarciaEstructuraEntities context = new AccesoDatos.GGarciaEstructuraEntities())
                {

                    var puestos = context.PuestoGetAll().ToList();

                    result.Objects = new List<object>();

                    if (puestos != null)
                    {
                        foreach (var objPuesto in puestos)
                        {

                            //Instancia de la Clase
                            Negocio.Puesto puesto = new Negocio.Puesto();
                            puesto.PuestoID = objPuesto.PuestoID;
                            puesto.Descripcion = objPuesto.Descripcion;




                            result.Objects.Add(puesto);
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
