using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Base.Project.Data;
using Base.Project.Models;
using Base.Project.Enums;
using Base.Project.Services;

namespace Base.Project.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
        {

            try
            {
                var usuarios = await context.Users.AsNoTracking().ToListAsync();
                if (usuarios.Count != 0 )
                {
                    return BadRequest(new
                    {
                        message = "Não Foi possível processar sua requisição"
                    });
                }
                var companyPadrao = new Company
                {
                    CorporateName = "Paulus Engenharias",
                    Active = true,
                    Address = "Rua da Casa do Paulo",
                    CreationDate = DateTime.Now,                    
                    Cnpj = "13.002.002/0001-42",
                    EMail = "contato@codestark.com.br",
                    CellPhone = "62992616406"                   
                };
                var usuarioPadrao  = new User { Name = "Paulo", Email = "pauloadm@gmail.com", Password = "fabio_eh_brother", Perfil = ePerfil.Administrador };  

                context.Companies.Add(companyPadrao);
                await context.SaveChangesAsync();
                context.Users.Add(usuarioPadrao);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Dados configurados"
                });
            }
            catch (Exception e) 
            {
                return BadRequest(new
                {
                    message = "Não Foi possível processar sua requisição"
                });
            }
        }
    }
}