import { Component } from '@angular/core';
import { SharedModule } from '../../../../.shared/shared.module';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { Video } from '../../models/video.model';
import { CreateEditVideoComponent } from '../../popups/create-edit-video/create-edit-video.component';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { HttpClient } from '@angular/common/http';
import { VideoService } from '../../video.service';
import { ToastrService } from 'ngx-toastr';
import { ICellRendererParams } from 'ag-grid-community';
import { Config } from '../../../../configuration/config';
import { AuthentificationHelper } from '../../../../authentification/authentification-helper';

@Component({
  selector: 'app-edit-video',
  templateUrl: './edit-video.component.html',
  styleUrl: './edit-video.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class EditVideoComponent implements ICellRendererAngularComp {
  public video!: Video;
  modalRefEditVideo: MdbModalRef<CreateEditVideoComponent> | null = null;
  mainId: string = AuthentificationHelper.getLoginToken().mainId;

  constructor(private httpClient: HttpClient, private videoService: VideoService, private toastr: ToastrService,
    private modalService: MdbModalService) {
  }

  openModal(){
    this.modalRefEditVideo = this.modalService.open(CreateEditVideoComponent, {
      data: { video: this.video, isEdit: true },
    });
    this.modalRefEditVideo.onClose.subscribe((refresh: any) => {
      if (refresh)
        this.getVideos();
    });
  }

  getVideos(): void {
    this.httpClient.get(Config.serverAddress + this.videoService.api.getAllByUserId + '/' + this.mainId).subscribe((response: any) => {
      this.videoService.videos = response;
    });
  }

  agInit(params: ICellRendererParams): void {
    this.video = params.data;
  }

  refresh(params: ICellRendererParams) {
    this.video = params.data;
    return true;
  }

}
