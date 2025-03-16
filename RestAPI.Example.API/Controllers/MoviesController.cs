using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Example.API.Mapping;
using RestAPI.Example.Application.Services;
using RestAPI.Example.Contract.Request;

namespace RestAPI.Example.API.Controllers
{
    [ApiController]
    public class MoviesController(IMovieService movieService) : ControllerBase
    {
        [Authorize(AuthConstants.AdminPolicy)]
        [HttpPost(APIEndpoints.Movie.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMovieRequest createMovieRequest, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var movie = createMovieRequest.MapToMovie();
            await movieService.CreateAsync(movie, cancellationToken);
            return Created(APIEndpoints.Movie.Get, movie.MapToMovieResponse());
            //return CreatedAtAction(nameof(GetByIdAsync), new { id = movie.Id }, movie);
        }

        [Authorize(AuthConstants.ApiUserPolicy)]
        [HttpGet(APIEndpoints.Movie.Get)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var movie = await movieService.GetByIdAsync(id, cancellationToken);

            if (movie is null) return NotFound();

            return Ok(movie.MapToMovieResponse());
        }

        [Authorize(AuthConstants.ApiUserPolicy)]
        [HttpGet(APIEndpoints.Movie.GetByName)]
        public async Task<IActionResult> GetByNameAsync([FromQuery] string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var movies = await movieService.GetByName(name, cancellationToken);

            if (movies is null) return NotFound();

            return Ok(movies.MapToMoviesResponse());
        }

        [Authorize(AuthConstants.ApiUserPolicy)]
        [HttpGet(APIEndpoints.Movie.GetAll)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var movies = await movieService.GetAllAsync(cancellationToken);
            return Ok(movies.MapToMoviesResponse());
        }

        [Authorize(AuthConstants.AdminPolicy)]
        [HttpPut(APIEndpoints.Movie.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateMovieRequest updateMovieRequest, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = await movieService.UpdateAsync(id, updateMovieRequest.MapToMovie(id), cancellationToken);

            if (response) return Ok(updateMovieRequest.MapToMovieResponse(id));

            return NotFound();
        }

        [Authorize(AuthConstants.AdminPolicy)]
        [HttpDelete(APIEndpoints.Movie.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var deleted = await movieService.DeleteAsync(id, cancellationToken);
            if (!deleted) return NotFound();

            return Ok("Record deleted successfully");
        }
    }
}
