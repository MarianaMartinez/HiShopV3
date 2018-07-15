using Microsoft.AspNetCore.Mvc;
using HiShop.Models;
using HiShop.Herramientas;
using HiShop.Entity;
using HiShop.Enum;
using HiShop.Entity.Data;
using HiShop.Dao;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using HiShop.Models.Data;
using System.IO;

namespace HiShop.Controllers
{
    public class CategoriaController : BaseCoreController
    {
        private IHostingEnvironment _env;
        public CategoriaController(HiShopContext context, IHostingEnvironment env) : base(context)
        {
            _env = env;
        }

        /// <summary>
        /// Te dirije a una vista con un listado de cateforias en forma de arbol editables
        /// AxelMolaro
        /// </summary>
        /// <returns></returns>
        public IActionResult ListarCategorias()
        {
            List<Categoria> lista = CategoriaDao.getCategoriasPorPadre(_context, null).Result;
            CategoriaModelAndView model = new CategoriaModelAndView(lista);
            model.llenarDatosGenerales(HttpContext, _context);
            return View("ListarCategorias", model);
        }


        /// <summary>
        /// Trae un listado de hijas , segun el padre y las devuelve a la vista , despues se tratan con ajax
        /// AxelMolaro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public List<Categoria> llenarCategoriasHijas(int id)
        {
            MensajeModel mensaje = new MensajeModel("La categoria se edito correctamente .", TipoMensaje.EXITO);

               Categoria categoriaPadre = CategoriaDao.getCategoria(_context, id).Result;
               List<Categoria> lista = CategoriaDao.getCategoriasPorPadre(_context, categoriaPadre).Result;
               return (lista);
        }

        /// Edita una categoria , devuelce un mensaje model q despues se trata con ajax
        /// AxelMolaro
        [HttpPost]
        public async Task<MensajeModel> editarCategoriaAsync(int id, string nombre )
        {
            MensajeModel mensaje = new MensajeModel();
            try
            {
                if (!String.IsNullOrEmpty(nombre))
                {
                    Categoria categoria = CategoriaDao.getCategoria(_context, id).Result;
                    if (categoria.Nombre.Equals(nombre))
                    {
                        mensaje.texto = "Ingrese un nombre diferente .";
                        mensaje.tipo = TipoMensaje.ERROR.ToString();
                    }
                    else {
                        categoria.Nombre = nombre;
                        if (CategoriaDao.existeCategoriaConEsePadreMasDeUnaVez(_context, categoria))
                        {
                            mensaje.texto = "Ya existe una categoria con ese nombre y padre.";
                            mensaje.tipo = TipoMensaje.ERROR.ToString();
                        }
                        else
                        {
                            await CategoriaDao.editarCategoria(_context, categoria);
                            mensaje.texto = "La categoría se guardo con éxito.";
                            mensaje.tipo = TipoMensaje.EXITO.ToString();

                        }
                    }
                 
                }
                else
                {
                    mensaje.texto = "Ingrese un nombre a la categoría.";
                    mensaje.tipo = TipoMensaje.ERROR.ToString();
                }
            }
            catch (OperationCanceledException oce)
            {

                mensaje.texto = "Error al guardar categoría.";
                mensaje.tipo = TipoMensaje.ERROR.ToString();
            }
            catch (Exception exc)
            {

                mensaje.texto = "Error al guardar categoria.";
                mensaje.tipo = TipoMensaje.ERROR.ToString();
            }
            return mensaje;
        }


        /// <summary>
        /// Alta categoria , devuelce un datamodel que contiene un dictiony string object para tratar los datos en ajax
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataModel> altaCategoriaAsync(int id,string nombre)
        {
            MensajeModel mensaje = new MensajeModel();
            DataModel dataModel = new DataModel();
            try
            {
                if (!String.IsNullOrEmpty(nombre))
                {
                    
                    Categoria categoria = new Categoria();
                    categoria.Padre = CategoriaDao.getCategoria(_context, id).Result;
                    categoria.Nombre = nombre;
                    mensaje.texto = "La categoria se guardo con exito .";
                    mensaje.tipo = TipoMensaje.EXITO.ToString();
                    await CategoriaDao.grabarCategoria(_context, categoria);
                }
                else
                {
                    mensaje.texto = "Ingrese un nombre a la categoria.";
                    mensaje.tipo = TipoMensaje.ERROR.ToString();
                }

            }
            catch (InvalidDataException e)
            {
                mensaje.texto = e.Message;
                mensaje.tipo = TipoMensaje.ERROR.ToString();
            }
            catch (Exception e)
            {
                mensaje.texto = e.Message;
                mensaje.tipo = TipoMensaje.ERROR.ToString();
            }
            finally
            {
                dataModel.data.Add("mensaje", mensaje);
                dataModel.data.Add("idCategoriaNueva", CategoriaDao.getUltimaCategoriaAgregada(_context).ID);
                
            }
            return dataModel;

        }

        /// <summary>
        /// Elimina una categoria con ajax 
        /// AxelMolaro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MensajeModel> eliminarCategoriaAsync(int id)
        {
            MensajeModel mensaje = new MensajeModel();
            try
            {
                Categoria categoria = CategoriaDao.getCategoria(_context, id).Result;
                 await CategoriaDao.eliminarCategoria(_context, categoria);
                mensaje.texto = "La categorÍa se a borrado con éxito .";
                mensaje.tipo = TipoMensaje.EXITO.ToString();
            }
            catch (InvalidDataException e)
            {
                mensaje.texto = e.Message;
                mensaje.tipo = TipoMensaje.ERROR.ToString();
            }

            catch (Exception e)
            {
                mensaje.texto = "Error al borrar la categoría";
                mensaje.tipo = TipoMensaje.ERROR.ToString();
            }
            return mensaje;
        }

    }
}