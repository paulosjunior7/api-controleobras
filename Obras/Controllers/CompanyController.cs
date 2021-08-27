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

    [Route("v1/Company")]
    public class CompanyController : ControllerBase
    {

        #region Métodos Públicos  

        [HttpGet]
        [Route("")]
        ////[Authorize]
        public async Task<ActionResult<List<Company>>> Get([FromServices] DataContext context)
        {
            var Companies = await context.Companies
                                .AsNoTracking().ToListAsync();

            return Ok(Companies);
        }

        [HttpGet]
        [Route("{id:int}")]
        //[Authorize]
        public async Task<ActionResult<Company>> GetById(
            int id,
            [FromServices] DataContext context
        )
        {
            var Company = await context.Companies.Include("Company.Users")
                                .Where(x => x.Id == id)
                                .FirstOrDefaultAsync();
            
            return Ok(Company);
        }

        [HttpPost]
        [Route("")]
        //[Authorize]
        public async Task<ActionResult<Company>> Post([FromBody]Company model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Verifica se possui alguma comanda aberta com o mesmo codigo.
            var Company = await context.Companies.AsNoTracking()
                                .Where(p => p.Cnpj == model.Cnpj)
                                .FirstOrDefaultAsync();

            if (Company != null)
                return BadRequest(new { message = "Já existe uma Concessão Cadastrada com esse CPF/CNPJ." });

            try
            {
                context.Companies.Add(model);               
                await context.SaveChangesAsync();
                Company = await context.Companies.Where(p => p.Id == model.Id)
                                .Include("Company.Users")
                                .AsNoTracking()
                                .FirstOrDefaultAsync();

                return Ok(Company);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível adicionar o Registro." });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize]
        public async Task<ActionResult<Company>> Put(
            int id,
            [FromBody] Company model,
            [FromServices] DataContext context
        )
        {

            if (model.Id != id)
                return NotFound(new { message = "Instância não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {                             
                var CompanyPersistida = await context.Companies.Include("Company.Users")
                                        .AsNoTracking()
                                        .Where(p => p.Id == model.Id)
                                        .FirstOrDefaultAsync();


                //Incluir Alterações que são Permitidas                
                CompanyPersistida = model;


                context.Entry<Company>(CompanyPersistida).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(CompanyPersistida);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o Registro." });
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize]
        public async Task<ActionResult<Company>> Delete(
            int id,
            [FromServices]DataContext context
        )
        {
            var Company = await context.Companies.FirstOrDefaultAsync(p => p.Id == id);
             
            if (Company == null)
                return NotFound(new { message = "Registro não encontrado" });

            try
            {
                context.Companies.Remove(Company);
                await context.SaveChangesAsync();
                return Ok(new { message = "Registro removido com sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível Remover o Registro." });
            }

        }
        
        #endregion


        #region Métodos Privados
        
        #endregion
    }
}

