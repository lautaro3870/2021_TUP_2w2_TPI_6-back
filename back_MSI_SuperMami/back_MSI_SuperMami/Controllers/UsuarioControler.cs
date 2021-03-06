using back_MSI_SuperMami.Comandos;
using back_MSI_SuperMami.Helpers;
using back_MSI_SuperMami.Models;
using back_MSI_SuperMami.Respuestas;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Controllers
{
    //[Authorize]
    [ApiController]
    [EnableCors("back_MSI_SuperMami")]
    public class UsuarioControler : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public UsuarioControler(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        private readonly d4nfd5l4d933b1Context db = new d4nfd5l4d933b1Context();


        

        [HttpGet]
        [Route("usuarios")]
        public ActionResult<RespuestaAPI> Get()
        {
            var resultado = new RespuestaAPI();
            try
            {

                resultado.Ok = true;
                resultado.Respuesta = db.Usuarios.ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al encontrar usuarios";
                return resultado;
            }

        }


        [HttpGet]
        [Route("usuarios/{id}")]
        public ActionResult<RespuestaAPI> GetUsuarios(int id)
        {
            var respusta = new RespuestaAPI();
            if (id == 0)
            {
                respusta.Ok = false;
                respusta.Respuesta = "Ingrese un usuario";
                return respusta;
            }
            else
            {
                var usu = db.Usuarios.Find(id);
                try
                {
                    if (usu != null)
                    {
                        respusta.Ok = true;
                        respusta.Respuesta = usu;
                        return respusta;
                    }
                    return respusta;

                }
                catch
                {
                    respusta.Ok = false;
                    respusta.Respuesta = "No se encuentra el usuario solicitado";
                    return respusta;
                }

            }

        }

        [HttpPost]
        [Route("login")]
        public ActionResult<RespuestaAPI> Login([FromBody] ComandoUsuarioLogin comando)
        {
            var token = jwtAuthenticationManager.Authenticate(comando.email, comando.password);

            RespuestaAPI resultado = new RespuestaAPI();
            var result = db.Usuarios.FirstOrDefault(x => x.Email == comando.email && x.Password == comando.password);
            if (string.IsNullOrEmpty(comando.email))
            {
                resultado.Ok = false;
                resultado.Error = "No se ingreso el nombre";
                return resultado;
            }
            if (string.IsNullOrEmpty(comando.password))
            {
                resultado.Ok = false;
                resultado.Error = "No se ingreso la contraseña";
                return resultado;
            }
            if (result == null)
            {
                Response.StatusCode = 404;
                resultado.Ok = false;
                resultado.Error = "Usuario y/o contraseña incorrecta";

                return resultado;
            }
            else
            {
                if (token == null)
                {
                    return Unauthorized();
                }
                else if (result != null)
                {
                    db.Entry(result).Reference(x => x.rol).Load();
                    var hash = HashHelper.Hash(result.Password);

                    result.Password = hash.Password;
                    
                    resultado.Ok = true;
                    resultado.InfoAdicional = "Login exitoso";
                    login lg = new login(result.NombreUsuario ,result.Email, token, result.rol.Rol);

                    resultado.Respuesta = lg;

                    return resultado;
                    
                }


                else
                {
                    var response = new JObject() {
                            { "StatusCode", 403 },
                            { "Message", "Usuario o contraseña no válida." }
                        };
                    return StatusCode(403, response);
                }
            }
        }
       



        


    } 
}
