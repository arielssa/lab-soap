using System.Collections.Generic;
using CoreWCF;
using System.Runtime.Serialization;

namespace ventasSOAP
{
    // --- 2. Modelo Factura ---
    [DataContract]
    public class Factura
    {
        [DataMember]
        public int IdFactura { get; set; }
        [DataMember]
        public string Cliente { get; set; }
        [DataMember]
        public string Fecha { get; set; }
        [DataMember]
        public List<Detalle> Detalles { get; set; } = new List<Detalle>();
    }

    // --- 3. Modelo Detalle ---
    [DataContract]
    public class Detalle
    {
        [DataMember]
        public string Producto { get; set; }
        [DataMember]
        public int Cantidad { get; set; }
        [DataMember]
        public decimal PrecioUnitario { get; set; }
        [DataMember]
        public decimal TotalLinea { get; set; }
    }

    // --- 4. Interfaz IFACTURA ---
    [ServiceContract]
    public interface IFACTURA
    {
        // 5. Contrato para facturar
        [OperationContract]
        string CrearFactura(Factura nuevaFactura, List<Detalle> nuevosDetalles);

        // 7. Contrato para visualizar
        [OperationContract]
        List<Factura> ObtenerFacturas();
    }

    // --- IMPLEMENTACIÓN DEL SERVICIO ---
    public class ServicioVentas : IFACTURA
    {
        // 6. Lista estática para guardar datos en memoria
        public static List<Factura> baseDeDatos = new List<Factura>();

        public string CrearFactura(Factura nuevaFactura, List<Detalle> nuevosDetalles)
        {
            nuevaFactura.Detalles = nuevosDetalles;
            
            // Calculamos totales
            foreach(var d in nuevosDetalles)
            {
                d.TotalLinea = d.Cantidad * d.PrecioUnitario;
            }

            baseDeDatos.Add(nuevaFactura);
            return $"Factura {nuevaFactura.IdFactura} creada exitosamente.";
        }

        public List<Factura> ObtenerFacturas()
        {
            return baseDeDatos;
        }
    }
}