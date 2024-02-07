import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../../../.shared/shared.module';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { Video } from '../../models/video.model';
import { YouTubePlayerModule } from '@angular/youtube-player';

@Component({
  selector: 'app-video-player',
  templateUrl: './video-player.component.html',
  styleUrl: './video-player.component.css',
  standalone: true,
  imports: [SharedModule, YouTubePlayerModule],
  providers: [MdbModalService]
})
export class VideoPlayerComponent implements OnInit {
  public videoId: any;
  public title: any;
  private apiLoaded = false;

  ngOnInit(): void {
    if(!this.apiLoaded) {
      const tag = document.createElement('script');
      tag.src = 'https://www.youtube.com/iframe_api';
      document.body.appendChild(tag);
      this.apiLoaded = true;
    }
  }

  constructor(public modalRef: MdbModalRef<VideoPlayerComponent>) {

  }

}
