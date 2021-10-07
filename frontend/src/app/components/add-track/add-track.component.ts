import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import Genre from "../../models/genre";
import Track from "../../models/track";
import {DataProviderService} from "../../services/data-provider.service";
import Playlist from "../../models/playlist";

@Component({
  selector: 'app-add-track',
  templateUrl: './add-track.component.html',
  styleUrls: ['./add-track.component.scss']
})
export class AddTrackComponent implements OnInit {
  genres: Genre[] = [];
  tracks: Track[] = [];

  selectedGenre: Genre;
  selectedTrack: Track;
  @Input() selectedPlaylist?: Playlist;

  trackSelected: boolean;
  @Output() cancelAddTrackDialog;

  constructor(public dataProviderService: DataProviderService) {
    this.selectedGenre = {genreId: -1, name: 'select one'};
    this.selectedTrack = {trackId: -1, name: 'select one'};
    this.trackSelected = false;
    this.cancelAddTrackDialog = new EventEmitter();
  }

  ngOnInit(): void {
    this.loadData();
  }

  async genreChanged() {
    this.tracks = (await this.dataProviderService.fetchTracksForGenre(this.selectedGenre.genreId)).data;
    this.tracks.unshift(this.selectedTrack = {trackId: -1, name: 'select one'});

    this.trackSelected = this.selectedGenre.genreId !== -1 && this.selectedTrack.trackId !== -1;
  }

  trackChanged() {
    this.trackSelected = this.selectedGenre.genreId !== -1 && this.selectedTrack.trackId !== -1;
  }

  async addTrackToPlaylist() {
    await this.dataProviderService.addTrackToPlaylist(this.selectedPlaylist?.playlistId!, this.selectedTrack.trackId);
    this.cancelAddTrackDialog.emit();
  }

  private async loadData() {
    this.genres = (await this.dataProviderService.fetchGenres()).data;
    this.genres.unshift(this.selectedGenre);
    this.tracks.unshift(this.selectedTrack);
  }
}
