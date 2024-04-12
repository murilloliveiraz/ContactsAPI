using AutoMapper;
using Azure;
using ContatosAPI.Data;
using ContatosAPI.Data.DTOs;
using ContatosAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ContatosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ContactContext _context;
        private readonly IMapper _mapper;

        public ContactController(ContactContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um contato ao banco de dados
        /// </summary>
        /// <param name="contactDTO">Objeto com os campos necessários para criação de um Contato</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(typeof(ContactDTO), 201)]
        public IActionResult CreateContact([FromBody] ContactDTO contactDTO)
        {
            Contact contact = _mapper.Map<Contact>(contactDTO);
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
        }

        /// <summary>
        /// Retorna todos os Contatos do banco de dados
        /// </summary>
        /// <returns>IEnumerable</returns>
        /// <response code="200">Caso o retorno seja feita com sucesso</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ContactDTO>), 200)]
        public IEnumerable<ContactDTO> ListContacts()
        {
            return _mapper.Map<List<ContactDTO>>(_context.Contacts);
        }

        /// <summary>
        /// Retorna um Contato do banco de dados especificado pelo id
        /// </summary>
        /// <param name="id">Número inteiro que indica o índice do usuário que você quer encontrar</param>
        /// <returns>IEnumerable</returns>
        /// <response code="200">Caso o retorno seja feita com sucesso</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContactDTO), 200)]
        public IActionResult GetContactById(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(contact => contact.Id == id);
            if (contact == null) return NotFound();
            var contactDTO = _mapper.Map<ContactDTO>(contact);
            return Ok(contactDTO);
        }

        /// <summary>
        /// Atualiza um Contato do banco de dados
        /// </summary>
        /// <param name="id">Número inteiro que indica o índice do usuário que você quer atualizar</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public IActionResult UpdateContact(int id, ContactDTO contactDTO)
        {
            var contact = _context.Contacts.FirstOrDefault(contact => contact.Id == id);
            if (contact == null) return NotFound();
            _mapper.Map(contactDTO, contact);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Atualiza parcialmente um Contato do banco de dados
        /// </summary>
        /// <param name="id">Número inteiro que indica o índice do usuário que você quer atualizar</param>
        /// <param name="patch">Objeto contendo as alterações a serem aplicadas</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public IActionResult UpdateContactPartially(int id, JsonPatchDocument<ContactDTO> patch)
        {
            var contact = _context.Contacts.FirstOrDefault(contact => contact.Id == id);
            if (contact == null) return NotFound();
            var contactPatch = _mapper.Map<ContactDTO>(contact);
            patch.ApplyTo(contactPatch, ModelState);
            if (!TryValidateModel(contactPatch)) return ValidationProblem(ModelState);
            _mapper.Map(contactPatch, contact);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta um Contato do banco de dados
        /// </summary>
        /// <param name="id">Número inteiro que indica o índice do usuário que você quer deletar</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a exclusão seja feita com sucesso</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteContact(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(contact => contact.Id == id);
            if (contact == null) return NotFound();
            _context.Remove(contact);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
