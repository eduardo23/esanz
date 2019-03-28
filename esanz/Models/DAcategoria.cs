using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace esanz.Models
{
    public class DAcategoria
    {
        private static SqlConnection cn;
        private static SqlCommand cmd;
        private static SqlDataReader dr;

        private static List<BEcategoria> lstBEcategoria;
        private static BEcategoria objcategoria;

        public DAcategoria()
        {
            cn = null;
            cmd = null;
            dr = null;

            lstBEcategoria = new List<BEcategoria>();
        }

        public List<BEcategoria> getLstCategoria()
        {
            try
            {
                using (cn = new SqlConnection(DAConexion.ConexionBD()))
                {
                    using (cmd = new SqlCommand("spcategoria_setAll", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cn.Open();

                        using (dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                objcategoria = new BEcategoria();
                                objcategoria.id_categoria = ((dr["id_categoria"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["id_categoria"]));
                                objcategoria.nombre = (dr["nombre"] == DBNull.Value ? string.Empty : dr["nombre"].ToString());

                                lstBEcategoria.Add(objcategoria);
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

            return lstBEcategoria;
        }
    }
}