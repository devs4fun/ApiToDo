using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ToDo.Models;
using ToDo.Repository;

namespace ToDo.Controllers
{
    public class ToDoController : Controller
    {
        readonly ITarefaRepository _tarefaRepository;
        HttpClient cliente = new HttpClient();
        public ToDoController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
            cliente.BaseAddress = new Uri("https://localhost:44357");
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> Get([FromHeader] string chave)
        {
            // veficar se a chave existe
            HttpResponseMessage response = await cliente.PostAsJsonAsync("/api/usuario/validarchave", chave);
            if (response.StatusCode.ToString() == "OK")
            {
                var requisicao = response.Content.ReadAsAsync<Usuario>();
                var listaDeTarefas = _tarefaRepository.Get();
                List<Tarefa> listaTarefasUsuario = new List<Tarefa>();
                foreach(Tarefa t in listaDeTarefas)
                {
                    if(t.IdUsuario == requisicao.Result.Id)
                    {
                        listaTarefasUsuario.Add(t);
                    }
                }
                return Ok(listaTarefasUsuario);
            }

            return BadRequest();
        }

        [Route("api/[controller]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Pegar([FromRoute] int id, [FromHeader] string chave)
        {
            HttpResponseMessage response = await cliente.PostAsJsonAsync("/api/usuario/validarchave", chave);
            if (response.StatusCode.ToString() == "OK")
            {
                var requisicao = response.Content.ReadAsAsync<Usuario>();
                if (id <= 0)
                    return NotFound();

                var tarefa = _tarefaRepository.Pegar(id);
                if (tarefa == null)
                    return NotFound();
                if (tarefa.IdUsuario == requisicao.Result.Id)
                {
                    return Ok(tarefa);
                }
            }

            return BadRequest();
        }

        [Route("api/[controller]")]
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Tarefa tarefa, [FromHeader] string chave)
        {
            // veficar se a chave existe, retornar o usuario dessa chave e vincular o usuário a essa tarefa
            HttpResponseMessage response = await cliente.PostAsJsonAsync("/api/usuario/validarchave", chave);

            if (response.StatusCode.ToString() == "OK")
            {
                var requisicao = response.Content.ReadAsAsync<Usuario>();
                if (string.IsNullOrEmpty(tarefa.Nome))
                    return BadRequest();
                else
                {

                    tarefa.IdUsuario = requisicao.Result.Id;
                        _tarefaRepository.Adicionar(tarefa);
                    return Ok(tarefa);
                }
                    

            }

            return BadRequest();
        }

        [Route("api/[controller]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Deletar([FromRoute] int id, [FromHeader] string chave)
        {
            // veficar se a chave existe, retornar o usuario dessa chave ,verificar se o usuario está vinculado a essa tarefa.
            HttpResponseMessage response = await cliente.PostAsJsonAsync("/api/usuario/validarchave", chave);

            if (response.StatusCode.ToString() == "OK")
            {
                var requisicao = response.Content.ReadAsAsync<Usuario>();

                if (id > 0)
                {
                    var tarefa = _tarefaRepository.Pegar(id);

                    if (tarefa == null)
                    {
                        return BadRequest();
                    }

                    if (requisicao.Result.Id == tarefa.IdUsuario)
                    {
                        _tarefaRepository.Deletar(tarefa);
                        return Ok();
                    }
                }
                                
            }
            return BadRequest();
        }

        [Route("api/[controller]")]
        [HttpPatch]
        public async Task<IActionResult> Atualizar([FromBody] Tarefa tarefa, [FromHeader] string chave)
        {
            if (tarefa.Id <= 0 || string.IsNullOrEmpty(tarefa.Nome))
            {
                return BadRequest();
            }

            // veficar se a chave existe, retornar o usuario dessa chave ,verificar se o usuario está vinculado a essa tarefa.
            HttpResponseMessage response = await cliente.PostAsJsonAsync("/api/usuario/validarchave", chave);

            if (response.StatusCode.ToString() == "OK")
            {
                var requisicao = response.Content.ReadAsAsync<Usuario>();

                //var tarefaDoBanco = _tarefaRepository.Pegar(tarefa.Id);

                if (requisicao.Result.Id == tarefa.IdUsuario)
                {
                    _tarefaRepository.Atualizar(tarefa);
                    return Ok(tarefa);
                }
            }

            return BadRequest();   
        }
    }
}