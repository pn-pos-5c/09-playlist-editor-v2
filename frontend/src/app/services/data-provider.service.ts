import {Injectable} from '@angular/core';
import axios, {AxiosInstance, AxiosResponse} from 'axios';

@Injectable({
  providedIn: 'root'
})
export class DataProviderService {

  private backend: AxiosInstance;

  constructor() {
    this.backend = axios.create({
      baseURL: 'http://localhost:8080/api',
      responseType: 'json'
    });
  }

  async fetchPlaylists(): Promise<AxiosResponse> {
    console.log('GET /api/playlists');
    return this.backend.get('/playlists');
  }

  async fetchTracksForPlaylist(playlistId: number): Promise<AxiosResponse> {
    console.log('GET /api/playlisttracks/:playlistId');
    return this.backend.get(`/playlisttracks/${playlistId}`);
  }

  async fetchGenres(): Promise<AxiosResponse> {
    console.log('GET /api/genres');
    return this.backend.get('/genres');
  }

  async removeTrackFromPlaylist(playlistId: number, trackId: number): Promise<AxiosResponse> {
    console.log('DELETE /api/track?playlistid=playlistId&trackid=trackId');
    return this.backend.delete(`/track?playlistid=${playlistId}&trackid=${trackId}`);
  }

  async fetchTracksForGenre(genreId: number): Promise<AxiosResponse> {
    console.log('GET /api/tracks?genreid=genreId');
    return this.backend.get(`/tracks?genreid=${genreId}`);
  }

  async addTrackToPlaylist(playlistId: number, trackId: number): Promise<AxiosResponse> {
    console.log('POST /api/track');
    return this.backend.post('/track', {playlistid: playlistId, trackid: trackId});
  }
}
