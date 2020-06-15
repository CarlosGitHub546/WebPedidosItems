using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPedidosItems.Models
{
    public class Pedido
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Persona { get; set; }
        public bool Cerrado { get; set; }
        public List<ItemPedido> Items { get; set; }

        public int Total { get {

                int total = 0;

                if (Items != null && Items.Count() > 0)
                {

                    for (int i = 0; i < Items.Count(); i++)
                    {
                        total += Items[i].Precio + Items[i].Cantidad;
                    }
                }

                return total;
            } }
    }
}