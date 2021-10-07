import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {PlaylistComponent} from './components/playlist/playlist.component';
import {FormsModule} from "@angular/forms";
import {AddTrackComponent} from './components/add-track/add-track.component';

@NgModule({
  declarations: [
    AppComponent,
    PlaylistComponent,
    AddTrackComponent
  ],
  imports: [
    BrowserModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
