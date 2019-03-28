using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace esanz.Models
{
    public class DAproducto
    {

        private static SqlConnection cn;
        private static SqlCommand cmd;
        private static SqlDataReader dr;

        private static List<BEproducto> lstBEproducto;
        private static BEproducto objproducto;

        public DAproducto()
        {
            cn = null;
            cmd = null;
            dr = null;

            lstBEproducto = new List<BEproducto>();
        }

        public List<BEproducto> getLstproducto(int idCategoria)
        {
            try
            {
                using (cn = new SqlConnection(DAConexion.ConexionBD()))
                {
                    using (cmd = new SqlCommand("spproductobycategoria", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_categoria", SqlDbType.Int).Value = idCategoria;
                        cn.Open();

                        using (dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                objproducto = new BEproducto();
                                objproducto.id_categoria = ((dr["id_categoria"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["id_categoria"]));
                                objproducto.id_producto = ((dr["id_producto"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["id_producto"]));
                                objproducto.nombre = (dr["nombre"] == DBNull.Value ? string.Empty : dr["nombre"].ToString());

                                lstBEproducto.Add(objproducto);
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

            return lstBEproducto;
        }

        public BEproducto getproductobyId(int id)
        {
            objproducto = new BEproducto();

            try
            {
                using (cn = new SqlConnection(DAConexion.ConexionBD()))
                {
                    using (cmd = new SqlCommand("spproductobyid", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cn.Open();

                        using (dr = cmd.ExecuteReader())
                        {
                            dr.Read();

                            if (dr.HasRows)
                            {
                                objproducto.id_categoria = ((dr["id_categoria"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["id_categoria"]));
                                objproducto.id_producto = ((dr["id_producto"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["id_producto"]));
                                objproducto.nombre = (dr["nombre"] == DBNull.Value ? string.Empty : dr["nombre"].ToString());
                                objproducto.precio = ((dr["precio"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["precio"]));
                                objproducto.stock = ((dr["stock"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["stock"]));
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

            return objproducto;
        }
    }
}