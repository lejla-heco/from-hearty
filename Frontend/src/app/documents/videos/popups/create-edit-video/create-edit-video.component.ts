import { Component } from '@angular/core';
import { Video } from '../../models/video.model';
import { SharedModule } from '../../../../.shared/shared.module';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';
import { VideoService } from '../../video.service';
import { Config } from '../../../../configuration/config';
import { AuthentificationHelper } from '../../../../authentification/authentification-helper';

@Component({
  selector: 'app-create-edit-video',
  templateUrl: './create-edit-video.component.html',
  styleUrl: './create-edit-video.component.css',
  standalone: true,
  imports: [SharedModule],
  providers: [MdbModalService]
})
export class CreateEditVideoComponent {
  public video: Video = new Video();
  public isEdit: boolean = false;

  constructor(public modalRef: MdbModalRef<CreateEditVideoComponent>, public toastr: ToastrService,
    private httpClient: HttpClient, private videoService: VideoService) {

  }

  saveVideo(): void {
    if (!this.validateFields()) return;
    this.addNewVideo();
  }

  addNewVideo(): void {
    this.video.userId = AuthentificationHelper.getLoginToken().mainId;
    this.httpClient.put(Config.serverAddress + this.videoService.api.createNewVideo, this.video)
      .subscribe((response: any) => {
        if (!this.isEdit) this.toastr.success("Successfully added new video!");
        else this.toastr.success("Successfully updated the video!");
        this.modalRef.close(true);
      });
  }

  validateFields(): boolean {
    if (this.video.link == undefined || this.video.link == null || this.video.title == undefined || this.video.title == null ||
      this.video.link.trim().length === 0 || this.video.title.trim().length === 0) {
      this.toastr.warning("All fields are mandatory!");
      return false;
    }
    return true;
  }
}
