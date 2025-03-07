using Microsoft.AspNetCore.Mvc;
using RestAPI.Example.API.Mapping;
using RestAPI.Example.Application.Respositories;
using RestAPI.Example.Contract.Request;

namespace RestAPI.Example.API.Controllers
{
    [ApiController]
    public class MoviesController(IMovieRepository movieRepository) : ControllerBase
    {

        [HttpPost(APIEndpoints.Movie.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMovieRequest createMovieRequest)
        {
            var movie = createMovieRequest.MapToMovie();
            await movieRepository.CreateAsync(movie);
            //return Created(APIEndpoints.Movie.Get, movie.MapToMovieResponse());
            return CreatedAtAction(nameof(GetByIdAsync), new { id = movie.Id }, movie);
        }

        [HttpGet(APIEndpoints.Movie.Get)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var movie = await movieRepository.GetByIdAsync(id);

            if(movie is null) return NotFound();

            return Ok(movie.MapToMovieResponse());
        }

        [HttpGet(APIEndpoints.Movie.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await movieRepository.GetAllAsync();
            return Ok(movies.MapToMoviesResponse());
        }

    }
}
