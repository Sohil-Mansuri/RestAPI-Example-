using Microsoft.AspNetCore.Mvc;
using RestAPI.Example.API.Mapping;
using RestAPI.Example.Application.Services;
using RestAPI.Example.Contract.Request;

namespace RestAPI.Example.API.Controllers
{
    [ApiController]
    public class MoviesController(IMovieService movieService) : ControllerBase
    {

        [HttpPost(APIEndpoints.Movie.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMovieRequest createMovieRequest)
        {
            var movie = createMovieRequest.MapToMovie();
            await movieService.CreateAsync(movie);
            return Created(APIEndpoints.Movie.Get, movie.MapToMovieResponse());
            //return CreatedAtAction(nameof(GetByIdAsync), new { id = movie.Id }, movie);
        }

        [HttpGet(APIEndpoints.Movie.Get)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var movie = await movieService.GetByIdAsync(id);

            if (movie is null) return NotFound();

            return Ok(movie.MapToMovieResponse());
        }

        [HttpGet(APIEndpoints.Movie.GetByName)]
        public async Task<IActionResult> GetByNameAsync([FromQuery] string name)
        {
            var movies = await movieService.GetByName(name);

            if (movies is null) return NotFound();

            return Ok(movies.MapToMoviesResponse());
        }

        [HttpGet(APIEndpoints.Movie.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await movieService.GetAllAsync();
            return Ok(movies.MapToMoviesResponse());
        }


        [HttpPut(APIEndpoints.Movie.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateMovieRequest updateMovieRequest)
        {
            var response = await movieService.UpdateAsync(id, updateMovieRequest.MapToMovie(id));

            if (response) return Ok(updateMovieRequest.MapToMovieResponse(id));

            return NotFound();
        }


        [HttpDelete(APIEndpoints.Movie.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var deleted = await movieService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return Ok("Record deleted successfully");
        }
    }
}
