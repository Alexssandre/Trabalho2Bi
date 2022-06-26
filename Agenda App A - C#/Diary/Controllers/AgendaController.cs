using Diary.Context;
using Diary.Model;
using Diary.Model.dto;
using Diary.Model.mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly AgendaMapper agendaMapper = new AgendaMapper();
        private readonly AppDbContext _context;
        static readonly HttpClient client = new HttpClient();

        public AgendaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Agenda
        [HttpGet("/teste")]
        public Agenda teste() //Metodo de teste
        {
            Agenda agenda = new Agenda();
            agenda.Cep = "81230170";
            agenda = PreencheAgendaComCEP(agenda);
            return agenda;
        }


        // GET: api/Agenda
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agenda>>> GetAgendas()
        {
            return await _context.Agendas.ToListAsync();
        }

        // GET: api/Agenda/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agenda>> GetAgenda(int id)
        {
            var agenda = await _context.Agendas.FindAsync(id);

            if (agenda == null)
            {
                return NotFound();
            }

            return agenda;
        }

        // PUT: api/Agenda/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgenda(int id, Agenda agenda)
        {
            if (id != agenda.Id)
            {
                return BadRequest();
            }
            agenda = PreencheAgendaComCEP(agenda); //chama a função que preenche a agenda com o cep atualizado!
            _context.Entry(agenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Agenda
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Agenda>> PostAgenda(AgendaDTO agendaDto)
        {
            Agenda agenda = agendaMapper.FromDto(agendaDto);
            agenda = PreencheAgendaComCEP(agenda); //chama a função que preenche a agenda com o cep atualizado!
            _context.Agendas.Add(agenda);
            await _context.SaveChangesAsync();


            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "mensagenss",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                      );

                    string mensagem = JsonSerializer.Serialize(agenda);
                    byte[] bytes = Encoding.UTF8.GetBytes(mensagem);
                    channel.BasicPublish(
                        body: bytes,
                        routingKey: "mensagenss",
                        basicProperties: null,
                        exchange: ""
                      );
                }
            }

            return CreatedAtAction("GetAgenda", new { id = agenda.Id }, agenda);
        }

        // DELETE: api/Agenda/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgenda(int id)
        {
            var agenda = await _context.Agendas.FindAsync(id);
            if (agenda == null)
            {
                return NotFound();
            }

            _context.Agendas.Remove(agenda);
            await _context.SaveChangesAsync();

            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "mensagenss",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                      );

                    string mensagem = JsonSerializer.Serialize(agenda);
                    byte[] bytes = Encoding.UTF8.GetBytes(mensagem);
                    channel.BasicPublish(
                        body: bytes,
                        routingKey: "mensagenss",
                        basicProperties: null,
                        exchange: ""
                      );
                }
            }

            return NoContent();
        }

        private bool AgendaExists(int id)
        {
            return _context.Agendas.Any(e => e.Id == id);
        }

        private Agenda PreencheAgendaComCEP(Agenda agenda)
        {

            String json = ChamaVIACEPERetornaOJSONComoTexto(agenda.Cep).Result;
            Address address = JsonSerializer.Deserialize<Address>(json); //converte a string json para um objeto que foi criado para poder recuperar os dados
            agenda.Cep = address.Cep; // pega o objeto que foi criado e preenche os dados dentro do objeto agenda
            agenda.Complemento = address.Complemento; // pega o objeto que foi criado e preenche os dados dentro do objeto agenda
            agenda.Localidade = address.Localidade;// pega o objeto que foi criado e preenche os dados dentro do objeto agenda
            agenda.Logradouro = address.Logradouro;// pega o objeto que foi criado e preenche os dados dentro do objeto agenda
            agenda.Uf = address.Uf;// pega o objeto que que foi criado e preenche os dados dentro do objeto agenda

            return agenda;
        }

        private async Task<String> ChamaVIACEPERetornaOJSONComoTexto(String cep)
        {
            HttpResponseMessage response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/"); //criando cliente http para fazer uma requisição GET ao via cep, informando o CEP
            response.EnsureSuccessStatusCode(); // valida o status code para ver se a requisição foi um sucesso
            string responseBody = await response.Content.ReadAsStringAsync(); // se a requisição der certo, ele le a resposta do servico como uma string no formato json
            return responseBody;
        }
    }
}
