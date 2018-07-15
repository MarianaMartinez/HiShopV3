using HiShop.Dao;
using HiShop.Entity;
using HiShop.Entity.Data;
using HiShop.Enum;
using HiShop.Models.Base;
using HiShop.Models.Filtros;
using HiShop.Views.Negocio;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Models.Negocio
{
    public class NegocioGeneralModelAndView : ModelBase
    {
        public FiltroNegocio filtro { get; set; }

        public int PackActivo { get; set; }
        public List<Entity.Negocio> listaDeNegocios { get; set; }
        public List<Provincia> listaDeProvincias { get; set; }
        public List<Localidad> listaDeLocalidades { get; set; }
        public List<Categoria> listaDeCategorias  { get; set; }
        public List<Entity.Showroom> listaDeShowrooms { get; set; }
        public Entity.Negocio negocioActivo { get; set; }
        public int idNegocioActivo { get; set; }
        public int id { get; set; }

        public string Nombre { get; set; }

        public string urlImagen { get; set; }

        public int CategoriaFK { get; set; }

        public int ProvinciaFK { get; set; }

        public int LocalidadFK { get; set; }

        public string Calle { get; set; }

        public string Numero { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Descripcion { get; set; }

        public IFormFile file { get; set; }

        public string urlForm { get; set; }

        public int ShowroomFk { get; set; }

        public Entity.Showroom Showroom { get; set; }

        public string UsuarioFKModel { get; set; }

        public EstadoNegocio estado { get; set; }

     

        public NegocioGeneralModelAndView(HttpContext httpContext,HiShopContext _context) : base(httpContext, _context)
        {
            listaDeNegocios = new List<Entity.Negocio>();
            //llenarDatosGenerales(httpContext);
        }

        public NegocioGeneralModelAndView(List<Entity.Negocio> listaNegocios)
        {
            if (listaNegocios != null)
              listaDeNegocios = listaNegocios;

            listaDeLocalidades = new List<Localidad>();
            listaDeProvincias = new List<Provincia>();
            listaDeCategorias = new List<Categoria>();
            filtro =  new FiltroNegocio();
            idNegocioActivo = 0;
        }

        public NegocioGeneralModelAndView(List<Entity.Negocio> listaNegocios, HiShopContext _context, HttpContext httpContext)
        {
            if (listaNegocios != null)
            {
                listaDeNegocios = listaNegocios;
                foreach (var n in listaNegocios)
                {
                    //n.Showroom = ShowroomDao.get(_context, n.ShowroomID);
                }
            }

            listaDeLocalidades = new List<Localidad>();
            listaDeProvincias = new List<Provincia>();
            listaDeCategorias = new List<Categoria>();
            filtro = new FiltroNegocio();
            llenarDatosGenerales(httpContext,_context);
            idNegocioActivo = 0;
        }


        public void setProvincias(List<Provincia> lista)
        {
            this.listaDeProvincias = lista;
        }
         public void setLocalidades(List<Localidad> lista)
        {
            this.listaDeLocalidades = lista;
        }

        public void setCategorias(List<Categoria> lista)
        {
            this.listaDeCategorias = lista;
        }

        public void llenarEnBaseANegocioModel(NegocioModelAndView model, HiShopContext _context)
        {
            this.id = model.id;
            this.Nombre = model.Nombre;
            this.CategoriaFK = model.CategoriaFK;
            this.ProvinciaFK = model.ProvinciaFK;
            this.LocalidadFK = model.LocalidadFK;
            this.Calle = model.Calle;
            this.Numero = model.Numero;
            this.Telefono = model.Telefono;
            this.Email = model.Email;
            this.Descripcion = model.Descripcion;
            llenarListados(_context);
        }

        public void llenarEnBaseAEditarNegocioModel(EditarNegocioModelAndView model, HiShopContext _context)
        {
            this.id = model.id;
            this.Nombre = model.Nombre;
            this.CategoriaFK = model.CategoriaFK;
            this.ProvinciaFK = model.ProvinciaFK;
            this.LocalidadFK = model.LocalidadFK;
            this.Calle = model.Calle;
            this.Numero = model.Numero;
            this.Telefono = model.Telefono;
            this.Email = model.Email;
            this.Descripcion = model.Descripcion;
            llenarListados(_context);
        }

        public void llenarListados(HiShopContext _context)
        {
            List<Provincia> pcias = ProvinciaDao.getListado(_context);
            List<Localidad> localidades = LocalidadDao.getListado(_context);
            this.setLocalidades(localidades);
            this.setProvincias(pcias);
            Categoria categoriaPadre = CategoriaDao.getCategoriaPorPadreYNombre(_context, null, "Negocio").Result;
            List<Categoria> lista = CategoriaDao.getCategoriasPorPadre(_context, categoriaPadre).Result;
            this.listaDeShowrooms = ShowroomDao.getListado(_context);
            this.setCategorias(lista);
        }

        public void llenarEnBaseAUnNegocio(Entity.Negocio negocio, HiShopContext _context)
        {
            this.id = negocio.ID;
            this.Nombre = negocio.Nombre;
            this.CategoriaFK = negocio.CategoriaID;
            this.ProvinciaFK = LocalidadDao.get(_context, negocio.LocalidadID).ProvinciaID;
            this.LocalidadFK = negocio.LocalidadID;
            this.Calle = negocio.Calle;
            this.Numero = negocio.Numero;
            this.Telefono = negocio.Telefono;
            this.Email = negocio.Email;
            this.Descripcion = negocio.Descripcion;
            this.urlImagen = negocio.UrlImagenNegocio;
           // this.ShowroomFk = negocio.ShowroomID;
            llenarListados(_context);
            
        }

       
    }
}
