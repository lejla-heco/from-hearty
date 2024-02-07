import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';
import { AgGridAngular } from 'ag-grid-angular';
import { Config } from '../../configuration/config';
import { Video } from './models/video.model';
import { ColDef, ValueFormatterParams, SizeColumnsToContentStrategy, SizeColumnsToFitGridStrategy, SizeColumnsToFitProvidedWidthStrategy } from 'ag-grid-community';
import { HttpClient } from '@angular/common/http';
import { VideoPlayerComponent } from './popups/video-player/video-player.component';
import { CreateEditVideoComponent } from './popups/create-edit-video/create-edit-video.component';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { VideoService } from './video.service';
import { AuthentificationHelper } from '../../authentification/authentification-helper';
import { DeleteVideoComponent } from './buttons/delete-video/delete-video.component';
import { EditVideoComponent } from './buttons/edit-video/edit-video.component';
import { PlayVideoComponent } from './buttons/play-video/play-video.component';

@Component({
  selector: 'app-videos',
  templateUrl: './videos.component.html',
  styleUrl: './videos.component.css',
  standalone: true,
  imports: [SharedModule, AgGridAngular],
  providers: [MdbModalService]
})
export class VideosComponent implements OnInit {
  themeClass = "ag-theme-quartz";
  videos: Video[] = [];
  defaultColDef: ColDef = { filter: true, floatingFilter: true }
  colDefs: ColDef<any>[] = [
    { field: "id", hide: true },
    { field: 'title' },
    { field: 'link' },
    { field: 'created', valueFormatter: this.dateFormatter },
    {
      headerName: 'Play',
      floatingFilter: false,
      filter: false,
      cellStyle: { textAlign: 'center' },
      width: 35,
      cellRenderer: PlayVideoComponent
    },
    {
      headerName: 'Edit',
      floatingFilter: false,
      filter: false,
      width: 35,
      cellStyle: { textAlign: 'center' },
      cellRenderer: EditVideoComponent
    },
    {
      headerName: 'Delete',
      floatingFilter: false,
      filter: false,
      width: 45,
      cellStyle: { textAlign: 'center' },
      cellRenderer: DeleteVideoComponent
    },
  ];
  public autoSizeStrategy:
    | SizeColumnsToFitGridStrategy
    | SizeColumnsToFitProvidedWidthStrategy
    | SizeColumnsToContentStrategy = {
      type: 'fitGridWidth',
    };
  modalRef: MdbModalRef<CreateEditVideoComponent> | null = null;

  dateFormatter(params: ValueFormatterParams) {
    return new Date(params.value).toLocaleDateString('en-us', {
      weekday: 'long',
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }
  mainId: string = AuthentificationHelper.getLoginToken().mainId;

  constructor(private httpClient: HttpClient, private modalService: MdbModalService, public videoService: VideoService) {
  }

  ngOnInit(): void {
    this.getVideos();
  }

  getVideos(): void {
    this.httpClient.get(Config.serverAddress + this.videoService.api.getAllByUserId + '/' + this.mainId).subscribe((response: any) => {
      this.videoService.videos = response;
    });
  }

  addNewVideo(): void {
    this.modalRef = this.modalService.open(CreateEditVideoComponent, {
    });
    this.modalRef.onClose.subscribe((refresh: any) => {
      if (refresh)
        this.getVideos();
    });
  }

  createOrUpdateVideo(newVideo: any, isEdit = false): void {

  }
}
