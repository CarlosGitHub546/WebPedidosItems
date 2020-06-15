using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPedidosItems.Models;

namespace WebPedidosItems.Services
{
    public class PedidosService
    {
        public List<Pedido> GetPedidos()
        {
            if (System.Web.HttpContext.Current.Session["Pedidos"] == null)
            {
                System.Web.HttpContext.Current.Session["Pedidos"] = new List<Pedido>();
            }

            return (List<Pedido>)System.Web.HttpContext.Current.Session["Pedidos"];

        }


        public void CreatePedido(Pedido pedido)
        {

            List<Pedido> pedidos = GetPedidos();

            int maxCodigo = 0;
            if (pedidos != null && pedidos.Count() > 0)
            {
                maxCodigo = pedidos.Select(x => x.Codigo).Max();
            }

            pedido.Codigo = maxCodigo + 1;

            if (!(pedido.Items != null && pedido.Items.Count() > 0))
            {
                pedido.Items = new List<ItemPedido>();
            }

            pedidos.Add(pedido);

            System.Web.HttpContext.Current.Session["Pedidos"] = pedidos;

        }

        public void EditPedido(Pedido pedido)
        {
            List<Pedido> pedidos = GetPedidos();

            Pedido pedidoBuscado = pedidos.FirstOrDefault(x => x.Codigo == pedido.Codigo);

            List<Pedido> listaPedidoNueva = new List<Pedido>();

            if (pedidoBuscado != null)
            {
                var enumPedidoNueva = pedidos.Where(x => x.Codigo != pedido.Codigo);
                if (enumPedidoNueva != null && enumPedidoNueva.Count() > 0)
                {
                    listaPedidoNueva.AddRange(enumPedidoNueva);
                }
            }

            listaPedidoNueva.Add(pedido);

            System.Web.HttpContext.Current.Session["Pedidos"] = listaPedidoNueva;

        }


        public Pedido GetPedido(int id)
        {
            List<Pedido> pedidos = GetPedidos();

            return pedidos.FirstOrDefault(x => x.Codigo == id);

        }

        public ItemPedido AddItem(ItemPedido itemPedido)
        {
            List<Pedido> pedidos = GetPedidos();

            Pedido pedidoBuscado = pedidos.FirstOrDefault(x => x.Codigo == itemPedido.CodigoPedido);

            List<Pedido> listaPedidoNueva = new List<Pedido>();

            if (pedidoBuscado != null)
            {
                var enumPedidoNueva = pedidos.Where(x => x.Codigo != itemPedido.CodigoPedido);
                if (enumPedidoNueva != null && enumPedidoNueva.Count() > 0)
                {
                    listaPedidoNueva.AddRange(enumPedidoNueva);
                }

                int maxCodigo = 0;
                if (pedidoBuscado.Items != null && pedidoBuscado.Items.Count() > 0)
                {
                    maxCodigo = pedidoBuscado.Items.Select(x => x.CodigoItem).Max();
                }

                itemPedido.CodigoItem = maxCodigo + 1;

                if (!(pedidoBuscado.Items != null && pedidoBuscado.Items.Count() > 0))
                {
                    pedidoBuscado.Items = new List<ItemPedido>();
                }

                pedidoBuscado.Items.Add(itemPedido);

                listaPedidoNueva.Add(pedidoBuscado);
            }

            System.Web.HttpContext.Current.Session["Pedidos"] = listaPedidoNueva;

            return itemPedido;
        }

        public void FinalizarPedido(int id)
        {
            List<Pedido> pedidos = GetPedidos();

            Pedido pedidoBuscado = pedidos.FirstOrDefault(x => x.Codigo == id);

            List<Pedido> listaPedidoNueva = new List<Pedido>();

            if (pedidoBuscado != null)
            {
                var enumPedidoNueva = pedidos.Where(x => x.Codigo != id);
                if (enumPedidoNueva != null && enumPedidoNueva.Count() > 0)
                {
                    listaPedidoNueva.AddRange(enumPedidoNueva);
                }

                pedidoBuscado.Cerrado = true;

                listaPedidoNueva.Add(pedidoBuscado);
            }

            

            System.Web.HttpContext.Current.Session["Pedidos"] = listaPedidoNueva;

        }
    }
}