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

        public static Negocio.Result UpdateEF(Negocio.Empleado empleado)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {

                using (AccesoDatos.GGarciaEstructuraEntities context = new AccesoDatos.GGarciaEstructuraEntities())
                {
                    var query = context.EmpleadoUpdate(empleado.EmpleadoID, empleado.Nombre,empleado.Departamento.DepartamentoID,empleado.Puesto.PuestoID);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el Empleado";
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

        public static Negocio.Result DeleteEF(Negocio.Empleado empleado)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {

                using (AccesoDatos.GGarciaEstructuraEntities context = new AccesoDatos.GGarciaEstructuraEntities())
                {
                    var query = context.EmpleadoDelete(empleado.EmpleadoID);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó el Empleado";
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
        public static Negocio.Result GetAllEF()
        {
            Negocio.Result result = new Negocio.Result();

            try
            {
                using (AccesoDatos.GGarciaEstructuraEntities context = new AccesoDatos.GGarciaEstructuraEntities())
                {

                    var empleados = context.EmpleadoGetAll().ToList();

                    result.Objects = new List<object>();

                    if (empleados != null)
                    {
                        foreach (var objEmpleado in empleados)
                        {

                            //Instancia de la Clase
                            Negocio.Empleado empleado = new Negocio.Empleado();
                            empleado.EmpleadoID= objEmpleado.EmpleadoID;
                            empleado.Nombre = objEmpleado.Nombre;
                            

                            ///Instancia clase Departamento
                            empleado.Departamento = new Negocio.Departamento();
                            empleado.Departamento.DepartamentoID = objEmpleado.DepartamentoID.Value;
                            empleado.Departamento.Descripcion = objEmpleado.DescripcionDepartamento;


                            //Instancia clase Puesto
                            empleado.Puesto = new Negocio.Puesto();
                            empleado.Puesto.PuestoID = objEmpleado.PuestoID.Value;
                            empleado.Puesto.Descripcion = objEmpleado.DescripcionPuesto;

                            result.Objects.Add(empleado);
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

        public static Negocio.Result GetByIdEF(int EmpleadoID)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (AccesoDatos.GGarciaEstructuraEntities context = new AccesoDatos.GGarciaEstructuraEntities())
                {

                    var objEmpleado = context.EmpleadoGetById(EmpleadoID).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objEmpleado != null)
                    {



                        //Instancia de la Clase
                        Negocio.Empleado empleado = new Negocio.Empleado();
                        empleado.EmpleadoID = objEmpleado.EmpleadoID;
                        empleado.Nombre = objEmpleado.Nombre;


                        ///Instancia clase Departamento
                        empleado.Departamento = new Negocio.Departamento();
                        empleado.Departamento.DepartamentoID = objEmpleado.DepartamentoID.Value;
                        empleado.Departamento.Descripcion = objEmpleado.DescripcionDepartamento;


                        //Instancia clase Puesto
                        empleado.Puesto = new Negocio.Puesto();
                        empleado.Puesto.PuestoID = objEmpleado.PuestoID.Value;
                        empleado.Puesto.Descripcion = objEmpleado.DescripcionPuesto;

                        ///Linea oara igualar el resultado de mi consulta
                        result.Object = empleado;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Empleado";
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
