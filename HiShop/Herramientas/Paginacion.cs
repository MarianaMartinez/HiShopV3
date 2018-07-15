using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Herramientas
{
    public class Paginacion<T>
    {
        public int paginaActual { get; set; }
        public int paginaSiguiente { get; set; }
        public int paginaAnterior { get; set; }
        public int total { get; set; }
        public int cantidadDeElementosPorSeccion { get; set; }

        public Paginacion()
        {
            paginaActual = 1;
            cantidadDeElementosPorSeccion = 4;
           
        }
        

        public List<T> paginar(List<T> lista,int paginaActual)
        {
            int cantidadTotalLista = lista.Count();
                int contador = 0;
                int cantidadElementosTotales = cantidadTotalLista;
                while (cantidadElementosTotales > 0)
                {
                    cantidadElementosTotales = cantidadElementosTotales - cantidadDeElementosPorSeccion;
                    contador++;
                }
                this.total = contador;
            
            List<T> listaPaginada = new List<T>();
            if (cantidadTotalLista < cantidadDeElementosPorSeccion)
            {
                return lista;
            }

            int i = 1;

            
            this.paginaActual = paginaActual;
            if (paginaActual != 0)
            {
                paginaAnterior = paginaActual - 1;
                i = (paginaAnterior * cantidadDeElementosPorSeccion) + 1;
            }
            else {
                paginaActual = 1;
            }

            int limite = cantidadDeElementosPorSeccion;
            limite = paginaActual * cantidadDeElementosPorSeccion;
            if (limite > cantidadTotalLista)
                limite = cantidadTotalLista;

            for (var j = i; i<=limite; i++) {
                listaPaginada.Add(lista[i-1]);
            }
            
               
            return listaPaginada;
        }

        public void llenarPaginar( int total, int paginaActual)
        {
            this.total = total;
            if (total == 0)
            {
                this.total = 1;
            }
            this.paginaActual = paginaActual;
        }
    }

}
