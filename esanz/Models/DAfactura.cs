using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace esanz.Models
{
    public class DAfactura
    {
        private static SqlConnection cn;
        private static SqlCommand cmd;
        private static SqlDataReader dr;

        private static List<BEFactura> lstBEfactura;
        private static BEFactura objfactura;
        private static BEcliente objcliente;
        private static BEmodo_pago objmodo_pago;
        private static BEdetalle objdetalle;

        private static ClientResponse clientResponse;
        public DAfactura()
        {
            cn = null;
            cmd = null;
            dr = null;
            lstBEfactura = new List<BEFactura>();
            clientResponse = new ClientResponse();
        }

        public List<BEFactura> getLstFacturaCab(int num_pago, DateTime fecini, DateTime fecfin, string cliente)
        {
            try
            {
                using (cn = new SqlConnection(DAConexion.ConexionBD()))
                {
                    using (cmd = new SqlCommand("spfactura_getAll", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@num_pago", SqlDbType.Int).Value = num_pago;
                        cmd.Parameters.Add("@fecini", SqlDbType.Date).Value = fecini;
                        cmd.Parameters.Add("@fecfin", SqlDbType.Date).Value = fecfin;
                        cmd.Parameters.Add("@cliente", SqlDbType.VarChar,120).Value = cliente;
                        cn.Open();
                        using (dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                objfactura = new BEFactura();
                                objfactura.num_factura = (dr["num_factura"] == DBNull.Value ? 0 : Convert.ToInt32(dr["num_factura"]));
                                objfactura.id_cliente = (dr["id_cliente"] == DBNull.Value ? 0 : Convert.ToInt32(dr["id_cliente"]));
                                objfactura.num_pago = (dr["num_pago"] == DBNull.Value ? 0 : Convert.ToInt32(dr["num_pago"]));
                                objfactura.total = (dr["total"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["total"]));

                                objmodo_pago = new BEmodo_pago();
                                objmodo_pago.num_pago = (dr["num_pago"] == DBNull.Value ? 0 : Convert.ToInt32(dr["num_pago"]));
                                objmodo_pago.nombre = (dr["nom_pago"] == DBNull.Value ? string.Empty : dr["nom_pago"].ToString());
                                objfactura.modo_pago = objmodo_pago;

                                objcliente = new BEcliente();
                                objcliente.id_cliente = ((dr["id_cliente"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["id_cliente"]));
                                objcliente.pNombre = (dr["pNombre"] == DBNull.Value ? string.Empty : dr["pNombre"].ToString());
                                objcliente.sNombre = (dr["sNombre"] == DBNull.Value ? string.Empty : dr["sNombre"].ToString());
                                objcliente.pApellido = (dr["pApellido"] == DBNull.Value ? string.Empty : dr["pApellido"].ToString());
                                objcliente.sApellido = (dr["sApellido"] == DBNull.Value ? string.Empty : dr["sApellido"].ToString());
                                objfactura.cliente = objcliente;

                                lstBEfactura.Add(objfactura);
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
                cn.Close();
                cn.Dispose();
                cmd.Dispose();
                dr.Dispose();
            }

            return lstBEfactura;
        }

        public BEFactura getFacturabyId(int id)
        {

            try
            {
                objfactura = new BEFactura();

                using (cn = new SqlConnection(DAConexion.ConexionBD()))
                {
                    using (cmd = new SqlCommand("spfactura_getbyid", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@num_factura", SqlDbType.Int).Value = id;
                        cn.Open();
                        using (dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {                                
                                dr.Read();

                                objfactura.num_factura = (dr["num_factura"] == DBNull.Value ? 0 : Convert.ToInt32(dr["num_factura"]));
                                objfactura.fecha = Convert.ToDateTime(dr["fecha"]);
                                objfactura.id_cliente = (dr["id_cliente"] == DBNull.Value ? 0 : Convert.ToInt32(dr["id_cliente"]));
                                objfactura.num_pago = (dr["num_pago"] == DBNull.Value ? 0 : Convert.ToInt32(dr["num_pago"]));

                                dr.NextResult();
                                while (dr.Read())
                                {
                                    objdetalle = new BEdetalle();
                                    objdetalle.num_detalle = Convert.ToInt32(dr["num_detalle"]);
                                    objdetalle.num_factura = Convert.ToInt32(dr["num_factura"]);
                                    objdetalle.id_producto = Convert.ToInt32(dr["id_producto"]);

                                    BEproducto objProducto = new BEproducto();
                                    objProducto.id_producto = Convert.ToInt32(dr["id_producto"]); ;
                                    objProducto.nombre = dr["nombre"].ToString();
                                    objdetalle.producto = objProducto;

                                    objdetalle.cantidad = Convert.ToInt32(dr["cantidad"]);
                                    objdetalle.precio = Convert.ToDecimal(dr["precio"]);

                                    objdetalle.subtotal = objdetalle.precio * objdetalle.cantidad;

                                    objfactura.detalle.Add(objdetalle);
                                }
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
                cn.Close();
                cn.Dispose();
                cmd.Dispose();
                dr.Dispose();
            }

            return objfactura;
        }

        public ClientResponse setGrabarFactura(BEFactura objFactura)
        {
            string xml = "";

            try
            {

                XElement root = new XElement("ROOT");
                foreach (BEdetalle detalle in objFactura.detalle)
                {
                    XElement element = new XElement("Detalle",
                    new XElement("num_detalle", detalle.num_detalle),
                    new XElement("id_producto", detalle.id_producto),
                    new XElement("cantidad", detalle.cantidad),
                    new XElement("precio", detalle.precio)
                    );
                    root.Add(element);
                }
                xml = root.ToString();

                using (cn = new SqlConnection(DAConexion.ConexionBD()))
                {
                    using (cmd = new SqlCommand("spfactura_grabar", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@num_factura", SqlDbType.Int).Value = objFactura.num_factura;
                        cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = objFactura.id_cliente;
                        cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = objFactura.fecha;
                        cmd.Parameters.Add("@num_pago", SqlDbType.Int).Value = objFactura.num_pago;
                        cmd.Parameters.AddWithValue("@xml", xml);
                        cmd.Parameters.Add("@ret", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@men", SqlDbType.VarChar,200).Direction = ParameterDirection.Output;
                        cn.Open();
                        cmd.ExecuteNonQuery();

                        clientResponse.Id = Convert.ToInt32(cmd.Parameters["@ret"].Value.ToString());
                        clientResponse.Mensaje = cmd.Parameters["@men"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                cmd.Dispose();
            }
            return clientResponse;
        }

        public ClientResponse setEliminaFactura(int num_factura)
        {

            try
            {

                using (cn = new SqlConnection(DAConexion.ConexionBD()))
                {
                    using (cmd = new SqlCommand("spfactura_eliminar", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@num_factura", SqlDbType.Int).Value = num_factura;
                        cmd.Parameters.Add("@ret", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@men", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                        cn.Open();
                        cmd.ExecuteNonQuery();

                        clientResponse.Id = Convert.ToInt32(cmd.Parameters["@ret"].Value.ToString());
                        clientResponse.Mensaje = cmd.Parameters["@men"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                cmd.Dispose();
            }
            return clientResponse;
        }
    }        
}