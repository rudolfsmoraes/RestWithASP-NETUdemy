using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Business;

namespace RestWithASPNETUdemy.Controllers
{
    /* Mapeia as requisições de http://localhost:{porta}/api/person/
    Por padrão o ASP.NET Core mapeia todas as classes que extendem Controller
    pegando a primeira parte do nome da classe em lower case [Person]Controller
    e expõe como endpoint REST
    */
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonsController : ControllerBase
    {
        //Declaração do serviço usado
        private IPersonBusiness _personBusiness;

        /* Injeção de uma instancia de IPersonService ao criar
        uma instancia de PersonController */
        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        // GET api/values
        //Mapeia as requisições GET para http://localhost:{porta}/api/person/
        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET api/values/5
        //Mapeia as requisições GET para http://localhost:{porta}/api/person/{id}
        //recebendo um ID como no Path da requisição
        //Get com parâmetros para o FindById --> Busca Por ID
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) { return NotFound(); }
            return  Ok(person);
        }

        // POST api/values
        //Mapeia as requisições POST para http://localhost:{porta}/api/person/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        [HttpPost]
        public ActionResult Post([FromBody] Person person)
        {
            if (person == null) { return BadRequest(); }
            return new ObjectResult(_personBusiness.Create(person));
        }

        // PUT api/values/5
        //Mapeia as requisições PUT para http://localhost:{porta}/api/person/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        [HttpPut]
        public ActionResult Put([FromBody] Person person)
        {
            if (person == null) { return BadRequest(); }
            var updatePerson = _personBusiness.Update(person);
            if (updatePerson == null) return NoContent();
            return new ObjectResult(updatePerson);
        }

        // DELETE api/values/5
        //Mapeia as requisições DELETE para http://localhost:{porta}/api/person/{id}
        //recebendo um ID como no Path da requisição
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
