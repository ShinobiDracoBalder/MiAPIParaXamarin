using MiAPIParaXamarin.Common.Entities;
using MiAPIParaXamarin.Factories.Interfaces;
using MiAPIParaXamarin.Factories.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiAPIParaXamarin.Controllers
{
    public class CategoriesController  : BaseApiController
    {
        private readonly IFactoryResponse _factoryResponse;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(IFactoryResponse factoryResponse, ICategoryRepository categoryRepository)
        {
            _factoryResponse = factoryResponse;
            _categoryRepository = categoryRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Categoria>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategorias()
        {
            var listaCategorias = await _categoryRepository.GetAllTblCategoriaAsync();
            if (listaCategorias.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaCategorias);
        }
        [AllowAnonymous]
        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(200, Type = typeof(Categoria))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCategoria(int? categoriaId)
        {
            if (categoriaId == null)
            {
                return BadRequest();
            }
            var itemCategoria = await _categoryRepository.GetByIdAsync(categoriaId.Value);
            if (itemCategoria == null)
            {
                return NotFound();
            }
        
            return Ok(itemCategoria);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Categoria))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearCategoria([FromBody] Categoria categoriaDto)
        {
            if (categoriaDto == null)
            {
                return BadRequest(ModelState);
            }

            if (await _categoryRepository.ExisteCategoriaAsync(categoriaDto.Nombre))
            {
                ModelState.AddModelError("", "La categoría ya existe");
                return StatusCode(404, ModelState);
            }
            

            if (await _categoryRepository.AddAsync(categoriaDto)<= 0)
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro{categoriaDto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategoria", new { categoriaId = categoriaDto.CategoriaId }, categoriaDto);
        }

        [HttpPatch("{categoriaId:int}", Name = "ActualizarCategoria")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarCategoria(int categoriaId, [FromBody] Categoria categoriaDto)
        {
            if (categoriaDto == null || categoriaId != categoriaDto.CategoriaId)
            {
                return BadRequest(ModelState);
            }
           
            if (await _categoryRepository.UpdateAsync(categoriaDto) <= 0)
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{categoriaDto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Borrar una categoría existente
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <returns></returns>

        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BorrarCategoria(int categoriaId)
        {
            if (!await _categoryRepository.ExisteCategoriaAsync(categoriaId))
            {
                return NotFound();
            }

            var categoria = await _categoryRepository.GetOnlyTblCategoriaAsync(categoriaId);

            if (await _categoryRepository.DeleteAsync(categoria.CategoriaId)<= 0)
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
