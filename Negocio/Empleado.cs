using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Empleado
    {
        ///Propiedades///
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
    
        //Variable para llenar la lista
        public List<object> Empleados { get; set; }

        ///Llaves Fóraneas
        public Negocio.Departamento Departamento { get; set; }
        public Negocio.Puesto Puesto { get; set; }

        ///Metodos
        public static Negocio.Result AddEF(Negocio.Empleado empleado)
        {
            Negocio.Result result = new Negocio.Result();

            try
            {
                ///Modelo Entity 
                using (AccesoDatos.GGarciaEstructuraEntities context = new AccesoDatos.GGarciaEstructuraEntities())
                {

                    ///Variable para hacer Add
                    var query = context.EmpleadoAdd(empleado.Nombre, empleado.Departamento.DepartamentoID, empleado.Puesto.PuestoID);


                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el Empleado";
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateEF(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL_EF.GGarciaProgramacionNCapasEntities context = new DL_EF.GGarciaProgramacionNCapasEntities())
                {
                    var query = context.PolizaUpdate(poliza.IdPoliza, poliza.Nombre, poliza.NumeroPoliza, poliza.User.IdUsuario, poliza.SubPoliza.IdSubPoliza);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó la Poliza";
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

        public static ML.Result DeleteEF(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL_EF.GGarciaProgramacionNCapasEntities context = new DL_EF.GGarciaProgramacionNCapasEntities())
                {
                    var query = context.PolizaDelete(poliza.IdPoliza);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó la Poliza";
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
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.GGarciaProgramacionNCapasEntities context = new DL_EF.GGarciaProgramacionNCapasEntities())
                {

                    var polizas = context.PolizaGetAll().ToList();

                    result.Objects = new List<object>();

                    if (polizas != null)
                    {
                        foreach (var objPoliza in polizas)
                        {

                            //Instancia de la Clase
                            ML.Poliza poliza = new ML.Poliza();
                            poliza.IdPoliza = objPoliza.IdPoliza;
                            poliza.Nombre = objPoliza.Nombre;
                            poliza.NumeroPoliza = objPoliza.NumeroPoliza;
                            poliza.FechaCreacion = objPoliza.FechaCreacion.ToString();
                            poliza.FechaModificacion = objPoliza.FechaModificacion.ToString();

                            ///Instancia clase Usuario
                            poliza.User = new ML.Usuario();
                            poliza.User.IdUsuario = objPoliza.IdUsuario.Value;
                            poliza.User.Nombre = objPoliza.UsuarioUsername;


                            //Instancia clase SubPoliza
                            poliza.SubPoliza = new ML.SubPoliza();
                            poliza.SubPoliza.IdSubPoliza = objPoliza.IdSubPoliza.Value;
                            poliza.SubPoliza.Nombre = objPoliza.SubPolizaNombre;

                            result.Objects.Add(poliza);
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

        public static ML.Result GetByIdEF(int IdPoliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.GGarciaProgramacionNCapasEntities context = new DL_EF.GGarciaProgramacionNCapasEntities())
                {

                    var objPoliza = context.PolizaGetById(IdPoliza).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objPoliza != null)
                    {



                        //Instancia de la Clase
                        ML.Poliza poliza = new ML.Poliza();
                        poliza.IdPoliza = objPoliza.IdPoliza;
                        poliza.Nombre = objPoliza.Nombre;
                        poliza.NumeroPoliza = objPoliza.NumeroPoliza;
                        poliza.FechaCreacion = objPoliza.FechaCreacion.ToString();
                        poliza.FechaModificacion = objPoliza.FechaModificacion.ToString();

                        ///Instancia clase Usuario
                        poliza.User = new ML.Usuario();
                        poliza.User.IdUsuario = objPoliza.IdUsuario.Value;
                        poliza.User.Nombre = objPoliza.UsuarioUsername;


                        //Instancia clase SubPoliza
                        poliza.SubPoliza = new ML.SubPoliza();
                        poliza.SubPoliza.IdSubPoliza = objPoliza.IdSubPoliza.Value;
                        poliza.SubPoliza.Nombre = objPoliza.SubPolizaNombre;

                        ///Linea oara igualar el resultado de mi consulta
                        result.Object = poliza;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Usuario";
                    }

                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
