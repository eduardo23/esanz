using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace esanz.Models
{
    public class DAmodo_pago
    {

        private static SqlConnection cn;
        private static SqlCommand cmd;
        private static SqlDataReader dr;

        private static List<BEmodo_pago> lstBEmodo_pago;
        private static BEmodo_pago objmodo_pago;

        public DAmodo_pago()
        {
            cn = null;
            cmd = null;
            dr = null;

            lstBEmodo_pago = new List<BEmodo_pago>();
        }

        public List<BEmodo_pago> getLstModo_Pago()
        {
            try
            {
                using (cn = new SqlConnection(DAConexion.ConexionBD()))
                {
                    using (cmd = new SqlCommand("spmodo_pago_getAll", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cn.Open();

                        using (dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                objmodo_pago = new BEmodo_pago();
                                objmodo_pago.num_pago = (dr["num_pago"] == DBNull.Value ? 0 : Convert.ToInt32(dr["num_pago"]));
                                objmodo_pago.nombre = (dr["nombre"] == DBNull.Value ? string.Empty : dr["nombre"].ToString());
                                lstBEmodo_pago.Add(objmodo_pago);
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Dispose();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }

            return lstBEmodo_pago;
        }

    }
}