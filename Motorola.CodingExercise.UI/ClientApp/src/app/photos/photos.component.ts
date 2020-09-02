import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html'
})
export class PhotosComponent {
  public photos: MarsRoverPhoto[];

  constructor(http: HttpClient, @Inject('BASE_URL_MARS_ROVER_PHOTO_SERVICE') baseUrl: string) {
    http.get<MarsRoverPhoto[]>(baseUrl + 'MarsRoverPhoto').subscribe(result => {
      this.photos = result;
    }, error => console.error(error));
  }
}

interface MarsRoverPhoto {
  id: number;
  img_src: string;
  earth_date: string;
}
