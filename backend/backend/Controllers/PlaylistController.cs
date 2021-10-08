using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        [HttpGet]
        [Route("playlists")]
        public IActionResult GetPlaylists()
        {
            Debug.WriteLine("GET /api/playlists");

            return Ok(DatabaseService.Playlists);
        }

        [HttpGet]
        [Route("playlisttracks/{id}")]
        public IActionResult GetPlaylistTracks(int id)
        {
            Debug.WriteLine($"GET /api/playlisttracks/{id}");

            List<TrackDto> tracks = new();
            DatabaseService.PlaylistTracks.Where(x => x.playlistId == id).ToList().ForEach(x => tracks.Add(DatabaseService.Tracks.Find(y => x.trackId == y.trackId)));

            if (id >= 0 && id < DatabaseService.PlaylistTracks.Count) return Ok(tracks.OrderBy(x => x.name));
            return BadRequest("Invlid id");
        }

        [HttpGet]
        [Route("genres")]
        public IActionResult GetGenres()
        {
            Debug.WriteLine("GET /api/genres");

            return Ok(DatabaseService.Genres);
        }

        [HttpGet]
        [Route("tracks")]
        public IActionResult GetTracksForGenre(int genreid)
        {
            Debug.WriteLine($"GET /api/tracks?genreid={genreid}");

            if (genreid >= 0 && genreid < DatabaseService.Genres.Count) return Ok(DatabaseService.Tracks.Where(track => track.genreId == genreid));
            return BadRequest("Invalid id");
        }

        [HttpPost]
        [Route("track")]
        public IActionResult AddTrackToPlaylist([FromBody] PlaylistTrackDto track)
        {
            Debug.WriteLine("POST /api/track");

            DatabaseService.PlaylistTracks.Add(track);
            return Ok(DatabaseService.PlaylistTracks.Find(t => t == track));
        }

        [HttpDelete]
        [Route("track")]
        public IActionResult RemoveTrackFromPlaylist(int playlistid, int trackid)
        {
            Debug.WriteLine($"DELETE /api/track?playlistid={playlistid}&trackid={trackid}");

            int index = DatabaseService.PlaylistTracks.FindIndex(x => x.playlistId == playlistid && x.trackId == trackid);
            if (index < 0) return BadRequest("Invalid id");

            DatabaseService.PlaylistTracks.RemoveAt(index);
            return Ok(index);
        }
    }
}
