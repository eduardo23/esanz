using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace esanz.Models
{
    public class DAcliente
    {

        private static SqlConnection cn;
        private static SqlCommand cmd;
        private static SqlDataReader dr;

        private static List<BEcliente> lstBEcliente;
        private static BEcliente objcliente;

        public DAcliente()
        {
            cn = null;
            cmd = null;
            dr = null;

            lstBEcliente = new List<BEcliente>();
        }

        public List<BEcliente> getLstCliente() {
            try
            {
                using (cn = new SqlConnection(DAConexion.ConexionBD()))
                {
                    using (cmd = new SqlCommand("spcliente_getAll", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cn.Open();

                        using (dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                objcliente = new BEcliente();
                                objcliente.id_cliente = ((dr["id_cliente"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["id_cliente"]));
                                objcliente.pNombre = (dr["pNombre"] == DBNull.Value ? string.Empty : dr["pNombre"].ToString());
                                objcliente.sNombre = (dr["sNombre"] == DBNull.Value ? string.Empty : dr["sNombre"].ToString());
                                objcliente.pApellido = (dr["pApellido"] == DBNull.Value ? string.Empty : dr["pApellido"].ToString());
                                objcliente.sApellido = (dr["sApellido"] == DBNull.Value ? string.Empty : dr["sApellido"].ToString());
                                objcliente.Nombres = objcliente.pNombre + ' ' + objcliente.sNombre + ' ' + objcliente.pApellido + ' ' + objcliente.sApellido;

                                lstBEcliente.Add(objcliente);
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                dr.Dispose();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }

            return lstBEcliente;
        }

    }
}