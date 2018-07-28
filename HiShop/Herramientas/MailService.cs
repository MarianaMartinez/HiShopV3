using HiShop.Dao;
using HiShop.Entity;
using HiShop.Entity.Data;
using HiShop.Models.Mail;
using System.Net.Mail;

namespace HiShop.Herramientas
{
    public class MailService
    {
        /// <summary>
        /// Envía un mail a la cuenta de los administradores para aprobar un nuevo Usuario.
        /// </summary>
        /// <param name="_objModelMail"></param>
        /// <param name="usuario"></param>
        public void aprobarUsuario(MailModel _objModelMail, Usuario usuario)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(_objModelMail.To = "hishopecommerce@gmail.com");  
            mail.From = new MailAddress(_objModelMail.From = "hishopecommerce@hotmail.com"); //se usa la misma cuenta para enviar y recibir. 
            mail.Subject = "Nueva solicitud de aprobación de usuario"; //asunto.
            string Body =
                "<form action='http://localhost:49837/Registro/AprobarUsuario/" + usuario.ID + "'>" +
                "El usuario: " + usuario.Nombre + " " + usuario.Apellido + "<br />" +
                "Con mail: " + usuario.Mail + "<br />" +
                "Se encuentra en estado: " + usuario.Estado.ToString() + "<br />" +
                "¿Desea aprobar su solicitud?" + "<br />" +
                "<input type='submit' value='Confirmar usuario' />" + "" +
                "</form>"; //formulario que envía a la action "AprobarUsuario" para cambiar el estado del mismo al hacer click en el botón. 
            mail.Body = Body;
            mail.IsBodyHtml = true; 
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("hishopecommerce@hotmail.com", "LigaFederal2018"); //mail y contra
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        /// <summary>
        /// Envía un mail de bienvenida al usuario que se registró y fue aprobado por los administradores.
        /// </summary>
        /// <param name="_objModelMail"></param>
        /// <param name="usuario"></param>
        public void avisoDeAprobacionAUsuario(MailModel _objModelMail, Usuario usuario)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(usuario.Mail);
            mail.From = new MailAddress(_objModelMail.From = "hishopecommerce@hotmail.com");
            mail.Subject = "Solicitud aprobada";
            string Body =
                "<form action= 'http://localhost:49837/Inicio/Inicio/'> " +
                "<center>" +
                "<div style=background-color:darkorange> " +
                "<img src='https://preview.ibb.co/hzaBQx/Logo.png' alt='Logo' border='0' width='200' /> " +
                "<h3>" + usuario.Nombre + " " + usuario.Apellido + ": </h3>" + 
                "<h3> ¡Bienvenido/a a nuestro sitio! </h3>" + 
                "<h2> Te invitamos a visitar nuestros productos </h2>" +
                "<input type='submit' value='Iniciar sesión' style=background-color:dodgerblue;-moz-border-radius: 10px;-webkit-border-radius: 10px;border - radius: 10px;border: 1px solid #000000;padding: 0 4px 0 4px; /> " +
                "</div> " + 
                "</center>" + 
                "</form> " +
                "</body> " +
                "</html>";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("hishopecommerce@hotmail.com", "LigaFederal2018");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        /// <summary>
        /// Envía un mail a la dirección de correo electrónico del Negocio con la consulta sobre un Producto. 
        /// Las consultas las puede realizar un usuario que no está registrado. 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_objModelMail"></param>
        /// <param name="producto"></param>
        public void consultarPorUnProducto(HiShopContext context, MailModel _objModelMail, Producto producto)
        {
            Negocio n = NegocioDao.get(context, producto.NegocioID);
            MailMessage mail = new MailMessage();

            mail.To.Add(n.Email);
            mail.From = new MailAddress(_objModelMail.From = "hishopecommerce@hotmail.com");
            mail.Subject = "Has recibido una consulta";
            string Body =
                "<form action='http://localhost:49837/Catalogo/Consultar/" + producto.ID + "'>" +
                _objModelMail.Nombre + ", te consultó por " + producto.Nombre + "(" + producto.Descripcion + ")" + "<br />" +
                "Mensaje: " + _objModelMail.Body + "<br />" +
                "Respondele a: " + _objModelMail.Email +
                "</form>";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("hishopecommerce@hotmail.com", "LigaFederal2018");
            smtp.EnableSsl = true;
            //smtp.Send(mail);
        }

        /// <summary>
        /// Envía un mail a la cuenta de los administradores para aprobar un Negocio.
        /// </summary>
        /// <param name="_objModelMail"></param>
        /// <param name="negocio"></param>
        public void aprobarNegocio(MailModel _objModelMail, Negocio negocio)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(_objModelMail.To = "hishopecommerce@gmail.com");  
            mail.From = new MailAddress(_objModelMail.From = "hishopecommerce@hotmail.com");
            mail.Subject = "Nueva solicitud de aprobación";
            string Body =
                "<form action='http://localhost:49837/Negocio/AprobarNegocio/" + negocio.ID + "'>" +
                "El negocio: " + negocio.Nombre + " " + "(" + negocio.Descripcion + ")" + "<br />" +
                "Con mail: " + negocio.Email + "<br />" +
                "Se encuentra en estado: " + negocio.Estado.ToString() + "<br />" +
                "¿Desea aprobar su solicitud?" + "<br />" +
                "<input type='submit' value='Confirmar negocio' />" + "" +
                "</form>";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("hishopecommerce@hotmail.com", "LigaFederal2018");
            smtp.EnableSsl = true;
            //smtp.Send(mail);
        }

        /*public void ConfirmarPedido(HiShopContext context, MailModel _objModelMail, OrdenPedido orden)
        {
            MailMessage mail = new MailMessage();

            Negocio n = NegocioDao.get(context, orden.NegocioID);

            mail.To.Add(n.Email);
            mail.From = new MailAddress(_objModelMail.From = "mensajesdelsitio@outlook.com");
            mail.Subject = "Orden de Pedido";
            string Body =
                "<form action='http://localhost:49837/Catalogo/ConcretarCompra/'>" +
                orden.Usuario.Nombre + "solicitó comprar " + orden.Producto.Nombre + "<br />" +
                "¿Se concretó la compra?" + "<br />" +
                "<input type='submit' value='Confirmar' />" + "</form>";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.live.com";
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("mensajesdelsitio@outlook.com", "MsjesDelSitioVetFdL");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }*/

        /*public void ConcretarCompra(HiShopContext context, MailModel _objModelMail, OrdenPedido orden)
        {
            MailMessage mail = new MailMessage();

            Usuario usuario = UsuarioDao.getUsuario(context, orden.Usuario.ID); 

            mail.To.Add(usuario.Mail);
            mail.From = new MailAddress(_objModelMail.From = "mensajesdelsitio@outlook.com");
            mail.Subject = "Orden de Pedido";
            string Body =
                orden.Negocio.Nombre + "aceptó tu solicitud de compra " +  "<br />" +
                "Contactate a: " + orden.Negocio.Email + "<br />";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.live.com";
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("mensajesdelsitio@outlook.com", "MsjesDelSitioVetFdL");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }*/
    }
}
