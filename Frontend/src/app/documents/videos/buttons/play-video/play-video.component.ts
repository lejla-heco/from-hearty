import { Component, OnInit } from '@angular/core';
import { ICellRendererParams } from 'ag-grid-community';
import { SharedModule } from '../../../../.shared/shared.module';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { HttpClient } from '@angular/common/http';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { ToastrService } from 'ngx-toastr';
import { AuthentificationHelper } from '../../../../authentification/authentification-helper';
import { Video } from '../../models/video.model';
import { CreateEditVideoComponent } from '../../popups/create-edit-video/create-edit-video.component';
import { VideoService } from '../../video.service';
import { Config } from '../../../../configuration/config';
import { VideoPlayerComponent } from '../../popups/video-player/video-player.component';

@Component({
  selector: 'app-play-video',
  templateUrl: './play-video.component.html',
  styleUrl: './play-video.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class PlayVideoComponent implements ICellRendererAngularComp {
  public video!: Video;
  modalRefPlayVideo: MdbModalRef<VideoPlayerComponent> | null = null;
  mainId: string = AuthentificationHelper.getLoginToken().mainId;
  public videoId: any;

  constructor(private httpClient: HttpClient, private videoService: VideoService, private toastr: ToastrService,
    private modalService: MdbModalService) {
  }

  openModal() {
    if (!this.getVideoIdFromLink()) return;
    this.modalRefPlayVideo = this.modalService.open(VideoPlayerComponent, {
      modalClass: 'modal-lg',
      data: { videoId: this.videoId, title: this.video.title },
    });
    this.modalRefPlayVideo.onClose.subscribe(() => {

    });
  }

  agInit(params: ICellRendererParams): void {
    this.video = params.data;
  }

  refresh(params: ICellRendererParams) {
    this.video = params.data;
    return true;
  }

  getVideoIdFromLink(): boolean {
    try {
      const params = new URL(this.video.link).searchParams;
      const videoId = params.get('v');
      if (!videoId) {
        this.toastr.error("Unable to parse video ID from the link!");
        return false;
      }
      this.videoId = videoId;
      return true;
    } catch (error) {
      this.toastr.error("Unable to parse video ID from the link!");
      return false;
    }
  }


}
