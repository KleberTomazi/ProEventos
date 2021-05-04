using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class EventoController : ControllerBase
  {
    public EventoController()
    {
    }

    public IEnumerable<Evento> _eventos = new Evento[]{
        new Evento()
        {
          EventoId = 1,
          Local = "Jundiaí - SP",
          DataEvento = DateTime.Now.AddDays(6).ToString("dd/MM/yyyy"),
          Tema = "Angular e .NET",
          QtdPessoas = 250,
          Lote = "KLT12345",
          ImagemURL = "http://www.google.com"
      },
        new Evento()
        {
          EventoId = 2,
          Local = "São Paulo - SP",
          DataEvento = DateTime.Now.AddDays(12).ToString("dd/MM/yyyy"),
          Tema = "Angular e .NET Core",
          QtdPessoas = 600,
          Lote = "HKL123123",
          ImagemURL = "http://www.yahoo.com"
      }
    };

    [HttpGet]
    public IEnumerable<Evento> Get()
    {
      return _eventos;
    }

    [HttpGet("{id}")]
    public IEnumerable<Evento> GetById(int id)
    {
      return _eventos.Where(evento => evento.EventoId == id);
    }

    [HttpPost]
    public string Post()
    {
      return "Ex. POST";
    }

    [HttpPut("{id}")]
    public string Put(int id)
    {
      return $"Ex. PUT c/ ID = {id}";
    }

    [HttpDelete("{id}")]
    public string Delete(int id)
    {
      return $"Ex. DELETE c/ ID = {id}";
    }
  }
}

