import { Component, EventEmitter, Output } from '@angular/core';
import { VideoService } from '../../video.service';
import { HttpClient } from '@angular/common/http';
import { Video } from '../../models/video.model';
import { Config } from '../../../../configuration/config';
import { ToastrService } from 'ngx-toastr';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { SharedModule } from '../../../../.shared/shared.module';
import { AuthentificationHelper } from '../../../../authentification/authentification-helper';
import { ConfirmationModalComponent } from '../../../../appointment/components/popups/confirmation-modal/confirmation-modal.component';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';

@Component({
  selector: 'app-delete-video',
  templateUrl: './delete-video.component.html',
  styleUrl: './delete-video.component.css',
  standalone: true,
  imports: [SharedModule],
  providers: [MdbModalService]
})
export class DeleteVideoComponent implements ICellRendererAngularComp {
  public video!: Video;
  mainId: string = AuthentificationHelper.getLoginToken().mainId;
  modalRefConfirmation: MdbModalRef<ConfirmationModalComponent> | null = null;

  constructor(private httpClient: HttpClient, private videoService: VideoService, private toastr: ToastrService,
    private modalService: MdbModalService) {
  }

  agInit(params: ICellRendererParams): void {
    this.video = params.data;
  }

  refresh(params: ICellRendererParams) {
    this.video = params.data;
    return true;
  }

  delete(): void {
    this.httpClient.delete(Config.serverAddress + this.videoService.api.delete + '/' + this.video.id).subscribe((response: any) => {
      this.toastr.success("Successfully deleted the video!");
      this.getVideos();
    });
  }

  getVideos(): void {
    this.httpClient.get(Config.serverAddress + this.videoService.api.getAllByUserId + '/' + this.mainId).subscribe((response: any) => {
      this.videoService.videos = response;
    });
  }

  openConfirmation(): void {
    let confirmationInfo: any = {
      title: 'Delete the video',
      question: 'Are you sure you want to delete the selected video?',
      action: 'Cancel',
      realise: false
    };
    this.openConfirmationModal(confirmationInfo);
  }

  openConfirmationModal(confirmationInfo: any): any {
    this.modalRefConfirmation = this.modalService.open(ConfirmationModalComponent, {
      modalClass: 'modal-sm',
      data: { confirmationInfo: confirmationInfo },
    });
    this.modalRefConfirmation.onClose.subscribe((confirmationInfo: any) => {
      if (confirmationInfo && confirmationInfo.realise && confirmationInfo.action === 'Cancel') this.delete();
    });
  }
}
